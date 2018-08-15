using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace MyGame
{
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
        private int siz
        private Color color;
        public Queue<Point> body = new Queue<Point>();

        public Snake()
        {
            size = 3;
            color = Color.DeepPink;
            element.Y = 100;
            for (int i = 0; i < size; ++i)
            {
                element.X = 60 - i*10;
                body.Enqueue(element);
            }
        }

        public int GetSize() => size;
        public Color GetColor() => color;

        public void Move(Direction d)
        {
            element = body.Peek();
            switch (d)
            {
                case Direction.Down:
                    element.Y -= 10;
                    break;
                case Direction.Left:
                    element.X -= 10;
                    break;
                case Direction.Right:
                    element.X += 10;
                    break;
                case Direction.Up:
                    element.Y += 10;
                    break;
            }
            body.Dequeue();
            body.Enqueue(element);
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
                g.DrawEllipse(myPen, body.Peek().X + 20 * i, body.Peek().Y, 11, 10);
            }
            myPen.Dispose();
            g.Dispose();
        }
        

    }

}*/
