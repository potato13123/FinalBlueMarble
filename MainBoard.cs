using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueMarbleProject
{
    public struct AreaInfomation // 각 지역별 필요한 정보 모음
    {
        public string AreaName; // 지역의 이름
        public int[] BuildPrice; // 건물 가격
        public int[] TollPrice; // 통행료 가격
        public int[] SellPrice; // 처분 가격
        public int GiveToSocial; // 사회 복지 기부금 가격
        public int Owner; // 현재 땅의 주인이 누구인지?

        public AreaInfomation(string AreaName, int[] BuildPrice, int[] TollPrice, int[] SellPrice, int GiveToSocial, int Owner) // 정보값 가져오기
        {
            this.AreaName = AreaName;
            this.BuildPrice = BuildPrice;
            this.TollPrice = TollPrice;
            this.SellPrice = SellPrice;
            this.GiveToSocial = GiveToSocial;
            this.Owner = Owner;   
        }
    }

    public struct GoldKey // 골드 키의 정보를 담는 구조체 선언
    {
        public string cardName; // 무슨 카드인지?
        public string cardExplain; // 카드 효과가 무엇인지?
    }

    public struct PlayerInfomation
    {
        public int Money; // 현재 보유 금액
        public int Location; // 플레이어 현재 위치
        public int EscapeIslandCnt; // 무인도를 나가기 위한 남은 턴의 숫자(0 ~ 3턴)
        public int AngelCard; // 천사카드(우대권) 개수
        public int EscapeIslandCard; // 무인도 탈출권 개수
        public int WorldTourCheck; // 세계여행중인지 아닌지 여부
        public List<int> AreaIndex; // 소유한 지역의 위치 인덱스
        public List<string> BuildName; // 소유한 지역 건물 이름
    }
    public partial class MainBoard : Form
    {
        public static PictureBox[] Area = new PictureBox[35]; // 픽쳐박스로 만든 각 지역을 지칭하는 배열 선언, 35 = 총 36칸
        public static PictureBox[] Build = new PictureBox[20]; // 각 건물을 지을 위치를 저장하는 픽쳐박스
        private PictureBox[] P1 = new PictureBox[35]; // 1플레이어를 나타내는 픽쳐박스
        private PictureBox[] P2 = new PictureBox[35]; // 2플레이어를 나타내는 픽쳐박스
        private PictureBox[] P3 = new PictureBox[35]; // 3플레이어를 나타내는 픽쳐박스
        private PictureBox[] P4 = new PictureBox[35]; // 4플레이어를 나타내는 픽쳐박스

        public static AreaInfomation[] AreaInfo = new AreaInfomation[36]; // 각 지역 정보 가져오기

        public GoldKey[] goldKeys = new GoldKey[8];

        public static PlayerInfomation[] PlayerInfo = new PlayerInfomation[5]; // 1-4P, 눈에 익기 쉽게 0 인덱스는 사용x, 팀 설정에 따라 바뀜

        public static int areaIndex = 0; // 해당 플레이어가 어느 지역에 위치해 있는지
        public static int diceTurn = 1; // 1-4까지 돌아가는 턴을 지정하는 변수
        public int diceSum = 0; // 주사위의 합
        public int doubleNum = 0; // 더블의 횟수를 카운트하는 변수 3번 나오면 무인도

        public int playerLastLocation; // 주사위를 던진 후 가게될 위치
        public static int playerNowLocation; // 주사위를 던지기 전 현재 있는 위치

        public int diceRoll1 = 0; // 왼쪽 주사위의 값
        public int diceRoll2 = 0; // 오른쪽 주사위의 값
        public int goldKeyIndex = 0; // 열쇠 효과의 고유 번호
        public int cnt = 0; // 턴마다 주사위의 숫자를 초기화 할 변수
        public int payFundCoin = 0; // 사회복지기금의 모금된 액수를 저장하는 변수
        public static int showAreaTurn = 0; // 보유 현황 확인 플레이어 번호

        // 인원수 인자값
        public static bool P2c; // 게임모드 값 전역변수로 받아내기
        public static bool P3c;
        public static bool P4c;
        // 모드 인자값
        public static bool Teammode;


        public MainBoard(bool P2BtnClicked, bool P3BtnClicked, bool P4BtnClicked, bool Team_BtnClicked, bool Solo_BtnClicked)
        {

            P2c = P2BtnClicked; // 게임 모드 선택 칸에서 받은 인자값 전역 변수로 옮기기
            P3c = P3BtnClicked;
            P4c = P4BtnClicked;
            // 팀 모드 인자값 전역변수로 만들기
            Teammode = Team_BtnClicked;

            InitializeComponent();
            GameSetting(P2BtnClicked, P3BtnClicked, P4BtnClicked, Team_BtnClicked, Solo_BtnClicked);
            AreaInfoSet();
            GoldKeySet();
            timer1.Start();
        }

        private void MainBoard_FormClosing(object sender, FormClosingEventArgs e) // hide
        {
            Application.Exit();
        }
        public void GameSetting(bool P2BtnClicked, bool P3BtnClicked, bool P4BtnClicked, bool Team_BtnClicked, bool Solo_BtnClicked) // 게임 세팅
        {
            P1_CurMoney.Text = "Player1 : 5,000,000원"; // 초기 금액 초기화
            P2_CurMoney.Text = "Player2 : 5,000,000원";
            P3_CurMoney.Text = "Player3 : 5,000,000원";
            P4_CurMoney.Text = "Player4 : 5,000,000원";
            CurDiceTurn.Text = "현재 턴: Player 1"; // 초기 턴 순서 초기화
            CurDiceTurn.ForeColor = Color.Red;

            if (P2c == true)
            {
                P3_CurMoney.Visible = false;
                P4_CurMoney.Visible = false;
                P3_CurAsset.Enabled = false; // 보유현황 버튼 비활
                P3_CurAsset.Visible = false; // 가리기
                P4_CurAsset.Enabled = false;
                P4_CurAsset.Visible = false;
            }
            if (P3c == true)
            {
                P4_CurMoney.Visible = false;
                P4_CurAsset.Enabled = false;
                P4_CurAsset.Visible = false;
            }


            Area = new PictureBox[] // 지역 초기화
            {
                Area1,Area2,Area3,Area4,Area5,Area6,Area7,Area8,Area9,Area10,
                Area11,Area12,Area13,Area14,Area15,Area16,Area17,Area18,Area19,Area20,
                Area21,Area22,Area23,Area24,Area25,Area26,Area27,Area28,Area29,Area30,
                Area31,Area32,Area33,Area34,Area35,Area36
            };

            P1 = new PictureBox[] // P1 위치 초기화
            {
                P1_Location_1, P1_Location_2, P1_Location_3, P1_Location_4, P1_Location_5, P1_Location_6, P1_Location_7, P1_Location_8, P1_Location_9, P1_Location_10,
                P1_Location_11, P1_Location_12, P1_Location_13, P1_Location_14, P1_Location_15, P1_Location_16, P1_Location_17, P1_Location_18, P1_Location_19, P1_Location_20,
                P1_Location_21, P1_Location_22, P1_Location_23, P1_Location_24, P1_Location_25, P1_Location_26, P1_Location_27, P1_Location_28, P1_Location_29, P1_Location_30,
                P1_Location_31, P1_Location_32, P1_Location_33, P1_Location_34, P1_Location_35, P1_Location_36
            };
            P2 = new PictureBox[] // P2 위치 초기화
            {
                P2_Location_1, P2_Location_2, P2_Location_3, P2_Location_4, P2_Location_5, P2_Location_6, P2_Location_7, P2_Location_8, P2_Location_9, P2_Location_10,
                P2_Location_11, P2_Location_12, P2_Location_13, P2_Location_14, P2_Location_15, P2_Location_16, P2_Location_17, P2_Location_18, P2_Location_19, P2_Location_20,
                P2_Location_21, P2_Location_22, P2_Location_23, P2_Location_24, P2_Location_25, P2_Location_26, P2_Location_27, P2_Location_28, P2_Location_29, P2_Location_30,
                P2_Location_31, P2_Location_32, P2_Location_33, P2_Location_34, P2_Location_35, P2_Location_36
            };
            P3 = new PictureBox[] // P3 위치 초기화
            {
                P3_Location_1, P3_Location_2, P3_Location_3,P3_Location_4, P3_Location_5, P3_Location_6, P3_Location_7, P3_Location_8, P3_Location_9, P3_Location_10,
                P3_Location_11, P3_Location_12, P3_Location_13, P3_Location_14, P3_Location_15, P3_Location_16, P3_Location_17, P3_Location_18, P3_Location_19, P3_Location_20,
                P3_Location_21, P3_Location_22, P3_Location_23, P3_Location_24, P3_Location_25, P3_Location_26, P3_Location_27, P3_Location_28, P3_Location_29, P3_Location_30,
                P3_Location_31, P3_Location_32, P3_Location_33, P3_Location_34, P3_Location_35, P3_Location_36
            };
            P4 = new PictureBox[] // P4 위치 초기화
            {
                P4_Location_1, P4_Location_2, P4_Location_3, P4_Location_4, P4_Location_5, P4_Location_6, P4_Location_7, P4_Location_8, P4_Location_9, P4_Location_10,
                P4_Location_11, P4_Location_12, P4_Location_13, P4_Location_14, P4_Location_15, P4_Location_16, P4_Location_17, P4_Location_18, P4_Location_19, P4_Location_20,
                P4_Location_21, P4_Location_22, P4_Location_23, P4_Location_24, P4_Location_25, P4_Location_26, P4_Location_27, P4_Location_28, P4_Location_29, P4_Location_30,
                P4_Location_31, P4_Location_32, P4_Location_33, P4_Location_34, P4_Location_35, P4_Location_36
            };
            Build = new PictureBox[] // 건물 위치 초기화
            {
                BuildArea1, BuildArea2, BuildArea3, BuildArea4, BuildArea5, BuildArea6, BuildArea7, BuildArea8, BuildArea9, BuildArea10,
                BuildArea11, BuildArea12, BuildArea13, BuildArea14, BuildArea15, BuildArea16, BuildArea17, BuildArea18, BuildArea19, BuildArea20,
                BuildArea21
            };

            P1[0].Visible = true; // 시작 지점의 1플레이어 보이게 하기
            P2[0].Visible = true; // 시작 지점의 2플레이어 보이게 하기

            PlayerInfo[1].Money = 300000; // 1 플레이어의 초기 자금 설정
            PlayerInfo[1].AreaIndex = new List<int>(); // 1 플레이어의 위치값
            PlayerInfo[1].BuildName = new List<string>(); // 1플레이어의 소유 건물

            PlayerInfo[2].Money = 300000; // 2 플레이어의 초기 자금 설정
            PlayerInfo[2].AreaIndex = new List<int>(); // 2 플레이어의 위치값
            PlayerInfo[2].BuildName = new List<string>(); // 2플레이어의 소유 건물
            if (P3BtnClicked || P4BtnClicked) // 만약 3이나 4인으로 체크했다면
            {
                P3[0].Visible = true; // 시작 지점의 3플레이어 보이게 하기
                PlayerInfo[3].Money = 300000; // 3 플레이어의 초기 자금 설정
                PlayerInfo[3].AreaIndex = new List<int>(); // 3 플레이어의 위치값
                PlayerInfo[3].BuildName = new List<string>(); // 3 플레이어의 소유 건물
            }
            if (P4BtnClicked) // 만약 4인으로 체크했다면
            {
                P4[0].Visible = true; // 시작 지점의 4플레이어 보이게 하기
                PlayerInfo[4].Money = 300000; // 4 플레이어의 초기 자금 설정
                PlayerInfo[4].AreaIndex = new List<int>(); // 4 플레이어의 위치값
                PlayerInfo[4].BuildName = new List<string>(); // 4 플레이어의 소유 건물
            }
        }

        public void AreaInfoSet() // 각 지역 정보 초기화
        {
            int[] price = { 50000, 150000, 250000, 350000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            int[] price2 = { 10000, 90000, 250000, 350000 }; // 지불 비용
            int[] price3 = { 25000, 75000, 125000, 175000 }; // 매각 비용
            AreaInfo[1] = new AreaInfomation("타이베이", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 50000, 150000, 250000, 400000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 20000, 180000, 450000, 600000 }; // 지불 비용
            price3 = new int[] { 25000, 75000, 125000, 200000 }; // 매각 비용
            AreaInfo[2] = new AreaInfomation("베이징", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 50000, 150000, 250000, 450000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 20000, 180000, 450000, 600000 }; // 지불 비용
            price3 = new int[] { 25000, 75000, 125000, 225000 }; // 매각 비용
            AreaInfo[3] = new AreaInfomation("마닐라", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0, 0, 0, 200000 }; // 구매 비용
            price2 = new int[] { 0, 0, 0, 300000 }; // 지불 비용
            price3 = new int[] { 0, 0, 0, 100000 }; // 매각 비용
            AreaInfo[4] = new AreaInfomation("제주도", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 50000, 150000, 250000, 500000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 30000, 270000, 550000, 700000 }; // 지불 비용
            price3 = new int[] { 25000, 75000, 125000, 175000 }; // 매각 비용
            AreaInfo[5] = new AreaInfomation("싱가폴", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[6] = new AreaInfomation("황금열쇠", price, price, price, 0, 0);
            price = new int[] { 50000, 150000, 250000, 550000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 30000, 270000, 650000, 750000 }; // 지불 비용
            price3 = new int[] { 25000, 75000, 125000, 225000 }; // 매각 비용
            AreaInfo[7] = new AreaInfomation("카이로", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 75000, 225000, 350000, 700000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 130000, 250000, 550000, 750000 }; // 지불 비용
            price3 = new int[] { 35000, 120000, 175000, 35000 }; // 매각 비용
            AreaInfo[8] = new AreaInfomation("이스탄불", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[9] = new AreaInfomation("무인도", price, price, price, 0, 0);
            price = new int[] { 100000, 300000, 500000, 750000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 150000, 450000, 750000, 9500000 }; // 지불 비용
            price3 = new int[] { 50000, 150000, 250000, 350000 }; // 매각 비용
            AreaInfo[10] = new AreaInfomation("아테네", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 100000, 300000, 500000, 750000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 180000, 500000, 900000, 1000000 }; // 지불 비용
            price3 = new int[] { 50000, 150000, 250000, 375000 }; // 매각 비용
            AreaInfo[11] = new AreaInfomation("코펜하겐", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 100000, 300000, 500000, 800000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 1800000, 500000, 900000, 1050000 }; // 지불 비용
            price3 = new int[] { 50000, 150000, 250000, 425000 }; // 매각 비용
            AreaInfo[12] = new AreaInfomation("스톡홀름", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0, 0, 0, 200000 }; // 구매 비용
            price2 = new int[] { 0, 0, 0, 300000 }; // 지불 비용
            price3 = new int[] { 0, 0, 0, 100000 }; // 매각 비용
            AreaInfo[13] = new AreaInfomation("콩고드여객기", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 100000, 300000, 500000, 825000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 180000, 500000, 900000, 925000 }; // 지불 비용
            price3 = new int[] { 50000, 150000, 250000, 425000 }; // 매각 비용
            AreaInfo[14] = new AreaInfomation("베른", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 100000, 300000, 500000, 850000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 180000, 500000, 900000, 950000 }; // 지불 비용
            price3 = new int[] { 50000, 150000, 250000, 425000 }; // 매각 비용
            AreaInfo[15] = new AreaInfomation("베를린", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[16] = new AreaInfomation("황금열쇠", price, price, price, 0, 0);
            price = new int[] { 100000, 300000, 500000, 900000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 220000, 600000, 1000000,1150000 }; // 지불 비용
            price3 = new int[] { 50000, 150000, 250000, 450000 }; // 매각 비용
            AreaInfo[17] = new AreaInfomation("오타와", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[18] = new AreaInfomation("사회복지기금 접수처", price, price, price, 0, 0);
            price = new int[] { 125000, 450000, 750000, 1050000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 300000, 750000, 1100000, 1300000 }; // 지불 비용
            price3 = new int[] { 75000, 225000, 375000, 525000 }; // 매각 비용
            AreaInfo[19] = new AreaInfomation("부에노스 아이레스", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[20] = new AreaInfomation("황금열쇠", price, price, price, 0, 0);
            price = new int[] { 150000, 450000, 750000, 1050000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 300000, 750000, 1100000, 1300000 }; // 지불 비용
            price3 = new int[] { 75000, 225000, 375000, 525000 }; // 매각 비용
            AreaInfo[21] = new AreaInfomation("상파울루", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0, 0, 0, 500000 }; // 구매 비용
            price2 = new int[] { 0, 0, 0, 600000 }; // 지불 비용
            price3 = new int[] { 0, 0, 0, 250000 }; // 매각 비용
            AreaInfo[22] = new AreaInfomation("부산", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 150000, 450000, 750000, 1100000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 300000, 750000, 1100000, 1450000 }; // 지불 비용
            price3 = new int[] { 75000, 225000, 375000, 550000 }; // 매각 비용
            AreaInfo[23] = new AreaInfomation("시드니", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 150000, 450000, 750000, 1100000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 300000, 750000, 1100000, 1450000 }; // 지불 비용
            price3 = new int[] { 75000, 225000, 375000, 550000 }; // 매각 비용
            AreaInfo[24] = new AreaInfomation("하와이", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0, 0, 0, 250000 }; // 구매 비용
            price2 = new int[] { 0, 0, 0, 350000 }; // 지불 비용
            price3 = new int[] { 0, 0, 0, 125000 }; // 매각 비용
            AreaInfo[25] = new AreaInfomation("퀸 엘리자베스 호", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 150000, 450000, 750000, 1250000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 390000, 900000, 1300000, 1800000 }; // 지불 비용
            price3 = new int[] { 75000, 225000, 375000, 650000 }; // 매각 비용
            AreaInfo[26] = new AreaInfomation("마드리드", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[27] = new AreaInfomation("세계여행", price, price, price, 0, 0);
            price = new int[] { 200000, 600000, 1000000, 1350000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 450000, 1000000, 1400000, 1900000 }; // 지불 비용
            price3 = new int[] { 10000, 300000, 500000, 675000 }; // 매각 비용
            AreaInfo[28] = new AreaInfomation("도쿄", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0, 0, 0, 450000 }; // 구매 비용
            price2 = new int[] { 0, 0, 0, 550000 }; // 지불 비용
            price3 = new int[] { 0, 0, 0, 225000 }; // 매각 비용
            AreaInfo[29] = new AreaInfomation("콜롬비아 호", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 200000, 600000, 1000000, 1400000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 500000, 1100000, 1500000, 2000000 }; // 지불 비용
            price3 = new int[] { 100000, 300000, 500000, 700000 }; // 매각 비용
            AreaInfo[30] = new AreaInfomation("파리", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[31] = new AreaInfomation("황금열쇠", price, price, price, 0, 0);
            price = new int[] { 200000, 600000, 1000000, 1450000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 550000, 1200000, 1600000, 2100000 }; // 지불 비용
            price3 = new int[] { 100000, 300000, 500000, 725000 }; // 매각 비용
            AreaInfo[32] = new AreaInfomation("로마", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 200000, 600000, 1000000, 1500000 }; // 구매 비용 각각 빌라 빌딩 호텔 랜드마크 순
            price2 = new int[] { 600000, 90000, 1700000, 2200000 }; // 지불 비용
            price3 = new int[] { 100000, 75000, 500000, 750000 }; // 매각 비용
            AreaInfo[33] = new AreaInfomation("런던", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[34] = new AreaInfomation("사회복지기금", price, price, price, 0, 0);
            price = new int[] { 0, 0, 0, 1000000 }; // 구매 비용
            price2 = new int[] { 0, 0, 0, 2000000 }; // 지불 비용
            price3 = new int[] { 0, 0, 0, 500000 }; // 매각 비용
            AreaInfo[35] = new AreaInfomation("서울", price, price2, price3, 0, 0); // 각각 이름, 구매, 지불, 매각, , 땅 주인의 번호
            price = new int[] { 0 };
            AreaInfo[0] = new AreaInfomation("출발지", price, price, price, 0, 0);

        }

        public void GoldKeySet() // 황금 열쇠 정보 초기화
        {
            goldKeys[0].cardName = "무인도 탈출";
            goldKeys[0].cardExplain = "특수 무전기 - (무인도에 갇혀 있을 때 사용할 수 있습니다, 1회 사용 후 반납합니다.)";
            goldKeys[1].cardName = "무인도";
            goldKeys[1].cardExplain = "폭풍을 만났습니다. 무인도로 곧장 가세요. - (출발지를 지나더라도 월급을 받을 수 없습니다.)";
            goldKeys[2].cardName = "관광여행";
            goldKeys[2].cardExplain = "제주도로 가세요 - (제주도 소유주에게 통행료를 지불합니다. 출발지를 지나갈 경우, 월급을 받습니다.)";
            goldKeys[3].cardName = "고속도로";
            goldKeys[3].cardExplain = "출발지까지 곧바로 가세요. - (출발지에서 월급을 받습니다.)";
            goldKeys[4].cardName = "우대권";
            goldKeys[4].cardExplain = "상대방이 소유한 장소에 통행료 없이 머무를 수 있습니다. (1회 사용후, 황금 열쇠함에 반납합니다. 중요한 순간에 쓰세요.)";
            goldKeys[5].cardName = "관광여행";
            goldKeys[5].cardExplain = "(가장비싼도시-부산)으로 가세요. - (부산을 상대방이 가지고 있는 경우, 통행료를 지불합니다)";
            goldKeys[6].cardName = "세계여행 초청장";
            goldKeys[6].cardExplain = "세계여행 초청장이 왔습니다. 세계여행 칸으로 이동하시오. (세계여행은 무료이므로 탑승료를 지불하지 않습니다, 출발지를 지나갈 경우 월급을 받습니다.)";
            goldKeys[7].cardName = "사회복지기금 접수처";
            goldKeys[7].cardExplain = "사회복지기금 기부칸으로 가세요.- (출발지를 지나갈 경우, 월급을 받습니다.)";
        }

        public void GoldKeyAction() // 황금 열쇠 이벤트 처리
        {
            playerNowLocation = PlayerInfo[diceTurn].Location; // diceturn에 해당되는 턴의 유저의 위치를 현재 위치를 나타내는 변수에 집어넣기 

            if (goldKeyIndex == 0) // 무인도 탈출권 이벤트
            {
                MessageBox.Show(string.Format(goldKeys[0].cardName + "\n" + goldKeys[0].cardExplain)); // 카드명과 카드 설명을 메세지 박스로 띄움
                PlayerInfo[diceTurn].EscapeIslandCard++; // 현재 턴의 플레이어에게 무인도 탈출권 하나를 지급
            }
            else if (goldKeyIndex == 1) // 무인도로 이동
            {
                MessageBox.Show(string.Format(goldKeys[1].cardName + "\n" + goldKeys[1].cardExplain));
                playerNowLocation = PlayerInfo[diceTurn].Location; // 현위치 부터
                PlayerInfo[diceTurn].Location = 9; // 무인도 인덱스로 이동
                playerLastLocation = PlayerInfo[diceTurn].Location; // 무인도까지 이동
                diceSum = 0; // 주사위로 이동 못하게
                doubleNum = 0; // 더블 횟수 초기화
                timer2.Start();
            }
            else if (goldKeyIndex == 2) // 제주도 관광 여행
            {
                MessageBox.Show(string.Format(goldKeys[2].cardName + "\n" + goldKeys[2].cardExplain));
                playerNowLocation = PlayerInfo[diceTurn].Location; // 현위치부터
                PlayerInfo[diceTurn].Location = 4; // 제주도 인덱스로 이동
                playerLastLocation = PlayerInfo[diceTurn].Location; //제주도까지 이동
                diceSum = 0; // 이동 금지
                timer2.Start();
            }
            else if (goldKeyIndex == 3) // 출발지로 이동
            {
                MessageBox.Show(string.Format(goldKeys[3].cardName + "\n" + goldKeys[3].cardExplain));
                playerNowLocation = PlayerInfo[diceTurn].Location; // 현위치에서
                PlayerInfo[diceTurn].Location = 0;
                playerLastLocation = PlayerInfo[diceTurn].Location;
                diceSum = 0;
                timer2.Start();
            }
            else if (goldKeyIndex == 4) // 천사카드
            {
                MessageBox.Show(string.Format(goldKeys[4].cardName + "\n" + goldKeys[4].cardExplain));
                PlayerInfo[diceTurn].AngelCard++; // 천사카드 지급 
            }
            else if (goldKeyIndex == 5) // 부산으로 이동
            {
                MessageBox.Show(string.Format(goldKeys[5].cardName + "\n" + goldKeys[5].cardExplain));
                playerNowLocation = PlayerInfo[diceTurn].Location;
                PlayerInfo[diceTurn].Location = 22;
                playerLastLocation = PlayerInfo[diceTurn].Location;
                diceSum = 0;
                timer2.Start();
            }
            else if (goldKeyIndex == 6) // 세계여행칸 이동
            {
                MessageBox.Show(string.Format(goldKeys[6].cardName + "\n" + goldKeys[6].cardExplain));
                playerNowLocation = PlayerInfo[diceTurn].Location;
                PlayerInfo[diceTurn].Location = 27;
                playerLastLocation = PlayerInfo[diceTurn].Location;
                diceSum = 0;
                doubleNum = 0;
                timer2.Start();
            }
            else if (goldKeyIndex == 7) // 사회복지 기금 납부
            {
                MessageBox.Show(string.Format(goldKeys[7].cardName + "\n" + goldKeys[7].cardExplain));
                playerNowLocation = PlayerInfo[diceTurn].Location;
                PlayerInfo[diceTurn].Location = 34;
                playerLastLocation= PlayerInfo[diceTurn].Location;
                diceSum = 0;
                timer2.Start();
            }
        }

        public void WorldTour() // 세계여행 로직
        {
            playerNowLocation = PlayerInfo[diceTurn].Location;
            playerLastLocation = areaIndex;
            Dice_Btn.Enabled = false; // 세계 여행 중 주사위 비활성화
            Dice_Btn.BackColor = Color.Purple; // 버튼 비활성화 색깔 표시
            diceSum = 0; // 주사위 초기화
            timer2.Start();
            PlayerInfo[diceTurn].Location = areaIndex; // 선택한 도시로 이동
            Dice_Btn.Enabled = true; // 다시 주사위 켜기
            Dice_Btn.BackColor = Color.Magenta;
            timer1.Start();
        }


        public void WelFareFund() // 사회 복지 기금 수령 로직
        {
            MessageBox.Show(string.Format("기부금 {0:N0}원을 받습니다.", AreaInfo[18].GiveToSocial)); // 사회복지 기금에 기부된 액수만큼 받는 메시지 박스 출력
            PlayerInfo[diceTurn].Money += AreaInfo[18].GiveToSocial; // 해당 턴의 유저에게 기부금의 액수만큼 더해줌
            AreaInfo[18].GiveToSocial = 0; // 기부금은 0으로 초기화
            payFundCoin = 0;
        }

        public void PayFund() // 사회 복지 기금 기부 로직
        {
            MessageBox.Show("150,000원을 기부합니다.");

            PlayerInfo[diceTurn].Money -= 150000; // 150000원 탈취
            AreaInfo[18].GiveToSocial += 150000; // 수령칸에 빼앗은 돈 만큼 저장 액수증가
            
            /* 
             * 매각 로직
             */
        }

        public bool ToolgateCheck() // 통행 가능한지? 불가능하면 매각 or 파산
        {
            int turnTemp = AreaInfo[playerNowLocation].Owner; // 현재 플레이어가 위치한 지역의 주인이 누구인지?
            int index = PlayerInfo[turnTemp].AreaIndex.IndexOf(areaIndex);
            string buildNameTemp = PlayerInfo[turnTemp].BuildName[index]; // 3-4인전 간 누가 주인인지를 모름
            int areaPrice = 0;
            switch (buildNameTemp)
            {
                case "별장":
                    areaPrice = AreaInfo[areaIndex].TollPrice[0]; // 별장은 0번 areainfo의 인덱스의 값을 통행료로 지불
                    break;
                case "빌딩":
                    areaPrice = AreaInfo[areaIndex].TollPrice[1]; // 별장은 0번 areainfo의 인덱스의 값을 통행료로 지불
                    break;
                case "호텔":
                    areaPrice = AreaInfo[areaIndex].TollPrice[2]; // 별장은 0번 areainfo의 인덱스의 값을 통행료로 지불
                    break;
                case "랜드마크":
                    areaPrice = AreaInfo[areaIndex].TollPrice[3]; // 별장은 0번 areainfo의 인덱스의 값을 통행료로 지불
                    break;
            }
            if (PlayerInfo[diceTurn].Money < areaPrice) // 통행료보다 가진 돈이 적을 경우
            {
                return false; // 지불 불가
            }
            else return true; // 지불
        }

        public bool TakeOverCheck() // 인수 가능한지 체크
        {
            int turnTemp = AreaInfo[playerNowLocation].Owner;
            int index = PlayerInfo[turnTemp].AreaIndex.IndexOf(areaIndex);
            string buildNameTemp = PlayerInfo[turnTemp].BuildName[index];

            if (buildNameTemp == "랜드마크") // 인수 불가
                return false;
            int areaPrice = 0;
            switch (buildNameTemp)
            {
                case "별장":
                    areaPrice = (int)(AreaInfo[areaIndex].BuildPrice[0] * 1.5);
                    break;
                case "빌딩":
                    areaPrice = (int)(AreaInfo[areaIndex].BuildPrice[1] * 1.5);
                    break;
                case "호텔":
                    areaPrice = (int)(AreaInfo[areaIndex].BuildPrice[2] * 1.5);
                    break;
                case "랜드마크":
                    areaPrice = (int)(AreaInfo[areaIndex].BuildPrice[3] * 1.5);
                    break;
            }
            if (PlayerInfo[diceTurn].Money < areaPrice) // 인수 할 돈이 없는 경우
                return false;
            else return true; // 인수 가능한 경우
        }

        public void AreaSpot() // 현재 무슨 건물이 지어져있는지에 따라 각기 다른 건물을 구매할 수 있는 함수
        { // 팀전 시 수정 필요
            if (AreaInfo[areaIndex].Owner == 0) // 소유주가 없을 경우
            {
                Building building = new Building(); // Building.cs 가져오기
                building.ShowDialog(); // building 폼 띄우기
                building.Dispose();
            }
            else if (AreaInfo[areaIndex].Owner == diceTurn) // 도착한 건물이 자기 소유이고
            {
                int index = PlayerInfo[diceTurn].AreaIndex.IndexOf(areaIndex); // 건물 번호 추적
                if (PlayerInfo[diceTurn].BuildName[index] == "랜드마크") // 랜드마크일 경우
                    return; // 창 띄우지 않기
                Building building = new Building();
                building.ShowDialog();
                building.Dispose();
            }
            else // 상대 소유의 칸에 도착할 경우
            {
                if (ToolgateCheck()) // 지불 가능 여부 검사
                {
                    GameTollgate tollgate = new GameTollgate();
                    tollgate.ShowDialog(); // 통행료 폼 띄우기
                    tollgate.Dispose();

                    P1_CurMoney.Text = string.Format("Player : {0:N0}원", PlayerInfo[1].Money);
                    P2_CurMoney.Text = string.Format("Player : {0:N0}원", PlayerInfo[2].Money);
                    P3_CurMoney.Text = string.Format("Player : {0:N0}원", PlayerInfo[3].Money);
                    P4_CurMoney.Text = string.Format("Player : {0:N0}원", PlayerInfo[4].Money);

                    if (TakeOverCheck()) // 인수 가능 여부 검사
                    {
                        TakeOver takeover = new TakeOver(); // 인수 창 띄우기
                        takeover.ShowDialog();
                        takeover.Dispose();
                    }
                }
                else // 지불이 불가능 할 경우
                {
                    GameTollgate tollgate = new GameTollgate();
                    tollgate.ShowDialog(); // 일단 남은 돈 지불
                    tollgate.Dispose();
                    if (PlayerInfo[diceTurn].AreaIndex.Count < 1) // 매각 건물이 없을 경우 즉시 패배 패배 문구 출력
                    {
                        MessageBox.Show(string.Format("GAME OVER!\nPlayer{0} 패배!!", diceTurn));
                        this.Close();
                    }
                    if (PlayerInfo[diceTurn].Money < 0) // 돈이 0보다 적을 때
                    {

                    }
                    if (PlayerInfo[diceTurn].Money < 0) // 매각 후 한번 더 검사 했는데 돈이 0보다 적을 경우 패배 처리
                    {
                        MessageBox.Show(string.Format("GAME OVER!\nPlayer{0} 패배!!", diceTurn));
                        this.Close();
                    }
                }
            }
            P1_CurMoney.Text = string.Format("Player : {0:N0}원", PlayerInfo[1].Money);
            P2_CurMoney.Text = string.Format("Player : {0:N0}원", PlayerInfo[2].Money);
            P3_CurMoney.Text = string.Format("Player : {0:N0}원", PlayerInfo[3].Money);
            P4_CurMoney.Text = string.Format("Player : {0:N0}원", PlayerInfo[4].Money);

        }
        public bool WorldTourCheck() // 월드 투어 여부 체크
        {
            if (PlayerInfo[diceTurn].WorldTourCheck == 0) return false; // 세계 여행이 아닐 때
            else // 세계 여행을 할 때
            {
                timer1.Stop();
                PlayerInfo[diceTurn].WorldTourCheck = 0;
                MessageBox.Show("이동 할 지역을 선택해주세요.");
                Dice_Btn.Enabled = false;
                Dice_Btn.BackColor = Color.Purple;
                return true;
            }
        }
        public void playerMove() // 이동 함수
        {
            PlayerInfo[diceTurn].Location += diceSum; // 주사위의 합만큼 이동
            PlayerInfo[diceTurn].Location %= 36; // 주사위를 던져 36칸을 넘어가게 될 경우 다시 1부터 시작할 수 있게 함
            
            if (doubleNum == 3) // 더블이 세번 나올 경우
            {
                PlayerInfo[diceTurn].Location = 9; // 무인도로 이동
                doubleNum = 0; // 더블 횟수 초기화
            }

            if (PlayerInfo[diceTurn].Location == 0) // 출발점에 도착할 경우 
            {
                return; // 일단 이 함수를 빠져 나와서 월급주는 함수로 이동
            }
            else if (PlayerInfo[diceTurn].Location == 6 || PlayerInfo[diceTurn].Location == 16 || PlayerInfo[diceTurn].Location == 20 || PlayerInfo[diceTurn].Location == 31)
            {
                Random rand = new Random(); // 난수 생성
                goldKeyIndex = rand.Next(0, 8); // 0부터 7까지 숫자를 뽑음
                GoldKeyAction(); // 랜덤 수에 나온대로 황금 열쇠 이벤트 발생
            }
            else if (PlayerInfo[diceTurn].Location == 9) // 무인도
            {
                PlayerInfo[diceTurn].EscapeIslandCnt = 3; // 무인도에 도착 할 경우 3턴 동안 갇힘
            }
            else if (PlayerInfo[diceTurn].Location == 18) // 기금 수령 칸
            {
                WelFareFund(); // 그에 맞는 로직 수행
            }
            else if (PlayerInfo[diceTurn].Location == 34) // 기금 지불 칸
            {
                PayFund(); // 위와 같음
            }
            else if (PlayerInfo[diceTurn].Location == 27) // 세계 여행 칸
            {
                PlayerInfo[diceTurn].WorldTourCheck++; // 세계 여행 카운트
                timer1.Stop();
                MessageBox.Show("다음 턴에 세계 여행을 이용할 수 있습니다.");
                timer1.Start();
            }
            else // 일반 지역에 걸렸을 때
            {
                areaIndex = PlayerInfo[diceTurn].Location;
                AreaSpot();
            }
            P1_CurMoney.Text = string.Format("Player1 : {0:N0}원", PlayerInfo[1].Money); // 칸을 지나고 난 뒤 플레이어의 액수 변화 저장
            P2_CurMoney.Text = string.Format("Player2 : {0:N0}원", PlayerInfo[2].Money);
            P3_CurMoney.Text = string.Format("Player3 : {0:N0}원", PlayerInfo[3].Money);
            P4_CurMoney.Text = string.Format("Player4 : {0:N0}원", PlayerInfo[4].Money);
        }

        public void UnislandCheck() // 무인도 탈출권 여부 확인
        {
            if (PlayerInfo[diceTurn].EscapeIslandCard > 0 && PlayerInfo[diceTurn].EscapeIslandCnt > 0) // 만약 탈출권이 있고 무인도에 묶인 턴 수가 1이상일 경우
            {
                if (PlayerInfo[diceTurn].EscapeIslandCard > 0 && PlayerInfo[diceTurn].EscapeIslandCnt > 0)
                {
                    PlayerInfo[diceTurn].EscapeIslandCard--; // 무인도 탈출권 사용
                    PlayerInfo[diceTurn].EscapeIslandCnt = 0; // 묶인 턴 0으로 초기화
                    MessageBox.Show(string.Format("무인도 탈출 카드를 사용합니다.\n현재 남은 카드의 개수는" + PlayerInfo[diceTurn].EscapeIslandCard + "개 입니다.")); // 남은 카드 수를 알려주는 메세지 박스 생성
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Dice_Btn_Click(object sender, EventArgs e)
        {
            Dice_Btn.Enabled = false; // 버튼 클릭 후 비활성화 (중복 클릭 방지)
            Dice_Btn.BackColor = Color.Purple; // 눌림 표시

            Random rand = new Random(); // 난수 생성
            diceRoll1 = rand.Next(1, 7); // 1-6까지 랜덤수 생성
            diceRoll2 = rand.Next(1, 7);

            diceSum = diceRoll1 + diceRoll2;

            lbdice1.Text = diceRoll1.ToString(); // 나온 주사위 값을 스트링 형으로 나타냄
            lbdice2.Text = diceRoll2.ToString();

            playerNowLocation = PlayerInfo[diceTurn].Location; // 현재 위치 초기화
            playerLastLocation = (PlayerInfo[diceTurn].Location + diceSum) % 36; // 최종 위치 초기화 (현재 위치 + 주사위의 합 % 전체칸)
            cnt = 0;

            if (PlayerInfo[diceTurn].EscapeIslandCnt > 0) // 아직 무인도에 남아있을 경우
            {
                PlayerInfo[diceTurn].EscapeIslandCnt--; // 묶인 턴수 차감
                cnt = 0;
                timer3.Start();
                cnt = 0;
            }
            else // 무인도가 아닐 경우
            {
                if (diceRoll1 == diceRoll2) // 같은 수가 나올 경우
                {
                    lbAlertDouble.Text = "더블! 주사위를 한 번 더 던지세요!"; // 주사위를 한 번 더 던지는지 여부를 표시해주는 라벨
                    doubleNum++; // 더블 횟수 증가
                    if (doubleNum != 3) // 더블이 3번이 아닐경우
                    {
                        timer3.Start();
                        cnt = 0;
                    }
                    else // 무인도로 이동 (더블 3회)
                    {
                        lbAlertDouble.Text = "";
                        diceSum = 0; // 이동 금지
                        timer3.Start();
                        cnt = 0;
                    }
                }
                else // 더블이 아닐 경우
                {
                    lbAlertDouble.Text = "";
                    timer3.Start();
                    cnt = 0;
                    doubleNum = 0; // 더블 값 초기화
                }
            }
            // 누구 턴인지 알려주는 라벨 변경
            if (Teammode)
            {
                if (diceTurn == 1) CurDiceTurn.ForeColor = Color.Red; // 1p, 3p 턴일 경우 빨간 색으로 변경
                else if (diceTurn == 2) CurDiceTurn.ForeColor = Color.Blue; // 2, 4p 턴일 경우 파란 색으로 변경
                else if (diceTurn == 3) CurDiceTurn.ForeColor = Color.Red;
                else if (diceTurn == 4) CurDiceTurn.ForeColor = Color.Blue;
                CurDiceTurn.Text = ($"현재 턴: Player {diceTurn}");
            }
            else // 개인전일 경우
            {
                if (diceTurn == 1) CurDiceTurn.ForeColor = Color.Red; // 1p 턴일 경우 빨간 색으로 변경
                else if (diceTurn == 2) CurDiceTurn.ForeColor = Color.Blue;
                else if (diceTurn == 3) CurDiceTurn.ForeColor = Color.Yellow;
                else if (diceTurn == 4) CurDiceTurn.ForeColor = Color.Green;
                CurDiceTurn.Text = ($"현재 턴: Player {diceTurn}");
            }
            Dice_Btn.Enabled = true; // 주사위 다시 활성화
            Dice_Btn.BackColor = Color.Magenta; // 활성화 색깔
        }
        // 세계여행간 각 지역 클릭 시 이동되는 함수들

        private void timer1_Tick(object sender, EventArgs e)
        {
            WorldTourCheck(); // 세계 여행 이동 시 사용
            UnislandCheck(); // 무인도에 있고 무인도 탈출 카드가 있는지 체크 
        }

        private void timer2_Tick(object sender, EventArgs e) // 이동 애니메이션 구현
        {

            Dice_Btn.Enabled = false; // 주사위 비활
            Dice_Btn.BackColor = Color.Purple;
            if (playerNowLocation == playerLastLocation) // 아직 이동하지 않았다면
            {
                timer2.Stop();
                int locationTemp = PlayerInfo[diceTurn].Location; // 현 위치값 임시 저장
                playerMove(); // 이동 함수
                if (doubleNum > 0) // 더블일 경우
                {
                    if (PlayerInfo[diceTurn].Location == 27 || PlayerInfo[diceTurn].Location == 10 || locationTemp == 10) // 세계 여행, 무인도 같이 더블이 나와도 한 번 더 던질 수 없는 경우, 혹은 무인도에서 탈출하여 한 번더 못 던질 경우
                    {
                        if (P2c == true) // 만약 2인전일 경우
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else diceTurn = 1; // 아니면 1p턴으로 변경
                        }
                        else if (P3c == true) // 만약 3인전일 경우
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else if (diceTurn == 2) diceTurn = 3; // 2p to 3p
                            else diceTurn = 1; // 3p to 1p
                        }
                        else if (P4c == true)
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else if (diceTurn == 2) diceTurn = 3; // 2p to 3p
                            else if (diceTurn == 3) diceTurn = 4; // 3p to 4p
                            else diceTurn = 1; // 4p to 1p
                        }
                        doubleNum = 0;
                    }
                }
                else // 더블이 아닐 경우
                {
                    if (goldKeyIndex == 0 || goldKeyIndex == 4) // 탈출권 혹은 우대권?
                    { // 2-4인용 시 수정 필요
                        if (P2c == true) // 만약 2인전일 경우
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else diceTurn = 1; // 아니면 1p턴으로 변경
                        }
                        else if (P3c == true) // 만약 3인전일 경우
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else if (diceTurn == 2) diceTurn = 3; // 2p to 3p
                            else diceTurn = 1; // 3p to 1p
                        }
                        else if (P4c == true)
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else if (diceTurn == 2) diceTurn = 3; // 2p to 3p
                            else if (diceTurn == 3) diceTurn = 4; // 3p to 4p
                            else diceTurn = 1; // 4p to 1p
                        }
                    }
                }
                goldKeyIndex = 0; // 황금 열쇠 값 초기화
                if (Teammode)
                {
                    if (diceTurn == 1) CurDiceTurn.ForeColor = Color.Red;
                    else if (diceTurn == 2) CurDiceTurn.ForeColor = Color.Blue;
                    else if (diceTurn == 3) CurDiceTurn.ForeColor = Color.Red;
                    else CurDiceTurn.ForeColor = Color.Blue;
                    CurDiceTurn.Text = ($"현재 턴: Player {diceTurn}");
                }
                else
                {
                    if (diceTurn == 1) CurDiceTurn.ForeColor = Color.Red;
                    else if (diceTurn == 2) CurDiceTurn.ForeColor = Color.Blue;
                    else if (diceTurn == 3) CurDiceTurn.ForeColor = Color.Yellow;
                    else CurDiceTurn.ForeColor = Color.Green;
                    CurDiceTurn.Text = ($"현재 턴: Player {diceTurn}");
                }

                int gameOverCheck = 0; // 초기값 0
                if (diceTurn == 1) gameOverCheck = 2; // 각 턴마다 게임 오버를 체크함
                else if (diceTurn == 2) gameOverCheck = 3;
                else if (diceTurn == 3) gameOverCheck = 4;
                else if (diceTurn == 4) gameOverCheck = 1;
                if (PlayerInfo[gameOverCheck].Money < 0) // 만약 게임 오버를 체크 했는데 가진 돈이 없을 경우
                    return; // 게임 오버

                Dice_Btn.Enabled = true; // 주사위 활성화
                Dice_Btn.BackColor = Color.Magenta;
                return;
            }

            if (diceTurn == 1) P1[playerNowLocation].Visible = false; // 1p 턴일 경우 현재 위치를 숨김
            else if (diceTurn == 2) P2[playerNowLocation].Visible = false; // 2p 턴일 경우 현재 위치를 숨김
            else if (diceTurn == 3) P3[playerNowLocation].Visible = false; // 3p 턴일 경우 현재 위치를 숨김
            else if (diceTurn == 4) P4[playerNowLocation].Visible = false; // 4p 턴일 경우 현재 위치를 숨김

            playerNowLocation = (playerNowLocation + 1) % 36; // 말 이동 후 현재 위치

            if (diceTurn == 1) P1[playerNowLocation].Visible = true;
            else if (diceTurn == 2) P2[playerNowLocation].Visible = true;
            else if (diceTurn == 3) P3[playerNowLocation].Visible = true;
            else if (diceTurn == 4) P4[playerNowLocation].Visible = true;


            if (playerNowLocation == 0) // 출발지점에 도착할 경우
            {
                PlayerInfo[diceTurn].Money += 300000;
                P1_CurMoney.Text = string.Format("Player1 : {0:N0}원", PlayerInfo[1].Money);
                P2_CurMoney.Text = string.Format("Player2 : {0:N0}원", PlayerInfo[2].Money);
                P3_CurMoney.Text = string.Format("Player3 : {0:N0}원", PlayerInfo[3].Money);
                P4_CurMoney.Text = string.Format("Player4 : {0:N0}원", PlayerInfo[4].Money);
                MessageBox.Show("월급 30만원을 지급합니다.");
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Dice_Btn.Enabled = false;
            Dice_Btn.BackColor = Color.Purple;
            if (cnt < 6) // 6번 굴려서 랜덤하게 나오는 것처럼 애니메이션 효과를 줌
            {
                Left_Dice.BackgroundImage = imageList1.Images[cnt];
                Right_Dice.BackgroundImage = imageList1.Images[cnt];
            }
            cnt++;

            if (cnt == 6)
            {
                Left_Dice.BackgroundImage = imageList1.Images[int.Parse(lbdice1.Text) - 1]; // 스트링 형으로 저장된 주사위 값을 인트형으로 변환 후 이미지 리스트의 배열 값에 맞게 -1을 하여 시각적으로 표현
                Right_Dice.BackgroundImage = imageList1.Images[int.Parse(lbdice2.Text) - 1];

                playerNowLocation = PlayerInfo[diceTurn].Location; // 현재 위치 초기화
                playerLastLocation = (PlayerInfo[diceTurn].Location + diceSum) % 36; // 주사위 굴린 후 최종 위치 초기화
            
                if (doubleNum == 3)
                {
                    MessageBox.Show("더블 3번 연속으로 무인도로 이동");

                    playerLastLocation = 9;
                }

                if (PlayerInfo[diceTurn].EscapeIslandCnt > 0) // 무인도에 있을 때
                {
                    if (diceRoll2 == diceRoll1) // 더블이 뜨면
                    {
                        MessageBox.Show("더블! 무인도를 탈출합니다.");
                        timer2.Start(); // 이동
                        PlayerInfo[diceTurn].EscapeIslandCnt = 0; // 무인도 턴 수 초기화
                        doubleNum = 0; // 더블 횟수 초기화
                        if (diceTurn == 1) CurDiceTurn.ForeColor = Color.Red;
                        else if (diceTurn == 2) CurDiceTurn.ForeColor = Color.Blue;
                        else if (diceTurn == 3) CurDiceTurn.ForeColor = Color.Yellow;
                        else CurDiceTurn.ForeColor = Color.Green;
                        CurDiceTurn.Text = ($"현재 턴 : Player {diceTurn}");
                        timer3.Stop();
                        return;
                    }
                    else // 더블 x 탈출 실패
                    { // 턴 넘기기
                        if (P2c == true) // 만약 2인전일 경우
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else diceTurn = 1; // 아니면 1p턴으로 변경
                        }
                        else if (P3c == true) // 만약 3인전일 경우
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else if (diceTurn == 2) diceTurn = 3; // 2p to 3p
                            else diceTurn = 1; // 3p to 1p
                        }
                        else if (P4c == true)
                        {
                            if (diceTurn == 1) diceTurn = 2; // 1p 턴일 경우 2p 턴으로 변경.
                            else if (diceTurn == 2) diceTurn = 3; // 2p to 3p
                            else if (diceTurn == 3) diceTurn = 4; // 3p to 4p
                            else diceTurn = 1; // 4p to 1p
                        }

                        if (Teammode)
                        {
                            if (diceTurn == 1) CurDiceTurn.ForeColor = Color.Red;
                            else if (diceTurn == 2) CurDiceTurn.ForeColor = Color.Blue;
                            else if (diceTurn == 3) CurDiceTurn.ForeColor = Color.Red;
                            else CurDiceTurn.ForeColor = Color.Blue;
                            CurDiceTurn.Text = ($"현재 턴 : Player {diceTurn}");
                        }
                        else
                        {
                            if (diceTurn == 1) CurDiceTurn.ForeColor = Color.Red;
                            else if (diceTurn == 2) CurDiceTurn.ForeColor = Color.Blue;
                            else if (diceTurn == 3) CurDiceTurn.ForeColor = Color.Yellow;
                            else CurDiceTurn.ForeColor = Color.Green;
                            CurDiceTurn.Text = ($"현재 턴 : Player {diceTurn}");
                        }
                        MessageBox.Show("탈출 실패!");
                        Dice_Btn.Enabled = true; // 주사위 활성화
                        Dice_Btn.BackColor = Color.Magenta;
                    }
                }
                else // 무인도가 아닐 경우
                {
                    timer2.Start();
                } 
                timer3.Stop();
                cnt = 0;
            }
        }


        // 각 플레이어의 자산 보유 현황을 띄우는 버튼 2-4인 따라 수정 필요
        private void P1_CurAsset_Click(object sender, EventArgs e)
        {
            showAreaTurn = 1;
            ShowArea showArea = new ShowArea();
            showArea.ShowDialog(); // 보유 현황 창 띄우기
            showArea.Dispose();
        }

        private void P2_CurAsset_Click(object sender, EventArgs e)
        {
            showAreaTurn = 2;
            ShowArea showArea = new ShowArea();
            showArea.ShowDialog(); // 보유 현황 창 띄우기
            showArea.Dispose();
        }

        private void P3_CurAsset_Click(object sender, EventArgs e)
        {
            showAreaTurn = 3;
            ShowArea showArea = new ShowArea();
            showArea.ShowDialog(); // 보유 현황 창 띄우기
            showArea.Dispose();
        }

        private void P4_CurAsset_Click(object sender, EventArgs e)
        {
            showAreaTurn = 4;
            ShowArea showArea = new ShowArea();
            showArea.ShowDialog(); // 보유 현황 창 띄우기
            showArea.Dispose();
        }
        // 세계 여행 중일 때 해당 픽쳐박스를 클릭하면 해당 지역으로 이동하는 함수들
        private void Area1_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27) 
            {
                areaIndex = 0; // 출발지를 나타냄
                WorldTour(); // 그 자리로 이동
            }
        }

        private void Area2_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 1; // 타이베이
                WorldTour();
            }
        }

        private void Area3_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 2; // 베이징
                WorldTour();
            }
        }

        private void Area4_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 3; // 마닐라
                WorldTour();
            }
        }

        private void Area5_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 4; // 제주도
                WorldTour();
            }
        }

        private void Area6_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 5; // 싱가폴
                WorldTour();
            }
        }

        private void Area7_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 6; // 황금열쇠
                WorldTour();
            }
        }

        private void Area8_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 7; // 카이로
                WorldTour();
            }
        }

        private void Area9_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 8; // 이스탄불
                WorldTour();
            }
        }

        private void Area10_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 9; // 무인도
                WorldTour();
            }
        }
        private void Area11_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 10; // 아테네
                WorldTour();
            }
        }

        private void Area12_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 11; // 코펜하겐
                WorldTour();
            }
        }

        private void Area13_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 12; // 스톡홀름
                WorldTour();
            }
        }

        private void Area14_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 13; // 콩고드 여객기
                WorldTour();
            }
        }

        private void Area15_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 14; // 베른
                WorldTour();
            }
        }

        private void Area16_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 15; // 베를린
                WorldTour();
            }
        }

        private void Area17_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 16; // 황금열쇠
                WorldTour();
            }
        }

        private void Area18_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 17; // 몬트리올
                WorldTour();
            }
        }

        private void Area19_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 18; // 사회복지기금 접수처
                WorldTour();
            }
        }

        private void Area20_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 19; // 부에노스 아이레스
                WorldTour();
            }
        }

        private void Area21_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 20; // 황금열쇠
                WorldTour();
            }
        }

        private void Area22_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 21; // 상파울루
                WorldTour();
            }
        }

        private void Area23_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 22; // 부산
                WorldTour();
            }
        }

        private void Area24_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 23; // 시드니
                WorldTour();
            }
        }

        private void Area25_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 24; // 하와이
                WorldTour();
            }
        }

        private void Area26_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 25; // 퀸 엘리자베스 호
                WorldTour();
            }
        }

        private void Area27_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 26; // 마드리드
                WorldTour();
            }
        }

        // area28 << 세계 여행으로는 이동 금지

        private void Area29_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 28; // 도쿄 
                WorldTour();
            }
        }

        private void Area30_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 29; // 콜롬비아 호
                WorldTour();
            }
        }

        private void Area31_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 30; // 파리
                WorldTour();
            }
        }

        private void Area32_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 31; // 황금열쇠
                WorldTour();
            }
        }

        private void Area33_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 32; // 로마
                WorldTour();
            }
        }

        private void Area34_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 33; // 런던
                WorldTour();
            }
        }

        private void Area35_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 34; // 사회복지기금
                WorldTour();
            }
        }

        private void Area36_Click(object sender, EventArgs e)
        {
            if (PlayerInfo[diceTurn].Location == 27)
            {
                areaIndex = 35; // 서울
                WorldTour();
            }
        }
    }
}
