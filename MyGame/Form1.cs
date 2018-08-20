using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyGame
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();
        private Snake snake = new Snake();
        private int NewBlockAppearsInSec;
        private int InitTimerIntervalValue = 500;
        private int Level = 1;
        private int Score;
        public Form1()
        {
            InitializeComponent();
            NewBlockAppearsInSec = 3000;
            snake.Draw(panel2);
            this.timer1.Interval = InitTimerIntervalValue;
            //timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (!snake.IsBlockCatched())
                snake.CatchBlock();
            if (snake.IsBlockCatched())
            {
                Score += Level * 10;
                if ((snake.body.Count() % 5) == 0)
                {
                    if (this.timer1.Interval > 50)
                        this.timer1.Interval -= 50;
                    ++Level;
                }
                label3.Text = $"{Score}";
                label4.Text = $"{Level}";
                //this.richTextBox1.Update();
                snake.GenerateNewBlock();
            }
            snake.PlayMode = GetPlayMode();
            snake.lastScore = Score;
            snake.Move();
            if (snake.Loosed)
            {
                this.timer1.Stop();
            }
            snake.Draw(panel2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            snake = new Snake();
            Level = 1;
            Score = 0;
            timer1.Interval = InitTimerIntervalValue;
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (snake.Loosed)
                return;
            Button button = (Button)sender;
            if (button.Text == "Pause")
            {
                button.Text = "Play";
                timer1.Stop();
            }
            else if (button.Text == "Play")
            {
                button.Text = "Pause";
                timer1.Start();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void GetNewBlock()
        {
            Point p = new Point();
            do
            {
                p.X = rnd.Next(2, 95) * 10;
                p.Y = rnd.Next(8, 58) * 10;
            }
            while (snake.body.Any( x => x.Equals(p)));
            DrawPoint(p.X,p.Y);
        }

        public void DrawPoint(int x, int y)
        {
            Bitmap bmp = new Bitmap(panel2.Width, panel2.Height);
            panel2.BackgroundImage = (Image)bmp;
            panel2.BackgroundImageLayout = ImageLayout.None;

            Graphics g = Graphics.FromImage(bmp);
            Pen myPen = new Pen(Color.DeepPink, 10);
            g.DrawEllipse(myPen, x, y, 10, 10);
            myPen.Dispose();
            g.Dispose();
        }

        private void panel2_KeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (snake.direction != Direction.Down)
                        snake.direction = Direction.Up;
                    return;
                case Keys.S:
                    if (snake.direction != Direction.Up)
                        snake.direction = Direction.Down;
                    return;
                case Keys.A:
                    if (snake.direction != Direction.Right)
                        snake.direction = Direction.Left;
                    return;
                case Keys.D:
                    if (snake.direction != Direction.Left)
                        snake.direction = Direction.Right;
                    return;
            }
        }

        private void form_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (snake.direction != Direction.Down)
                        snake.direction = Direction.Up;
                    //e.Handled = true;
                    return;
                case Keys.S:
                    //e.Handled = true;
                    if (snake.direction != Direction.Up)
                        snake.direction = Direction.Down;
                    return;
                case Keys.A:
                    if (snake.direction != Direction.Right)
                        snake.direction = Direction.Left;
                    return;
                case Keys.D:
                    if (snake.direction != Direction.Up)
                        snake.direction = Direction.Right;
                    return;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.radioButton2.TabIndex = 0;
            this.radioButton1.TabIndex = 9;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.radioButton1.TabIndex = 0;
            this.radioButton2.TabIndex = 9;
        }
        public int GetPlayMode() => (radioButton1.TabIndex == 0 ? 2:1);
    }

    public enum Direction
    {
        Up = 1,
        Down,
        Left,
        Right
    }

    class Snake
    {
        public Direction direction;
        private Point element = new Point();
        public Point NewBlock;
        private Color color;
        public List<Point> body;
        private Random rnd = new Random();
        private bool BlockCatched;
        public int lastScore;
        public int PlayMode;
        public bool Loosed;

        public Snake()
        {
            BlockCatched = false;
            body = new List<Point>();
            NewBlock = new Point();
            direction = Direction.Right;
            color = Color.DeepPink;
            element.Y = 300;
            PlayMode = 1;
            Loosed = false;
            for (int i = 0; i < 3; ++i)
            {
                element.X = 60 - i * 20;
                body.Add(element);
            }
            GenerateNewBlock();
        }

        public int GetSize() => body.Count();
        public Color GetColor() => color;

        public void Move(Point p = new Point())
        {
            if (Loosed)
                return;
            if ((p.X == 0) && (p.Y == 0))
            {
                element = body[0];
                switch (direction)
                {
                    case Direction.Down:
                        element.Y += 20;
                        break;
                    case Direction.Left:
                        element.X -= 20;
                        break;
                    case Direction.Right:
                        element.X += 20;
                        break;
                    case Direction.Up:
                        element.Y -= 20;
                        break;
                 }

                if (element.Y > 580)
                {
                    if (PlayMode == 1)
                        element.Y %= 580;
                    else
                        GameOver(lastScore);
                 }
                if (element.Y < 0)
                {
                    if (PlayMode == 1)
                        element.Y += 590;
                    else
                        GameOver(lastScore);
                }
                
                if (element.X > 940)
                {
                    if (PlayMode == 1)
                        element.X %= 940;
                    else
                        GameOver(lastScore);
                }
                
                if (element.X < 0)
                {
                    if (PlayMode == 1)
                        element.X += 940;
                    else
                        GameOver(lastScore);
                }
                if (body.Any(x => x.Equals(element)))
                    GameOver(lastScore);
                if (Loosed) 
                    return;
                body.RemoveAt(body.Count - 1);
            }
            else
            {
                element = p;
            }
             body.Insert(0, element);
        }
        public void Draw(Panel panel)
        {
            Bitmap bmp = new Bitmap(panel.Width, panel.Height);
            panel.BackgroundImage = (Image)bmp;
            panel.BackgroundImageLayout = ImageLayout.None;

            Graphics g = Graphics.FromImage(bmp);
            Pen myPen = new Pen(Color.DeepPink, 10);
            for (int i = 0; i < body.Count(); ++i)
            {
                g.DrawEllipse(myPen, body[i].X, body[i].Y, 11, 10);
            }
            if (!BlockCatched)
            {
                g.DrawEllipse(myPen, NewBlock.X, NewBlock.Y, 11, 10);
            }
            if (PlayMode == 2)
            {
                myPen.Color = Color.DimGray;
                g.DrawLine(myPen, 1, 1, 939, 1);// 639);
                g.DrawLine(myPen, 1, 1, 1, 579);
                g.DrawLine(myPen, 1, 1, 939, 1);
                g.DrawLine(myPen, 939, 1, 939, 579);
                g.DrawLine(myPen, 1, 579, 939, 579);
            }
            myPen.Dispose();
            g.Dispose();
        }

        public void GenerateNewBlock()
        {
            NewBlock = new Point(rnd.Next(1, 47) * 20, rnd.Next(1, 29) * 20);
            BlockCatched = false;
        }

        public bool IsBlockCatched() => BlockCatched;
        public void CatchBlock()
        {
            switch (direction)
            {
                case Direction.Down:
                case Direction.Up:
                    if ((body[0].X == NewBlock.X) && (Math.Abs(NewBlock.Y - body[0].Y) < 22))
                    {
                        Move(NewBlock);
                        BlockCatched = true;
                    }
                        break;
                case Direction.Left:
                case Direction.Right:
                    if ((body[0].Y == NewBlock.Y) && (Math.Abs(body[0].X - NewBlock.X) < 22))
                    {
                        Move(NewBlock);
                        BlockCatched = true;
                    }
                    break;
            }
            if (BlockCatched)
                NewBlock = new Point();
                
        }
        private void GameOver(int score)
        {
            Loosed = true;
            System.Windows.Forms.MessageBox.Show($"Game Over\nYour Score: {score}");
        }

        static Predicate<Point> IsSnakePart(Point newPoint)
        {
            return delegate (Point p)
            {
                return p.Equals(newPoint);
            };
        }
    }

}
