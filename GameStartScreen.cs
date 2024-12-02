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
    public partial class GameStartScreen : Form
    {

        public GameStartScreen()
        {
            InitializeComponent();
        }

        private void Exit_Btn_Click(object sender, EventArgs e) // 나가기 버튼 클릭 시 
        {
            if (MessageBox.Show("종료하시겠습니까?", "게임 종료", MessageBoxButtons.OKCancel) == DialogResult.OK) // OKCancel창이 뜨고
                this.Close(); // Cancel은 이전, OK는 종료
        }
        private void GameStartScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // 메인 폼이 종료될 때 (hide된 된 폼 제거)앱 종료
        }
        private void GameStart_btn_Click(object sender, EventArgs e) // 게임 시작 버튼 클릭 시 
        {
            GameModeScreen GRSS_Form = new GameModeScreen(); // 게임 모드창을 가르키는 객체 생성.
            GRSS_Form.Show(); // 게임 모드 정하는 창을 띄운다.
            this.Hide(); // 초기화면 임시 제거
        }
    }
}
