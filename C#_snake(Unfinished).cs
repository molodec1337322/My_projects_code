using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int GetX() { return itsPosX; }
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

        public Snake(ref SnakeSegment[] segments, string segChar)
        /* Конструктор */
        {
            itsSegments = segments;
            this.segChar = segChar;
        }
        public void MoveSegments(int newPosX, int newPosY)
        {
            for (int i = SnakeSegment.length - 1; i > 0; i--)
            {
                itsSegments[i].SetPos(itsSegments[i - 1].GetX(), itsSegments[i - 1].GetY());
            }
            itsSegments[0].SetPos(newPosX, newPosY);
        }
        public void AddSegment()
        {
            Array.Resize(ref itsSegments, SnakeSegment.length + 1);
            itsSegments[SnakeSegment.length - 1] = new SnakeSegment(-10, -10, segChar);
        }

        public bool CheckSnakeAlive()
        {
            if (itsSegments[0].GetX() == 0 || itsSegments[0].GetY() == 0 || itsSegments[0].GetX() == 14 || itsSegments[0].GetY() == 29)
            {
                return false;
            }
            for(int i=1; i<SnakeSegment.length; i++)
            {
                if (itsSegments[0].GetX() == itsSegments[i].GetX() && itsSegments[0].GetY() == itsSegments[i].GetY())
                {
                    return false;
                }
            }
            return true;
        }

        public int GetFirstX() { return itsSegments[0].GetX(); }
        public int GetFirstY() { return itsSegments[0].GetY(); }
        public SnakeSegment GetSegment(int index) { return itsSegments[index]; }// возвращает элемент змейки с индексом index
    }

    class Apple
    {
        private int itsPosX;
        private int itsPosY;
        private string itsChar;
        private Random rnd;
        public Apple(int newPosX, int newPosY, string newChar, Random rnd)
        {
            itsPosX = newPosX;
            itsPosY = newPosY;
            itsChar = newChar;
            this.rnd = rnd;
        }

        public void Relocate()
        {
            itsPosX = rnd.Next()%13+1;
            itsPosY = rnd.Next()%28+1;
        }

        public int GetX() { return itsPosX; }
        public int GetY() { return itsPosY; }
        public string GetChar() { return itsChar; }
    }

    class Graphics
    {
        private int sizeX;
        private int sizeY;
        private Snake snake;
        private Apple apple;
        private string[,] gameField;
        public Graphics(int sizeX, int sizeY, Snake snake, Apple apple)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.snake = snake;
            this.apple = apple;
            gameField = new string[sizeX, sizeY];

            //первичное заполнение поля
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

        public void Update()
        {
            for (int i = 0; i < SnakeSegment.length; i++)
            {
                gameField[snake.GetSegment(i).GetX(), snake.GetSegment(i).GetY()] = snake.GetSegment(i).GetChar();
            }
            gameField[snake.GetSegment(SnakeSegment.length - 1).GetX(), snake.GetSegment(SnakeSegment.length - 1).GetY()] = " ";
            gameField[apple.GetX(), apple.GetY()] = apple.GetChar();
        }

        public void Draw()
        {
            Update();
            Console.Clear();
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
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
            string segChar = "0";
            SnakeSegment[] segments = new SnakeSegment[4]
                { new SnakeSegment(3, 7, "#"),
                    new SnakeSegment(3, 6, segChar),
                    new SnakeSegment(3, 5, segChar),
                    new SnakeSegment(3, 4, segChar)}; // тоже по факту костыль, но ладно

            Random rnd = new Random();

            Snake snake = new Snake(ref segments, segChar);

            Apple apple = new Apple(10, 10, "A", rnd);


            Graphics g = new Graphics(15, 30, snake, apple);
            g.Draw();

            ConsoleKeyInfo dir;
            while (snake.CheckSnakeAlive())
            {
                dir = Console.ReadKey();
                if(dir.Key == ConsoleKey.DownArrow)
                {
                    snake.MoveSegments(snake.GetFirstX() + 1, snake.GetFirstY());
                    if(snake.GetFirstX() == apple.GetX() && snake.GetFirstY() == apple.GetY())
                    {
                        snake.AddSegment();
                        apple.Relocate();
                    }
                    g.Draw();
                }
                else if(dir.Key == ConsoleKey.UpArrow)
                {
                    snake.MoveSegments(snake.GetFirstX() - 1, snake.GetFirstY());
                    if (snake.GetFirstX() == apple.GetX() && snake.GetFirstY() == apple.GetY())
                    {
                        snake.AddSegment();
                        apple.Relocate();
                    }
                    g.Draw();
                }
                else if(dir.Key == ConsoleKey.RightArrow)
                {
                    snake.MoveSegments(snake.GetFirstX(), snake.GetFirstY() + 1);
                    if (snake.GetFirstX() == apple.GetX() && snake.GetFirstY() == apple.GetY())
                    {
                        snake.AddSegment();
                        apple.Relocate();
                    }
                    g.Draw();
                }
                else if(dir.Key == ConsoleKey.LeftArrow)
                {
                    snake.MoveSegments(snake.GetFirstX(), snake.GetFirstY() - 1);
                    if (snake.GetFirstX() == apple.GetX() && snake.GetFirstY() == apple.GetY())
                    {
                        snake.AddSegment();
                        apple.Relocate();
                    }
                    g.Draw();
                }
                else
                {
                    
                }
                
            }
        }
    }
}
