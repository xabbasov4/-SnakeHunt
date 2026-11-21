namespace SnakeHunt
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        bool isBomb = false;
        int score = 0;
        int sec = 0;
        bool gameOver = false;
        int suret = 20;
        public Form1()
        {
            InitializeComponent();
            timer2.Interval = 2000;
            timer2.Tick += timer2_Tick;
            StartNewGame();
        }
        void StartNewGame()
        {
            score = 0;
            sec = 0;
            gameOver = false;
            timer1.Interval = 200;
            suret = 15;
            pictureBox1.Location = new Point(100, 100);
            SpawnItem(true);
            timer1.Start();
            timer2.Start();
        }
        void SpawnItem(bool forceApple = false)
        {
            sec = 0;
            isBomb = !forceApple && r.Next(2) == 0;

            if (isBomb)
                pictureBox2.Image = Image.FromFile("C:\\Users\\User\\Desktop\\bomba.png");
            else
                pictureBox2.Image = Image.FromFile("C:\\Users\\User\\Downloads\\apple.png");

            pictureBox2.Left = r.Next(ClientSize.Width - pictureBox2.Width);
            pictureBox2.Top = r.Next(ClientSize.Height - pictureBox2.Height);
        }
        void CheckHit()
        {
            if (!pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds)) return;

            if (isBomb)
            {
                gameOver = true;
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("Xal: " + score);
            }
            else
            {
                score++;
                if (suret < 60)
                    suret += 5;
                SpawnItem();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (gameOver) return;
            sec++;
            if (sec >= 5)
            {
                SpawnItem();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameOver) return;
            CheckHit();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartNewGame();
                return;
            }
            if (e.KeyCode == Keys.Left && pictureBox1.Left > 0)
            {
                pictureBox1.Left -= suret;
            }
            if (e.KeyCode == Keys.Right && pictureBox1.Right < ClientSize.Width)
            {
                pictureBox1.Left += suret;
            }
            if (e.KeyCode == Keys.Up && pictureBox1.Top > 0)
            {
                pictureBox1.Top -= suret;
            }
            if (e.KeyCode == Keys.Down && pictureBox1.Bottom < ClientSize.Height)
            {
                pictureBox1.Top += suret;
            }
            CheckHit();
        }
    }
}