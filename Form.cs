using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color Black = Color.White;

            Pen Pen = new Pen(Black);

            Pen.StartCap = LineCap.Round;
            Pen.EndCap = LineCap.Round;
            Pen.Width = 5;

            e.Graphics.DrawLine(Pen, 250, 170, 700, 170);
            e.Graphics.DrawLine(Pen, 250, 300, 700, 300);


            e.Graphics.DrawLine(Pen, 380, 70, 380, 400);
            e.Graphics.DrawLine(Pen, 530, 70, 530, 400);

        }

        enPlayer PlayerTurn = enPlayer.Player1;
        enum enPlayer
        {
            Player1,
            Player2
        }

        enum enWinner
        {
            //None,// just to became the default value of the enum and work logically with game because at first no one is win
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;

        }

        stGameStatus GameStatus;

        void EndGame()
        {
            lblPlayerTurn.Text = "Game Over";
            // Methode 1
            //lblProgress.Text = GameStatus.Winner.ToString();

            // Methode 2
            switch (GameStatus.Winner)
            {

                case enWinner.Player1:

                    lblProgress.Text = "Player1";
                    break;

                case enWinner.Player2:

                    lblProgress.Text = "Player2";
                    break;

                case enWinner.Draw:

                    lblProgress.Text = "Draw";
                    break;

            }

            MessageBox.Show($"Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        bool CheckValues(PictureBox pic1, PictureBox pic2, PictureBox pic3)
        {

            if (pic1.Tag.ToString() != "?" && pic1.Tag == pic2.Tag && pic2.Tag == pic3.Tag)
            {
                pic1.BackColor = Color.GreenYellow;
                pic2.BackColor = Color.GreenYellow;
                pic3.BackColor = Color.GreenYellow;

                if (pic1.Tag.ToString() == "X")
                    GameStatus.Winner = enWinner.Player1;
                else
                    GameStatus.Winner = enWinner.Player2;


                GameStatus.GameOver = true;
                EndGame();

                return true;
            }

            //GameStatus.GameOver = false;// not nesseccery becasue GameOver by dafault are false
            return false;
        }

        void CheckWinner()
        {
            if (CheckValues(pictureBox1, pictureBox2, pictureBox3))
                return;
            if (CheckValues(pictureBox4, pictureBox5, pictureBox6))
                return;
            if (CheckValues(pictureBox7, pictureBox8, pictureBox9))
                return;


            if (CheckValues(pictureBox1, pictureBox4, pictureBox7))
                return;
            if (CheckValues(pictureBox2, pictureBox5, pictureBox8))
                return;
            if (CheckValues(pictureBox3, pictureBox6, pictureBox9))
                return;


            if (CheckValues(pictureBox1, pictureBox5, pictureBox9))
                return;
            if (CheckValues(pictureBox3, pictureBox5, pictureBox7))
                return;

        }

        public void ChangeImage(PictureBox pic)
        {
            if (GameStatus.GameOver)
                return;
            if (pic.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        pic.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        lblPlayerTurn.Text = PlayerTurn.ToString();
                        GameStatus.PlayCount++;
                        pic.Tag = "X";
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        pic.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lblPlayerTurn.Text = PlayerTurn.ToString();
                        GameStatus.PlayCount++;
                        pic.Tag = "O";
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (GameStatus.PlayCount == 9 && !GameStatus.GameOver)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox1);
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox2);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox5);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox6);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox7);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox8);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            ChangeImage(pictureBox9);
        }

        void ResetPictureBox(PictureBox pictureBox)
        {
            pictureBox.Image = Resources.question_mark_96;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Tag = "?";
        }

        void RestartGame()
        {
            ResetPictureBox(pictureBox1);
            ResetPictureBox(pictureBox2);
            ResetPictureBox(pictureBox3);
            ResetPictureBox(pictureBox4);
            ResetPictureBox(pictureBox5);
            ResetPictureBox(pictureBox6);
            ResetPictureBox(pictureBox7);
            ResetPictureBox(pictureBox8);
            ResetPictureBox(pictureBox9);

            GameStatus.GameOver = false;
            GameStatus.PlayCount = 0;

            PlayerTurn = enPlayer.Player1;
            lblPlayerTurn.Text = PlayerTurn.ToString();
            GameStatus.Winner = enWinner.GameInProgress;

            //Method 1
            //lblProgress.Text = GameStatus.Winner.ToString(); // Not Nessecery 

            //Method 2
            lblProgress.Text = "In Progress";


        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

    }
}

/* void CheckWinner(Button btn){...}
         btn send default by ref 
 */