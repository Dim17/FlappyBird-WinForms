using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Media;
using System.Resources;
using System.Reflection;


namespace FlappyBirdGame
{
   
    public partial class Game : Form
    {
        public Game()
        {
            
            InitializeComponent();
            label1.Visible = false;
            StartGame();
            fl.AI = true;
            button1.Enabled = true;
            button1.Visible = true;
        }

        Flags fl = new Flags(); //все bool переменные
        Pipe a = new Pipe();//
        int step = 3;
        int OriginalX = 30, OriginalY = 56;
        int points;        
        int SetStep = 0;
        int score = 0;
        int diff_Y = 0;
        int i = 0;     
        static int BackStep = 1;

        Graphics gr;
        Image im1 = new Bitmap("Pic\\BackGround.png");
        Image img = new Bitmap(350, 450);
        PictureBox GameOver = new PictureBox();
        Label currentScore = new Label();
        Label currentScoreCount = new Label();
        Label bestScore = new Label();
        Label bestScoreCount = new Label();

        Label NEW = new Label();

        public List<Point> m_points = new List<Point>();

        public void threeangle()
        {
           
                
            if (chicken.Location.X - a.pipe1[0] > chicken.Location.X - a.pipe2[0])
            {
                
                diff_Y = chicken.Location.Y - a.pipe1[1] - 60;
                diff_Y /= 10;
                diff_Y = -diff_Y;
                i++;
                if (i < 11)
                {
                    chicken.Location = new Point(chicken.Location.X, chicken.Location.Y + diff_Y);
                }
                else
                    i = 0;
                
            }
            else
            {
                
                diff_Y = chicken.Location.Y - a.pipe2[1] - 60;
                diff_Y /= 10;
                diff_Y = -diff_Y;
                if (i < 11)
                {
                    chicken.Location = new Point(chicken.Location.X, chicken.Location.Y + diff_Y);
                }
                else
                    i = 0;
                
            }
        }

        public void Die()
        {
            
            fl.AI = false;
            fl.running = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            GameOver.Size = new Size(250, 200);
            GameOver.Location = new Point(40, 125);
            GameOver.BackColor = Color.White;
            GameOver.Image = Image.FromFile("Pic\\GameOver.png");

            currentScore.Text = "Current score:";
            currentScore.Font = new Font("Arial", 10);
            currentScore.ForeColor = Color.Red;
            currentScore.BackColor = Color.White;
            currentScore.Size = new Size(99, 20);
            currentScore.Location = new Point(180, 140);

            this.Controls.Add(this.currentScore);

            currentScoreCount.Size = new Size(60, 25);
            currentScoreCount.Location = new Point(230, 160);
            currentScoreCount.Font = new Font("Arial", 15);
            currentScoreCount.BackColor = Color.White;
            currentScoreCount.ForeColor = Color.Red;
            currentScoreCount.Text = Convert.ToString(points);

            this.Controls.Add(this.currentScoreCount);

            bestScore.Text = "Best score:";
            bestScore.Font = new Font("Arial", 12);
            bestScore.ForeColor = Color.Red;
            bestScore.BackColor = Color.White;
            bestScore.Size = new Size(87, 20);
            bestScore.Location = new Point(192, 185);

            this.Controls.Add(this.bestScore);

            bestScoreCount.Size = new Size(60, 25);
            bestScoreCount.Location = new Point(230, 205);
            bestScoreCount.Font = new Font("Arial", 15);
            bestScoreCount.BackColor = Color.White;
            bestScoreCount.ForeColor = Color.Red;
            ReadAndShowScore();

            this.Controls.Add(this.bestScoreCount);

            
            

            this.Controls.Add(this.GameOver);

            GameOver.Visible = true;
            currentScoreCount.Visible = true;
            currentScore.Visible = true;
            bestScore.Visible = true;
            bestScoreCount.Visible = true;
            //NEW.Visible = true;
            button1.Visible = true;
            button1.Enabled = true;
            

            points = 0;

            chicken.Location = new Point(OriginalX, OriginalY);
            fl.ResetPipes = true;
            a.pipe1.Clear();
            a.pipe2.Clear();
            Image f = Image.FromFile("Pic\\flappy1.gif");
            chicken.Image = f;
        }

        private void ReadAndShowScore()
        {
            using (StreamReader reader = new StreamReader("Score\\Score.txt"))
            {
                score = int.Parse(reader.ReadToEnd());
                reader.Close();
                if (score < int.Parse(label1.Text))
                {
                    using (StreamWriter writer = new StreamWriter("Score\\Score.txt"))
                    {
                        writer.Write(label1.Text);
                        writer.Close();
                    }

                    NEW.Text = "NEW";
                    NEW.Font = new Font("Arial", 10);
                    NEW.ForeColor = Color.RoyalBlue;
                    NEW.BackColor = Color.White;
                    NEW.Size = new Size(40, 20);
                    NEW.Location = new Point(194, 210);

                    this.Controls.Add(this.NEW);

                    NEW.Visible = true;

                    bestScoreCount.Text = Convert.ToString(int.Parse(label1.Text));
                }
                else
                    bestScoreCount.Text = Convert.ToString(score);

                       

                    //return score;
            }

        }

        private void StartGame()
        {
            
            chicken.Location = new Point(OriginalX, OriginalY);
            fl.play = true;
            fl.Death = false;
            fl.AI = false;
            fl.ResetPipes = false;
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;
            Random random = new Random();
            int num = random.Next(40, this.Height - a.PipeDifferentY);
            int num1 = num + a.PipeDifferentY;
           a.pipe1.Clear();
           a.pipe1.Add(this.Width);
           a.pipe1.Add(num);
           a.pipe1.Add(this.Width);
           a.pipe1.Add(num1);

           num = random.Next(40, (this.Height - a.PipeDifferentY));
           num1 = num + a.PipeDifferentY;
           a.pipe2.Clear();
           a.pipe2.Add(this.Width + a.PipeDifferentX);
           a.pipe2.Add(num);
           a.pipe2.Add(this.Width + a.PipeDifferentX);
           a.pipe2.Add(num1);

            button1.Visible = false;
            button1.Enabled = false;
            GameOver.Visible = false;
            currentScoreCount.Visible = false;
            currentScore.Visible = false;
            bestScore.Visible = false;
            bestScoreCount.Visible = false;
            NEW.Visible = false;
            fl.running = true;


            step = 3;

            Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            points = 0;
            label1.Visible = true;
            fl.start = true;
            StartGame();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                gr = Graphics.FromImage(img);
                this.BackgroundImage = img;
                BackStep++;
                gr.DrawImage(im1, new Rectangle(0, 0, 350, 450), new Rectangle(0 + BackStep, 0, 350, 450), GraphicsUnit.Pixel);
                if (BackStep == 350)
                    BackStep = 0;

                if (a.pipe1[0] + a.PipeWidth <= 0)
                {
                    Random rnd = new Random();
                    int px = this.Width;
                    int py = rnd.Next(10, (this.Height - a.PipeDifferentY));
                    var p2x = px;
                    var p2y = py + a.PipeDifferentY;
                    a.pipe1.Clear();
                    a.pipe1.Add(px);
                    a.pipe1.Add(py);
                    a.pipe1.Add(p2x);
                    a.pipe1.Add(p2y);

                }
                else
                {
                    a.pipe1[0] = a.pipe1[0] - 2;
                    a.pipe1[2] = a.pipe1[2] - 2;
                }

                if (a.pipe2[0] + a.PipeWidth <= 0)//| start
                {
                    Random rnd = new Random();
                    int px = this.Width;
                    int py = rnd.Next(10, this.Height - a.PipeDifferentY);
                    var p2x = px;
                    var p2y = py + a.PipeDifferentY;
                    int[] pl = { px, py, p2x, p2y };
                    a.pipe2.Clear();
                    a.pipe2.Add(px);
                    a.pipe2.Add(py);
                    a.pipe2.Add(p2x);
                    a.pipe2.Add(p2y);

                }
                else
                {
                    a.pipe2[0] = a.pipe2[0] - 2;
                    a.pipe2[2] = a.pipe2[2] - 2;
                }

            }
        }

       

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (!fl.ResetPipes && a.pipe1.Any() && a.pipe2.Any())
            {

                e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(a.pipe1[0], 0, a.PipeWidth, a.pipe1[1]));
                e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(a.pipe1[0] - 10, a.pipe1[3] - a.PipeDifferentY, 75, 15));

                e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(a.pipe1[2], a.pipe1[3], a.PipeWidth, this.Height - a.pipe1[3]));
                e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(a.pipe1[2] - 10, a.pipe1[3], 75, 15));

                e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(a.pipe2[0], 0, a.PipeWidth, a.pipe2[1]));
                e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(a.pipe2[0] - 10, a.pipe2[3] - a.PipeDifferentY, 75, 15));

                e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(a.pipe2[2], a.pipe2[3], a.PipeWidth, this.Height - a.pipe2[3]));
                e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(a.pipe2[2] - 10, a.pipe2[3], 75, 15));
            }
        }
        private void CheckForPoint()
        {
            Rectangle rec = chicken.Bounds;
            Rectangle rec1 = new Rectangle(a.pipe1[2] + 20, a.pipe1[3] - a.PipeDifferentY, 15, a.PipeDifferentY);
            Rectangle rec2 = new Rectangle(a.pipe2[2] + 20, a.pipe2[3] - a.PipeDifferentY, 15, a.PipeDifferentY);
            Rectangle intersect1 = Rectangle.Intersect(rec, rec1);
            Rectangle intersect2 = Rectangle.Intersect(rec, rec2);
            if (!fl.ResetPipes | fl.start)
            {
                if (intersect1 != Rectangle.Empty | intersect2 != Rectangle.Empty)
                {
                    if (!fl.inPipe)
                    {
                        if (fl.start)
                        new Thread(SoundsF.Pipeplay).Start();     

                        points++;
                        fl.inPipe = true;
                    }
                }
                else fl.inPipe = false;
            }
        }

        private void CheckForCollision()
        {
            Rectangle rec = chicken.Bounds;
            Rectangle rec1 = new Rectangle(a.pipe1[0], 0, a.PipeWidth, a.pipe1[1]);
            Rectangle rec2 = new Rectangle(a.pipe1[2], a.pipe1[3], a.PipeWidth, this.Height - a.pipe1[3]);
            Rectangle rec3 = new Rectangle(a.pipe2[0], 0, a.PipeWidth, a.pipe2[1]);
            Rectangle rec4 = new Rectangle(a.pipe2[2], a.pipe2[3], a.PipeWidth, this.Height - a.pipe2[3]);
            Rectangle intersect1 = Rectangle.Intersect(rec , rec1);
            Rectangle intersect2 = Rectangle.Intersect(rec, rec2);
            Rectangle intersect3 = Rectangle.Intersect(rec, rec3);
            Rectangle intersect4 = Rectangle.Intersect(rec, rec4);
            if (!fl.ResetPipes | fl.start)
            {
                if (intersect1 != Rectangle.Empty | intersect2 != Rectangle.Empty | intersect3 != Rectangle.Empty | intersect4 != Rectangle.Empty)
                {
                    timer2.Enabled = false;
                    fl.Death = true;

                    if (fl.play)
                    {
                        new Thread(SoundsF.Dieplay).Start();
                        fl.play = false;
                    }
                    
                   
                        
                        step++;

                        Image f1 = Image.FromFile("Pic\\flappy1.gif");

                        f1.RotateFlip(RotateFlipType.Rotate270FlipX);
                        f1.RotateFlip(RotateFlipType.Rotate180FlipX);
                        chicken.Image = f1;
                        fl.AI = false;
                    
                    
                        
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!fl.Death)
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:

                        new Thread(SoundsF.Wingsplay).Start();
                        step -= 14;

                        Image f1 = Image.FromFile("Pic\\flappy1.gif");

                        chicken.Image = f1;
                        
                        break;
                    
                    case Keys.G:

                        if (fl.GODMODE)
                        {
                            fl.GODMODE = false;

                        }
                        else
                        {
                            fl.GODMODE = true;
                            if (fl.AI)
                                fl.GODMODE = false;
                        }
                        
                        break;
                    case Keys.P:
                        points += 99;
                        break;
                    case Keys.A:
                        if (fl.AI)
                            fl.AI = false;
                        else
                            fl.AI = true;
                        
                        break;
                }
            }
            else
            {
                step = 1;
                do{
                    step++;
                }while(step != 10);
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!fl.AI)// || DIE)
            {
               
                chicken.Location = new Point(chicken.Location.X, chicken.Location.Y + step);
                if (chicken.Location.Y < 0)
                {
                    chicken.Location = new Point(chicken.Location.X, 0);
                }
                if (chicken.Location.Y + chicken.Height > this.ClientSize.Height)
                {
                    if (!fl.GODMODE)
                    {
                        Die();
                        return;
                    }
                    else
                        chicken.Location = new Point(chicken.Location.X, this.ClientSize.Height - chicken.Height);
                }

                if (!fl.GODMODE)
                    CheckForCollision();

                SetStep++;
                if (step > 0 && (SetStep % 11) == 0)
                    step += 1;

            }
            else // Искуственный Интелект
            {
                threeangle();
                CheckForCollision();               
            }

            if (fl.running)
            {
                CheckForPoint();
            }
            label1.Text = Convert.ToString(points);
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!fl.Death)
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:

                        step = 1;

                        break;
                }
            }
            else
            {
                step = 1;
                do
                {
                    step++;
                } while (step != 10);
            }
        }
    }
    
}