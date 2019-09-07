using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class SnakeSegment
    {
        public int itsPosX, itsPosY, itsSize;
        public static int length = 0;

        public SnakeSegment(int newPosX, int newPosY, int newSize)
        /* Конструктор */
        {
            itsPosX = newPosX;
            itsPosY = newPosY;
            itsSize = newSize;
            length++;
        }

        public int GetX() {return itsPosX; }
        public int GetY() { return itsPosY; }
        public int GetSize() { return itsSize; }
        public void SetPos(int X, int Y)
        {
            itsPosX = X;
            itsPosY = Y;
        }

    }

    class Snake
    {
        private SnakeSegment[] itsSegments;
        private int segSize;

        public Snake(SnakeSegment[] segments, int segSize)
        /* Конструктор */
        {
            itsSegments = segments;
            this.segSize = segSize;
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
            itsSegments[SnakeSegment.length] = new SnakeSegment(-10, -10, segSize); //дада, костыль, но рабочий
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

    class Program
    {

        static void Main(string[] args)
        {
            const int segSize = 20;
            SnakeSegment[] segments = new SnakeSegment[3]
                { new SnakeSegment(0, 3, segSize),
                    new SnakeSegment(0, 2, segSize),
                    new SnakeSegment(0, 1, segSize)};

            Snake snake = new Snake(segments, segSize);
            snake.showSegs();

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
