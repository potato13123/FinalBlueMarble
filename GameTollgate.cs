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
    public partial class GameTollgate : Form
    {
        int idx; // 인덱스
        public GameTollgate()
        {
            InitializeComponent();
            LbTextSet();
            FreeCheck();
        }

        public void FreeCheck()
        {
            btnFree.Enabled = false;
            if (MainBoard.PlayerInfo[MainBoard.diceTurn].AngelCard > 0)
                btnFree.Enabled = true;
            else
                btnFree.Enabled = false;
        }

        public void LbTextSet()
        {
            lbAreaName.Text = MainBoard.AreaInfo[MainBoard.areaIndex].AreaName; // 위치를 못잡음
            int diceTurn = MainBoard.AreaInfo[MainBoard.playerNowLocation].Owner;
            /*if (MainBoard.P2c == true)
            {
                if (MainBoard.diceTurn == 1) diceTurn = 2;
                else diceTurn = 1;
            }
            else if (MainBoard.P3c == true)
            {
                if (MainBoard.diceTurn == 1) diceTurn = 2;
                else if (MainBoard.diceTurn == 2) diceTurn = 3;
                else diceTurn = 1;
            }
            else if (MainBoard.P4c == true)
            {
                if (MainBoard.diceTurn == 1) diceTurn = 2;
                else if (MainBoard.diceTurn == 2) diceTurn = 3;
                else if (MainBoard.diceTurn == 3) diceTurn = 4;
                else diceTurn = 1;
            } */
            int nowMoney = 0;
            int TollPrice = 0;
            idx = MainBoard.PlayerInfo[diceTurn].AreaIndex.IndexOf(MainBoard.areaIndex);
            if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "별장")
            {
                nowMoney = MainBoard.PlayerInfo[MainBoard.diceTurn].Money - MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[0];
                TollPrice = MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[0];
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "빌딩")
            {
                nowMoney = MainBoard.PlayerInfo[MainBoard.diceTurn].Money - MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[0];
                TollPrice = MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[0];
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "호텔")
            {
                nowMoney = MainBoard.PlayerInfo[MainBoard.diceTurn].Money - MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[2];
                TollPrice = MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[2];
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "랜드마크")
            {
                nowMoney = MainBoard.PlayerInfo[MainBoard.diceTurn].Money - MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[3];
                TollPrice = MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[3];
            }
            lbPayMoney.Text = string.Format("통행료 : {0:N0}원", TollPrice);
            lbNowMoney.Text = string.Format("지불후 금액 : {0:N0}원", nowMoney);
        }

        public void TollGatePay()
        {
            int diceTurn = MainBoard.AreaInfo[MainBoard.playerNowLocation].Owner;
            idx = MainBoard.PlayerInfo[diceTurn].AreaIndex.IndexOf(MainBoard.areaIndex);
            if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "별장")
            {
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[0];
                MainBoard.PlayerInfo[diceTurn].Money += MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[0];
                this.Close();
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "빌딩")
            {
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[1];
                MainBoard.PlayerInfo[diceTurn].Money += MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[1];
                this.Close();
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "호텔")
            {
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[2];
                MainBoard.PlayerInfo[diceTurn].Money += MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[2];
                this.Close();
            }
            else if (MainBoard.PlayerInfo[diceTurn].BuildName[idx] == "랜드마크")
            {
                MainBoard.PlayerInfo[MainBoard.diceTurn].Money -= MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[3];
                MainBoard.PlayerInfo[diceTurn].Money += MainBoard.AreaInfo[MainBoard.areaIndex].TollPrice[3];
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TollGatePay();
        }

        private void btnFree_Click(object sender, EventArgs e)
        {
            MainBoard.PlayerInfo[MainBoard.diceTurn].AngelCard--;

            MessageBox.Show("우대권을 사용하여 무료로 통과합니다.");
            this.Close();
        }

        private void GameTollgate_Load(object sender, EventArgs e)
        {

        }
    }
}