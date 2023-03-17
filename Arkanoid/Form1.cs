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
        bool right = true, down = true;
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
                Text += "L";

                //Graphics g = Graphics.FromImage(pictureBox1.Image);
                //g.Clear(Color.Blue);
                //g.DrawEllipse(new Pen(Color.Red, 5), 50, 50, 100, 150);
                //g.FillEllipse(new SolidBrush(Color.Red), 10, 10, 50, 50);
                //pictureBox1.Refresh();
            }
            if (e.KeyCode == Keys.Right)
            {
                Text += "R";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //Text += "!";
            if (Ball.Y >= pictureBox1.Height - (Ball.Height))
            {
                timer1.Stop();
                MessageBox.Show("Przegrana!");
                down = false;
            }
            else if (Ball.Y <= 0) down = true;

            if (Ball.X >= pictureBox1.Width - (Ball.Width)) right = false;
            else if (Ball.X <= 0) right = true;


            g.Clear(Color.Green);
            g.FillEllipse(new SolidBrush(Color.Yellow), Ball);
            g.DrawEllipse(new Pen(Color.Purple, 5), Ball);

            if (right) Ball.X += 15;
            else Ball.X -= 15;

            if (down) Ball.Y += 25;
            else Ball.Y -= 25;

            

            pictureBox1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);
            //g.Clear(Color.Green);
            //g.DrawEllipse(new Pen(Color.Yellow, 5), 50, 50, 100, 150);
            //g.FillEllipse(new SolidBrush(Color.Yellow), x, y, 50, 50);
            //pictureBox1.Refresh();
            Ball = new Rectangle(100, 100, 100, 100);
        }
    }
}
