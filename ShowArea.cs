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
    public partial class ShowArea : Form
    {
        public ShowArea()
        {
            InitializeComponent();
            ListViewSet();
        }
        public void ListViewSet()
        {
            for (int i = 0; i < MainBoard.PlayerInfo[MainBoard.showAreaTurn].AreaIndex.Count; i++)
            {
                int AreaIndex = MainBoard.PlayerInfo[MainBoard.showAreaTurn].AreaIndex[i];
                string BuildName = MainBoard.PlayerInfo[MainBoard.showAreaTurn].BuildName[i];
                string AreaName = MainBoard.AreaInfo[AreaIndex].AreaName;
                int BuildPrice = 0;
                switch (BuildName)
                {
                    case "별장":
                        BuildPrice = MainBoard.AreaInfo[AreaIndex].SellPrice[0];
                        break;
                    case "빌딩":
                        BuildPrice = MainBoard.AreaInfo[AreaIndex].SellPrice[1];
                        break;
                    case "호텔":
                        BuildPrice = MainBoard.AreaInfo[AreaIndex].SellPrice[2];
                        break;
                    case "랜드마크":
                        BuildPrice = MainBoard.AreaInfo[AreaIndex].SellPrice[3];
                        break;
                }
                    listView1.Items.Add(new ListViewItem(new string[] { AreaName, BuildName, BuildPrice.ToString() }));
            }
            lbAngel.Text = string.Format("우대권 개수 : " + MainBoard.PlayerInfo[MainBoard.showAreaTurn].AngelCard + "개");
            lbEscape.Text = string.Format("무인도 탈출권 개수 : " + MainBoard.PlayerInfo[MainBoard.showAreaTurn].EscapeIslandCard + "개");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}