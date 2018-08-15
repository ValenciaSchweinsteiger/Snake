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
        private int Level = 1;
        private int Score;
        public Form1()
        {
            InitializeComponent();
            NewBlockAppearsInSec = 3000;
            snake.Draw(panel2);
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
                    timer1.Interval -= 100; 
                    ++Level;
                }
                richTextBox1.Text = $"{Score} Level:{Level}";
                richTextBox1.Update();
                snake.GenerateNewBlock();
            }
            snake.Move();
            snake.Draw(panel2);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            if (button.Text == "Pause")
            {
                button.Text = "Play";
            }
            else if (button.Text == "Play")
            {
                button.Text = "Pause";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void GetNewBlock()
        {
            DrawPoint(rnd.Next(2, 95) * 10, rnd.Next(8, 58) * 10);
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
        public List<Point> body = new List<Point>();
        private Random rnd = new Random();
        private bool BlockCatched = false;

        public Snake()
        {
            direction = Direction.Right;
            color = Color.DeepPink;
            element.Y = 300;
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
    }
}
