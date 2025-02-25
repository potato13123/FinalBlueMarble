﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BlueMarbleProject
{
    public partial class BuildingSell : Form
    {
        private int payMoney = 0; // 매각 총 금액
        private int nowMoney = 0; // 매각 후 현재 플레이어 금액
        private int listViewCnt = 0; // listView2 몇개의 항목인지

        public BuildingSell()
        {
            InitializeComponent();
            ListViewSet();
        }

        public void ListViewSet() // ListView1에 보유한 지역 정보 입력
        {
            for (int i = 0; i < MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Count; i++)
            {
                int areaIndex = MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex[i]; // 해당 지역의 인덱스 번호
                string buildName = MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName[i]; // 건물 상태 ( 별장, 빌딩, 호텔, 랜드마크 )
                string areaName = MainBoard.AreaInfo[areaIndex].AreaName; // 해당 지역의 이름
                // 건물 상태에 따른 가격
                int buildPrice = 0;
                switch (buildName)
                {
                    case "별장":
                        buildPrice = MainBoard.AreaInfo[areaIndex].SellPrice[0];
                        break;
                    case "빌딩":
                        buildPrice = MainBoard.AreaInfo[areaIndex].SellPrice[1];
                        break;
                    case "호텔":
                        buildPrice = MainBoard.AreaInfo[areaIndex].SellPrice[2];
                        break;
                    case "랜드마크":
                        buildPrice = MainBoard.AreaInfo[areaIndex].SellPrice[3];
                        break;
                }
                listView1.Items.Add(new ListViewItem(new string[] { areaName, buildName, buildPrice.ToString() }));
            }
            // 금액 라벨로 보여주기
            P_Money.Text = string.Format("매각 금액 : {0:N0}", payMoney);
            nowMoney = MainBoard.PlayerInfo[MainBoard.diceTurn].Money;
            N_Money.Text = string.Format("보유 금액 : {0:N0}", nowMoney);
        }

        private void btnSelect_Click(object sender, EventArgs e) // 매각 선택
        {
            try
            {
                // 선택 항목 listView2로 이동
                int index = listView1.SelectedIndices[0];
                string[] strTemp = { listView1.Items[index].SubItems[0].Text, listView1.Items[index].SubItems[1].Text, listView1.Items[index].SubItems[2].Text };
                listView2.Items.Add(new ListViewItem(new string[] { strTemp[0], strTemp[1], strTemp[2] }));
                listView1.Items.RemoveAt(index);
                listViewCnt++;

                // 금액 라벨로 보여주기
                payMoney += int.Parse(strTemp[2]);
                nowMoney += int.Parse(strTemp[2]);
                P_Money.Text = string.Format("매각 금액 : {0:N0}", payMoney);
                N_Money.Text = string.Format("보유 금액 : {0:N0}", nowMoney);
            }
            catch (Exception ex)
            {
                MessageBox.Show("리스트 항목을 선택해주세요.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) // 매각 선택 취소
        {
            try
            {
                // 선택 취소 항목 listView1로 이동
                int index = listView2.SelectedIndices[0];
                string[] strTemp = { listView2.Items[index].SubItems[0].Text, listView2.Items[index].SubItems[1].Text, listView2.Items[index].SubItems[2].Text };
                listView1.Items.Add(new ListViewItem(new string[] { strTemp[0], strTemp[1], strTemp[2] }));
                listView2.Items.RemoveAt(index);
                listViewCnt--;

                // 금액 라벨로 보여주기
                payMoney -= int.Parse(strTemp[2]);
                nowMoney -= int.Parse(strTemp[2]);
                P_Money.Text = string.Format("지불 할 금액 : {0:N0}", payMoney);
                N_Money.Text = string.Format("현재 금액 : {0:N0}", nowMoney);
            }
            catch (Exception ex)
            {
                MessageBox.Show("리스트 항목을 선택해주세요.");
            }
        }

        private void btnOK_Click(object sender, EventArgs e) // 매각 진행
        {
            int[] buildArea = { 0, 0, 1, 2, 0, 3, 0, 4, 5, 0, 6, 7, 8, 0, 9, 10, 0, 11, 0, 12, 0, 13, 0, 14, 15, 0, 16, 0, 17, 0, 18, 0, 19, 20, 0, 0 };
            // 매각 지역이 선택 안되었을 때
            if (listViewCnt == 0)
            {
                MessageBox.Show("매각 할 지역이 없습니다.");
                return;
            }
            // 현재 금액이 0원보다 적을 경우
            if (nowMoney < 0)
            {
                MessageBox.Show(string.Format("보유 금액이 0원보다 적습니다.\n추가 매각이나 항복을 진행하세요."));
                return;
            }
            for (int i = 0; i < listViewCnt; i++)
            {
                string[] strTemp = { listView2.Items[i].SubItems[0].Text, listView2.Items[i].SubItems[1].Text, listView2.Items[i].SubItems[2].Text };
                // 해당 인덱스 위치 찾기
                int areaIndex = 0;
                for (int j = 0; j < MainBoard.AreaInfo.Length; j++)
                {
                    if (MainBoard.AreaInfo[j].AreaName == strTemp[0])
                    {
                        areaIndex = j;
                        break;
                    }
                }

                MainBoard.AreaInfo[areaIndex].Owner = 0; // 해당 지역 오너 0으로 초기화
                int buildIndex = buildArea[areaIndex]; // 건물 별 인덱스 위치

                // 건물 사진 지우기
                MainBoard.Build[buildIndex].Image = null;
                MainBoard.Build[buildIndex].Visible = false;
                // 해당 플레이어의 areaIndex, buildName 리스트 안의 값 제거
                int index = MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.IndexOf(areaIndex);
                MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.RemoveAt(index);
                MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.RemoveAt(index);
            }
            // 현재 금액이 0원 이상일 경우
            MainBoard.PlayerInfo[MainBoard.diceTurn].Money = nowMoney;
            this.Close();
        }

        private void btnSurrender_Click(object sender, EventArgs e) // 항복
        {
            MessageBox.Show("항복하셨습니다.");
            this.Close();
        }
    }
}