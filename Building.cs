using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueMarbleProject
{
    public partial class Building : Form
    {
        public Building()
        {
            InitializeComponent();
            BuildingInfoSet();
        }

        public void BuildingInfoSet() // 라디오 버튼 활성 or 비활성 처리 및 건물 별 가격 초기화
        {
            // 지역명 텍스트 초기화
            areaName.Text = MainBoard.AreaInfo[MainBoard.areaIndex].AreaName;

            // 라디오 버튼 활성화
            Villa.Enabled = true;
            Apart.Enabled = true;
            Hotel.Enabled = true;
            Randmark.Enabled = true;

            int[] buildPrice = new int[4]; // 건물 별 가격 저장
            buildPrice[0] = MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[0];
            buildPrice[1] = MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[1];
            buildPrice[2] = MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[2];
            buildPrice[3] = MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[3];

            // 라디오 버튼 텍스트 지역별 가격 초기화
            Villa.Text = string.Format("별장 : " + buildPrice[0]);
            Apart.Text = string.Format("빌딩 : " + buildPrice[1]);
            Hotel.Text = string.Format("호텔 : " + buildPrice[2]);
            Randmark.Text = string.Format("랜드마크 : " + buildPrice[3]);

            if (MainBoard.AreaInfo[MainBoard.areaIndex].Owner == 0) // 소유주가 없을 경우
            {
                Randmark.Enabled = false; // 랜드마크는 호텔이 지어져있을 경우 업그레이드 가능
                // 관광지일 경우 랜드마크만 건설
                if (MainBoard.areaIndex == 4 || MainBoard.areaIndex == 13 || MainBoard.areaIndex == 22 || MainBoard.areaIndex == 29 || MainBoard.areaIndex == 35)
                {
                    Villa.Enabled = false;
                    Apart.Enabled = false;
                    Hotel.Enabled = false;
                    Randmark.Enabled = true;
                }
            }
            else // 소유주가 본인일 경우
            {
                int index = MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.IndexOf(MainBoard.areaIndex); // 해당 지역이 플레이어 areaIndex 리스트의 몇번 인덱스에 있는지
                string buildName = MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName[index];
                if (buildName == "별장")
                {
                    // 라디오 버튼 비활성화
                    Villa.Enabled = false;
                    Randmark.Enabled = false;

                    // 해당 건물 별 가격 초기화
                    Villa.Text = string.Format("별장 : " + 0);
                    Apart.Text = string.Format("빌딩 : " + (buildPrice[1] - buildPrice[0]));
                    Hotel.Text = string.Format("호텔 : " + (buildPrice[2] - buildPrice[0]));
                    Randmark.Text = string.Format("랜드마크 : " + (buildPrice[3] - buildPrice[0]));
                }
                else if (buildName == "빌딩")
                {
                    // 라디오 버튼 비활성화
                    Villa.Enabled = false;
                    Apart.Enabled = false;
                    Randmark.Enabled = false;

                    // 해당 건물 별 가격 초기화
                    Villa.Text = string.Format("별장 : " + 0);
                    Apart.Text = string.Format("빌딩 : " + 0);
                    Hotel.Text = string.Format("호텔 : " + (buildPrice[2] - buildPrice[1]));
                    Randmark.Text = string.Format("랜드마크 : " + (buildPrice[3] - buildPrice[1]));
                }
                else if (buildName == "호텔")
                {
                    // 라디오 버튼 비활성화
                    Villa.Enabled = false;
                    Apart.Enabled = false;
                    Hotel.Enabled = false;

                    // 해당 건물 별 가격 초기화
                    Villa.Text = string.Format("별장 : " + 0);
                    Apart.Text = string.Format("빌딩 : " + 0);
                    Hotel.Text = string.Format("호텔 : " + 0);
                    Randmark.Text = string.Format("랜드마크 : " + (buildPrice[3] - buildPrice[2]));
                }
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            int[] buildArea = { 0, 0, 1, 2, 0, 3, 0, 4, 5, 0, 6, 7, 8, 0, 9, 10, 0, 11, 0, 12, 0, 13, 0, 14, 15, 0, 16, 0, 17, 0, 18, 0, 19, 20, 0, 0 };
            int buildIndex = buildArea[MainBoard.areaIndex]; // 건물 별 인덱스 위치

            int playerBuildIndex = MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.IndexOf(MainBoard.areaIndex); // 해당 플레이어가 소유한 건물의 위치 areaIndex 배열 안의 인덱스 번호

            if (!Villa.Checked && !Apart.Checked && !Hotel.Checked && !Randmark.Checked) // 건물 하나도 선택 안했을 경우
            {
                MessageBox.Show("건물을 하나 선택해 주세요.");
                return;
            }
            if (Villa.Checked) // 빌라 선택시
            {
                string[] strTemp = Villa.Text.Split(new string[] { "별장 : " }, StringSplitOptions.None);
                int price = int.Parse(strTemp[1]);
                // 플레이어가 가진 금액이 사려는 건물 금액보다 작을 경우
                if (MainBoard.PlayerInfo[MainBoard.diceTurn].Money < price)
                {
                    MessageBox.Show("금액이 부족합니다.");
                    return;
                }

                // player 정보에 구매 지역 전 리스트 값 제거
                if (MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Contains(MainBoard.areaIndex))
                {
                    MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.RemoveAt(playerBuildIndex);
                    MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.RemoveAt(playerBuildIndex);
                }

                // player 정보에 구매 지역 인덱스 번호, 건물 이름 저장
                MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Add(MainBoard.areaIndex);
                MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.Add("별장");

                // 건물 구매 시 player 보유 금액 빼기
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= price;

                // 건물 사진 넣기
                MainBoard.Build[buildIndex].Image = imageList1.Images[0];
                MainBoard.Build[buildIndex].Visible = true;
            }
            else if (Apart.Checked) // 빌딩 선택시
            {
                string[] strTemp = Apart.Text.Split(new string[] { "빌딩 : " }, StringSplitOptions.None);
                int price = int.Parse(strTemp[1]);
                // 플레이어가 가진 금액이 사려는 건물 금액보다 작을 경우
                if (MainBoard.PlayerInfo[MainBoard.diceTurn].Money < price)
                {
                    MessageBox.Show("금액이 부족합니다.");
                    return;
                }

                // player 정보에 구매 지역 전 리스트 값 제거
                if (MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Contains(MainBoard.areaIndex))
                {
                    MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.RemoveAt(playerBuildIndex);
                    MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.RemoveAt(playerBuildIndex);
                }

                // player 정보에 구매 지역 인덱스 번호, 건물 이름 저장
                MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Add(MainBoard.areaIndex);
                MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.Add("빌딩");

                // 건물 구매 시 player 보유 금액 빼기
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= price;

                // 건물 사진 넣기
                MainBoard.Build[buildIndex].Image = imageList1.Images[1];
                MainBoard.Build[buildIndex].Visible = true;
            }
            else if (Hotel.Checked) // 호텔 선택시
            {
                string[] strTemp = Hotel.Text.Split(new string[] { "호텔 : " }, StringSplitOptions.None);
                int price = int.Parse(strTemp[1]);
                // 플레이어가 가진 금액이 사려는 건물 금액보다 작을 경우
                if (MainBoard.PlayerInfo[MainBoard.diceTurn].Money < price)
                {
                    MessageBox.Show("금액이 부족합니다.");
                    return;
                }

                // player 정보에 구매 지역 전 리스트 값 제거
                if (MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Contains(MainBoard.areaIndex))
                {
                    MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.RemoveAt(playerBuildIndex);
                    MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.RemoveAt(playerBuildIndex);
                }

                // player 정보에 구매 지역 인덱스 번호, 건물 이름 저장
                MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Add(MainBoard.areaIndex);
                MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.Add("호텔");

                // 건물 구매 시 player 보유 금액 빼기
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= price;

                // 건물 사진 넣기
                MainBoard.Build[buildIndex].Image = imageList1.Images[2];
                MainBoard.Build[buildIndex].Visible = true;
            }
            else if (Randmark.Checked) // 랜드마크 선택시
            {
                string[] strTemp = Randmark.Text.Split(new string[] { "랜드마크 : " }, StringSplitOptions.None);
                int price = int.Parse(strTemp[1]);
                // 플레이어가 가진 금액이 사려는 건물 금액보다 작을 경우
                if (MainBoard.PlayerInfo[MainBoard.diceTurn].Money < price)
                {
                    MessageBox.Show("금액이 부족합니다.");
                    return;
                }

                // player 정보에 구매 지역 전 리스트 값 제거
                if (MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Contains(MainBoard.areaIndex))
                {
                    MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.RemoveAt(playerBuildIndex);
                    MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.RemoveAt(playerBuildIndex);
                }

                // player 정보에 구매 지역 인덱스 번호, 건물 이름 저장
                MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Add(MainBoard.areaIndex);
                MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.Add("랜드마크");

                // 건물 구매 시 player 보유 금액 빼기
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= price;

                // 관광지는 컬러 변경으로 대체
                if (MainBoard.areaIndex == 4 || MainBoard.areaIndex == 13 || MainBoard.areaIndex == 22 || MainBoard.areaIndex == 29)
                {
                    MainBoard.Area[MainBoard.areaIndex].BackColor = Color.DeepSkyBlue;
                }
                else if (MainBoard.areaIndex == 35)
                {
                    MainBoard.Area[MainBoard.areaIndex].BackColor = Color.Purple;
                }
                else // 랜드마크 사진 넣기 
                {
                    MainBoard.Build[buildIndex].Image = imageList1.Images[3];
                    MainBoard.Build[buildIndex].Visible = true;
                }
            }
            MainBoard.AreaInfo[MainBoard.areaIndex].Owner = MainBoard.diceTurn; // 소유주 변경
            MessageBox.Show("구매 완료.");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
