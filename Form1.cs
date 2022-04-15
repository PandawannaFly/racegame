using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R_games_csharp
{
    public partial class Form1 : Form
    {
        int roadSpeed;
        int traffricSpeed;
        int playerSpeed = 12;
        int score;
        int carImage;

        Random rand=new Random(); // khoi tao bien so ngau nhien
        Random carPosition = new Random(); 

        bool goleft ,goright;

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }
        //cac funtion de chay game
        private void keyisDown(object sender, KeyEventArgs e) // khi phim dc nhan 
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        private void keyisUP(object sender, KeyEventArgs e) //khi phim dc tha 
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
        }

        private void timerEvent(object sender, EventArgs e)
        {
            //funtion cong diem
            txtScore.Text = "Score" + score;
            score++;



            //gioi han vung di chuyen cua player
            if (goleft == true && player.Left > 7)
            {
                player.Left -= playerSpeed;
            }
            if (goright == true && player.Left < 295)
            {
                player.Left += playerSpeed;
            }
            //
            roadTrack1.Top += roadSpeed;
            roadTrack2.Top += roadSpeed;
            //tao vong lap duong chay
            if (roadTrack2.Top > 519)
            {
                roadTrack2.Top = -519; // quay tro lai location top -519
            }
            if (roadTrack1.Top > 519)
            {
                roadTrack1.Top = -519; // quay tro lai location top -519
            }
            //xac dinh vi tri top va them object moi 20s
            AI1.Top += traffricSpeed;
            AI2.Top += traffricSpeed;
            
            if (AI1.Top > 530) //ngay khi cham day hinh se quay tro lai ngay ben tren
            {
                changeAIcars(AI1);
            }
            if (AI2.Top > 530) //ngay khi cham day hinh se quay tro lai ngay ben tren
            {
                changeAIcars(AI2);
            }
            //
            if (player.Bounds.IntersectsWith(AI1.Bounds)|| player.Bounds.IntersectsWith(AI2.Bounds)) //xet va cham giua xe player va xe AI chi can 1 trong 2 true la het game
            {
                gameOver();
            }

            //xet diem so va tao do kho game

            if (score >500 && score < 1000)
            {
                Award.Image = Properties.Resources.bronze;
            }
            if (score > 1000 && score < 10000)
            {
                Award.Image = Properties.Resources.silver;
                roadSpeed = 20; // tang toc do tro choi khi dat muc bac rach
                traffricSpeed = 22; 

            }
            if (score > 10000)
            {
                Award.Image = Properties.Resources.gold;
                roadSpeed = 30; // tang toc do tro choi khi dat muc pro
                traffricSpeed = 32;
            }

        }
        private void changeAIcars(PictureBox tempCar) //tim kiem mau xe va thay doi ngau nhien.
        {
            carImage = rand.Next(1, 8); //khoi tao bien voi cac mau xe tu 1 den 8 
                switch (carImage) 
            {
                case 1:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.CarRed;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
            }

            tempCar.Top = carPosition.Next(100, 400)* -1;

            if ((string)tempCar.Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(5, 200);
            }

            if ((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(245, 422);
            }
        }
        private void gameOver()
        {
            playSound();
            gameTimer.Stop();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5); // vi tri xuat hien vu no 
            explosion.BackColor = Color.Transparent;//background vu no trong suot

            Award.Visible = true;
            Award.BringToFront();
            btnStart.Enabled = true;
        }
        private void resetGame()
        {
            btnStart.Enabled = false;
            explosion.Visible = false;
            Award.Visible = false;
            goleft = false;
            goright=false;
            score = 0;
            Award.Image = Properties.Resources.bronze;
            roadSpeed = 12;
            traffricSpeed = 15;

            AI1.Top = carPosition.Next(200, 500) * -1; //vi tri cua xe 1 di chuyen ngau nhien trong khoang cho phep
            AI1.Left = carPosition.Next(5, 200);

            AI2.Top = carPosition.Next(200, 500) * -1; //vi tri cua xe 2 di chuyen ngau nhien trong khoang cho phep
            AI2.Left = carPosition.Next(245,422);

            gameTimer.Start();


        }

        private void restartGame(object sender, EventArgs e)
        {
            resetGame();
        }

        private void playSound()
        {
            System.Media.SoundPlayer playerCrash = new System.Media.SoundPlayer(Properties.Resources.hit);
            playerCrash.Play();
        }

    }
}
