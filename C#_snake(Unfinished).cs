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
        public static int lenght = 0;

        public SnakeSegment(int newPosX, int newPosY, int newSize)
        {
            itsPosX = newPosX;
            itsPosY = newPosY;
            itsSize = newSize;
            lenght++;
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
        private int itsLenght;
        private SnakeSegment[] itsSegments;

        public Snake(int lenght, SnakeSegment[] segments)
        {
            itsLenght = lenght;
            itsSegments = segments;
        }

        public int GetLenth() { return itsLenght; }
        public void moveSegments(int newPosX, int newPosY)
        {
            itsSegments[0].SetPos(newPosX, newPosY);
            for (int i=itsLenght; i>0; i--)
            {
                itsSegments[i].SetPos(itsSegments[i - 1].GetX(), itsSegments[i - 1].GetY());
            }
        }
        public void showSegs()
        {
            foreach(SnakeSegment segment in itsSegments)
            {
                Console.Write(segment.GetX().ToString());
                Console.Write(segment.GetY().ToString());
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            const int segSize = 20;
            SnakeSegment[] segments = new SnakeSegment[3]
                { new SnakeSegment(0, 1, segSize),
                    new SnakeSegment(0, 2, segSize),
                    new SnakeSegment(0, 3, segSize)};

            Snake snake = new Snake(SnakeSegment.lenght, segments);
            snake.showSegs();
            Console.ReadKey();
        }
    }
}
