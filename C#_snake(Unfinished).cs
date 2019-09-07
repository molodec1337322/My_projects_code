using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    class SnakeSegment
    {
        private int itsPosX, itsPosY;
        private string itsChar;
        public static int length = 0;
        

        public SnakeSegment(int newPosX, int newPosY, string newChar)
        /* Конструктор */
        {
            itsPosX = newPosX;
            itsPosY = newPosY;
            itsChar = newChar;
            length++;
        }

        public int GetX() {return itsPosX; }
        public int GetY() { return itsPosY; }
        public string GetChar() { return itsChar; }
        public void SetPos(int X, int Y)
        {
            itsPosX = X;
            itsPosY = Y;
        }

    }

    class Snake
    {
        private SnakeSegment[] itsSegments;
        private string segChar;

        public Snake(SnakeSegment[] segments, string segChar)
        /* Конструктор */
        {
            itsSegments = segments;
            this.segChar = segChar;
        }
        public void MoveSegments(int newPosX, int newPosY)
        {
            for (int i=SnakeSegment.length-1; i>0; i--)
            {
                itsSegments[i].SetPos(itsSegments[i - 1].GetX(), itsSegments[i - 1].GetY());
            }
            itsSegments[0].SetPos(newPosX, newPosY);
        }
        public void AddSegment()
        {
            Array.Resize(ref itsSegments, SnakeSegment.length + 1);
            itsSegments[SnakeSegment.length] = new SnakeSegment(-10, -10, segChar); //дада, костыль, но рабочий
        }

        public int GetFirstX() { return itsSegments[0].GetX(); }
        public int GetFirstY() { return itsSegments[0].GetY(); }
        public void showSegs()
        {
            foreach(SnakeSegment segment in itsSegments)
            {
                Console.Write(segment.GetX().ToString());
                Console.Write(" ");
                Console.Write(segment.GetY().ToString());
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }

    class Apple
    {
        private int itsPosX;
        private int itsPosY;
        public Apple(int newPosX, int newPosY)
        {
            itsPosX = newPosX;
            itsPosY = newPosY;
        }

        public int GetX() { return itsPosX; }
        public int GetY() { return itsPosY; }
    }

    class Graphics
    {
        private int sizeX;
        private int sizeY;
        private string[,] gameField;
        public Graphics(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            gameField = new string[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (x == 0 || y == 0 || x == sizeX - 1 || y == sizeY - 1)
                    {
                        gameField[x, y] = "*";
                    }
                    else
                    {
                        gameField[x, y] = " ";
                    }
                }
            }
        }

        public void Update(Snake segments, Apple apple)
        {

        }

        public void Draw()
        {
            Console.Clear();
            for (int x=0; x<sizeX; x++)
            {
                for(int y=0; y<sizeY; y++)
                {
                    Console.Write(gameField[x, y]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            string segChar = "%";
            SnakeSegment[] segments = new SnakeSegment[3]
                { new SnakeSegment(3, 7, segChar),
                    new SnakeSegment(3, 6, segChar),
                    new SnakeSegment(3, 5, segChar)}; // тоже по факту костыль, но ладно

            Snake snake = new Snake(segments, segChar);
            snake.showSegs();

            Apple apple = new Apple(10, 10);

            Graphics g = new Graphics(15, 30);
            g.Draw();

            int dir;
            while (true)
            {
                dir = int.Parse(Console.ReadLine());
                switch(dir)
                {
                    case 2:
                        snake.MoveSegments(snake.GetFirstX() + 1, snake.GetFirstY());
                        snake.showSegs();
                        break;
                    case 8:
                        snake.MoveSegments(snake.GetFirstX() - 1, snake.GetFirstY());
                        snake.showSegs();
                        break;
                    case 6:
                        snake.MoveSegments(snake.GetFirstX(), snake.GetFirstY() + 1);
                        snake.showSegs();
                        break;
                    case 4:
                        snake.MoveSegments(snake.GetFirstX(), snake.GetFirstY() - 1);
                        snake.showSegs();
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }
            }
        }

    }
}
