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
        Snake snake = new Snake();

        public Form1()
        {
            InitializeComponent();
       
            snake.Draw(panel2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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
            TextBox box = (TextBox)sender;
            //box.Text = Score.ToString();

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
        private int size;
        private Color color;
        public List<Point> body = new List<Point>();

        public Snake()
        {
            direction = Direction.Down;
            size = 3;
            color = Color.DeepPink;
            element.Y = 100;
            for (int i = 0; i < size; ++i)
            {
                element.X = 60 - i * 20;
                body.Add(element);
            }
        }

        public int GetSize() => size;
        public Color GetColor() => color;

        public void Move()
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
            body.Insert(0, element);
        }
        public void Draw(Panel panel)
        {
            Bitmap bmp = new Bitmap(panel.Width, panel.Height);
            panel.BackgroundImage = (Image)bmp;
            panel.BackgroundImageLayout = ImageLayout.None;

            Graphics g = Graphics.FromImage(bmp);
            Pen myPen = new Pen(Color.DeepPink, 10);
            for (int i = 0; i < size; ++i)
            {
                g.DrawEllipse(myPen, body[i].X, body[i].Y, 11, 10);
            }
            myPen.Dispose();
            g.Dispose();
        }

        

    }
}
