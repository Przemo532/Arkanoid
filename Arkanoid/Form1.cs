using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanoid
{
    public partial class Form1 : Form
    {
        Graphics g;
        Rectangle Ball;
        Rectangle Paddle;
        List<Rectangle> Bricks;
        int PlayerSpeed = 15;
        bool right = true, down = true;
        Keys PlayerDirection;
        int score = 0;
        public Form1()
        {
            InitializeComponent();

            //pictureBox1.Image = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            
            //Graphics g = Graphics.FromImage(pictureBox1.Image);
            
            //g.Clear(Color.Green);
            //g.DrawEllipse(new Pen(Color.Yellow, 5), 50, 50, 100, 150);
            //g.FillEllipse(new SolidBrush(Color.Yellow), 10, 10, 50, 50);
            //pictureBox1.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {

                //Text += "L";
                PlayerDirection = Keys.Left;
                //Graphics g = Graphics.FromImage(pictureBox1.Image);
                //g.Clear(Color.Blue);
                //g.DrawEllipse(new Pen(Color.Red, 5), 50, 50, 100, 150);
                //g.FillEllipse(new SolidBrush(Color.Red), 10, 10, 50, 50);
                //pictureBox1.Refresh();
            }
            else if (e.KeyCode == Keys.Right)
            {
                //Text += "R";
                PlayerDirection = Keys.Right;
                //Paddle.X+=PlayerSpeed;
            }
            else if (!timer1.Enabled)
            {
                if (e.KeyCode == Keys.T)
                {
                    Reload();
                }
                if (e.KeyCode == Keys.N)
                {
                    this.Close();
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && PlayerDirection == Keys.Left)
            {
                PlayerDirection = Keys.None;
            }
            if (e.KeyCode == Keys.Right && PlayerDirection == Keys.Right)
            {
                PlayerDirection = Keys.None;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PlayerDirection == Keys.Left)
            {
                Paddle.X -= PlayerSpeed;
            }
            if (PlayerDirection == Keys.Right)
            {
                Paddle.X += PlayerSpeed;
            }
            
            
            //else if (Ball.Y >= Paddle.Y - (Ball.Height) && Ball.X >= Paddle.X-Ball.Width && Ball.X <= Paddle.X + Paddle.Width) down = false;
            if (Ball.Y <= 0) down = true;

            if (Ball.X >= pictureBox1.Width - (Ball.Width)) right = false;
            else if (Ball.X <= 0) right = true;
            else
            {
                Rectangle brickToRemove = Rectangle.Empty;
                foreach(Rectangle brick in Bricks)
                {
                    if (Ball.IntersectsWith(brick))
                    {
                        if (down) down = false;
                        else down = true;
                        brickToRemove = brick;
                        score += 1;
                        break;
                    }
                }
                Bricks.Remove(brickToRemove);
            }

            if (Ball.IntersectsWith(Paddle)) down = false;

            g.Clear(Color.Green);
            g.FillRectangle(new SolidBrush(Color.Pink), Paddle);
            g.DrawRectangle(new Pen(Color.Red), Paddle);

            g.FillEllipse(new SolidBrush(Color.Yellow), Ball);
            g.DrawEllipse(new Pen(Color.Purple, 5), Ball);

            foreach (Rectangle brick in Bricks)
            {
                g.FillRectangle(new SolidBrush(Color.Red),brick);
                g.DrawRectangle(new Pen(Color.Purple), brick);
            }

            string s;
            s = $"Punkty: {score}";
            g.DrawString(s, new Font("Comic Sans", 30F), new SolidBrush(Color.Wheat),0,0);

            if (right) Ball.X += 15;
            else Ball.X -= 15;

            if (down) Ball.Y += 25;
            else Ball.Y -= 25;

            //Text += "!";
            string info = "";
            if (Ball.Y >= pictureBox1.Height - (Ball.Height))
            {
                timer1.Stop();
                info = "Przegrana!\nCzy chcesz zagrać jeszcze raz?  (T/N)";
                //MessageBox.Show("Przegrana!");
                //down = false;
            }
            else if (Bricks.Count == 0)
            {
                timer1.Stop();
                info = "Wygrana!\nCzy chcesz zagrać jeszcze raz? (T/N)";
                //MessageBox.Show("Wygrana!");
            }
            g.DrawString(info, new Font("Comic Sans", 30F), new SolidBrush(Color.Wheat), pictureBox1.Width / 2, pictureBox1.Height / 2);


            pictureBox1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);

            Reload();
        }

        private void Reload()
        {           
            //g.Clear(Color.Green);
            //g.DrawEllipse(new Pen(Color.Yellow, 5), 50, 50, 100, 150);
            //g.FillEllipse(new SolidBrush(Color.Yellow), x, y, 50, 50);
            //pictureBox1.Refresh();
            Ball = new Rectangle((pictureBox1.Width / 2) - 30, (pictureBox1.Height - 400), 30, 30);
            Paddle = new Rectangle((pictureBox1.Width / 2) - 100, (pictureBox1.Height - 50), 200, 25);

            Bricks = new List<Rectangle>();
            {
                int xMax = 15;
                int yMax = 5;
                for (int x = 0; x < xMax; ++x)
                {
                    for (int y = 0; y < yMax; ++y)
                    {
                        Bricks.Add(new Rectangle((pictureBox1.Width / xMax * x),
                            ((int)(pictureBox1.Height * 0.05) * y) + (pictureBox1.Height / 10),
                            (pictureBox1.Width / xMax),
                            (int)(pictureBox1.Height * 0.05)));
                    }
                }
            }
            score = 0;
            timer1.Start();
        }
    }
}
