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
    public partial class TakeOver : Form
    {
        private int idx; // 가지고있는 땅의 인덱스 위치

        public TakeOver()
        {
            InitializeComponent();
            LbTextSet();
        }
        public void LbTextSet()
        {
            lbAreaName.Text = ($"{MainBoard.AreaInfo[MainBoard.areaIndex].AreaName} 인수 하시겠습니까?");
            int diceTurn = MainBoard.AreaInfo[MainBoard.playerNowLocation].Owner; 

            int nowMoney = 0;
            int buildPrice = 0;

            idx = MainBoard.PlayerInfo[diceTurn].AreaIndex.IndexOf(MainBoard.areaIndex);
            if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "별장")
            {
                int price = (int)(MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[0] * 1.5);
                nowMoney = MainBoard.PlayerInfo[MainBoard.diceTurn].Money - price;
                buildPrice = price;
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "빌딩")
            {
                int price = (int)(MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[1] * 1.5);
                nowMoney = MainBoard.PlayerInfo[MainBoard.diceTurn].Money - price;
                buildPrice = price;
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "호텔")
            {
                int price = (int)(MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[2] * 1.5);
                nowMoney = MainBoard.PlayerInfo[MainBoard.diceTurn].Money - price;
                buildPrice = price;
            }
            lbPayMoney.Text = string.Format("인수비용 : {0:N0}원", buildPrice);
            lbNowMoney.Text = string.Format("인수후 금액 : {0:N0}원", nowMoney);
        }
        public void TakeOverPay() // 지불가격
        {
            int diceTurn = MainBoard.AreaInfo[MainBoard.playerNowLocation].Owner;
            idx = MainBoard.PlayerInfo[diceTurn].AreaIndex.IndexOf(MainBoard.areaIndex);
            if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "별장")
            {
                int price = (int)(MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[0] * 1.5);
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= price;
                MainBoard.PlayerInfo[diceTurn].Money += price;
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "빌딩")
            {
                int price = (int)(MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[1] * 1.5);
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= price;
                MainBoard.PlayerInfo[diceTurn].Money += price;
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "호텔")
            {
                int price = (int)(MainBoard.AreaInfo[MainBoard.areaIndex].BuildPrice[2] * 1.5);
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= price;
                MainBoard.PlayerInfo[diceTurn].Money += price;
            }
        }
        public void TakeOverBuilding()
        {
            int diceTurn = MainBoard.AreaInfo[MainBoard.playerNowLocation].Owner;
            idx = MainBoard.PlayerInfo[diceTurn].AreaIndex.IndexOf(MainBoard.areaIndex);
            if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "별장")
            {
                MainBoard.PlayerInfo[diceTurn].AreaIndex.RemoveAt(idx);
                MainBoard.PlayerInfo[diceTurn].BuildName.RemoveAt(idx);
                MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Add(MainBoard.areaIndex);
                MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.Add("별장");
                MainBoard.AreaInfo[MainBoard.areaIndex].Owner = MainBoard.diceTurn;
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "빌딩")
            {
                MainBoard.PlayerInfo[diceTurn].AreaIndex.RemoveAt(idx);
                MainBoard.PlayerInfo[diceTurn].BuildName.RemoveAt(idx);
                MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Add(MainBoard.areaIndex);
                MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.Add("빌딩");
                MainBoard.AreaInfo[MainBoard.areaIndex].Owner = MainBoard.diceTurn;
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "호텔")
            {
                MainBoard.PlayerInfo[diceTurn].AreaIndex.RemoveAt(idx);
                MainBoard.PlayerInfo[diceTurn].BuildName.RemoveAt(idx);
                MainBoard.PlayerInfo[MainBoard.diceTurn].AreaIndex.Add(MainBoard.areaIndex);
                MainBoard.PlayerInfo[MainBoard.diceTurn].BuildName.Add("호텔");
                MainBoard.AreaInfo[MainBoard.areaIndex].Owner = MainBoard.diceTurn;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TakeOverPay();
            TakeOverBuilding();
            MessageBox.Show("인수 완료!!");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
