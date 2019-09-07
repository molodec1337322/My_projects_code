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
            itsSegments[SnakeSegment.length] = new SnakeSegment(-1, -1, segSize);
        }
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
            snake.MoveSegments(0, 4);
            snake.showSegs();
            snake.AddSegment();
            snake.showSegs();
            snake.MoveSegments(0, 5);
            snake.showSegs();
            Console.ReadKey();
        }

    }
}
