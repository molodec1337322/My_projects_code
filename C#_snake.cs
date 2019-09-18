using System;
using System.Threading;


namespace Snake
{
    /// <summary>
    /// 
    /// класс сегмента змейки 
    /// 
    /// </summary>
    class SnakeSegment
    {
        protected int itsPosX, itsPosY;
        protected string itsChar;
        public static int length = -1;// -1 нужен, что бы учитывались только элементы змейки, а не яблока,
                                      // тк класс яблока наследуется от класса сегмента змейки,
                                      // в результате чего, при создания объекта яблока и 4 сегментов змейки
                                      // length равен 5, а не 4, как и должно быть


        public SnakeSegment(int newPosX, int newPosY, string newChar)
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

    /// <summary>
    /// 
    /// класс змейки
    /// 
    /// </summary>
    class Snake
    {
        private SnakeSegment[] itsSegments;
        private string segChar;

        public Snake(ref SnakeSegment[] segments, string segChar)
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
            itsSegments[SnakeSegment.length] = new SnakeSegment(itsSegments[SnakeSegment.length - 1].GetX(), itsSegments[SnakeSegment.length - 1].GetY(), segChar);
        }

        public bool CheckSnakeAlive()
        {
            if (itsSegments[0].GetX() == 0 || itsSegments[0].GetY() == 0 || itsSegments[0].GetX() == 14 || itsSegments[0].GetY() == 24)
            {
                return false;
            }
            for (int i = 1; i < SnakeSegment.length; i++)
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

    /// <summary>
    /// 
    /// Класс Яблока
    /// 
    /// </summary>
    class Apple : SnakeSegment
    {
        private Random rnd;
        public Apple(int newPosX, int newPosY, string newChar, Random rnd) : base(newPosX, newPosY, newChar)
        {
            this.rnd = rnd;
        }

        public void Relocate()
        {
            itsPosX = rnd.Next() % 13 + 1;
            itsPosY = rnd.Next() % 23 + 1;
        }
    }

    /// <summary>
    /// 
    /// класс вывода изображения в консоль
    /// 
    /// </summary>
    class Graphics
    {
        private int sizeX;
        private int sizeY;
        private Snake snake;
        private Apple apple;
        private string[,] gameField;
        int score;
        string strScore;
        public Graphics(int sizeX, int sizeY, Snake snake, Apple apple)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.snake = snake;
            this.apple = apple;
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

        /// <summary>
        /// 
        /// обновляет расположение объектов на экране
        /// 
        /// </summary>
        private void Update()
        {
            for (int i = 0; i < SnakeSegment.length; i++)
            {
                gameField[snake.GetSegment(i).GetX(), snake.GetSegment(i).GetY()] = snake.GetSegment(i).GetChar();
            }
            gameField[snake.GetSegment(SnakeSegment.length - 1).GetX(), snake.GetSegment(SnakeSegment.length - 1).GetY()] = " ";
            gameField[apple.GetX(), apple.GetY()] = apple.GetChar();
        }
        
        /// <summary>
        /// 
        /// отрисовывает изображение в консоль
        /// 
        /// </summary>
        public void Draw()
        {
            score = SnakeSegment.length - 4;
            strScore = score.ToString();
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
            Console.WriteLine($"\nScore:{strScore}");
        }
    }

    class Program
    {
        static public void WaitForExit()
        {
            while (true)
            {
                Console.ReadKey();
            }
        }

        static public void WaitForPressKey(int timer, ref Direction dir)
        {
            ConsoleKeyInfo key;
            while (timer != 0)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        dir = Direction.Right;
                        break;
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        dir = Direction.Left;
                        break;
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        dir = Direction.Up;
                        break;
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        dir = Direction.Down;
                        break;
                    }
                }
                Thread.Sleep(100);
                timer--;
            }
        }

        public enum Direction { Right, Left, Up, Down };

        static void Main(string[] args)
        {


            string segChar = "0";
            SnakeSegment[] segments = new SnakeSegment[4]
                { new SnakeSegment(3, 7, "#"),
                    new SnakeSegment(3, 6, segChar),
                    new SnakeSegment(3, 5, segChar),
                    new SnakeSegment(3, 4, segChar)}; 

            Random rnd = new Random();

            Snake snake = new Snake(ref segments, segChar);

            Apple apple = new Apple(10, 10, "A", rnd);

            Graphics g = new Graphics(15, 25, snake, apple);
            g.Draw();

            ConsoleKeyInfo key;
            Direction dir = 0;

            while (snake.CheckSnakeAlive())
            {
                WaitForPressKey(2, ref dir);

                if (dir == Direction.Right)
                {
                    snake.MoveSegments(snake.GetFirstX(), snake.GetFirstY() + 1);
                }
                else if (dir == Direction.Left)
                {
                    snake.MoveSegments(snake.GetFirstX(), snake.GetFirstY() - 1);
                }
                else if (dir == Direction.Up)
                {
                    snake.MoveSegments(snake.GetFirstX() - 1, snake.GetFirstY());
                }
                else if (dir == Direction.Down)
                {
                    snake.MoveSegments(snake.GetFirstX() + 1, snake.GetFirstY());
                }

                if (snake.GetFirstX() == apple.GetX() && snake.GetFirstY() == apple.GetY())
                {
                    snake.AddSegment();
                    apple.Relocate();
                }

                g.Draw();
            }
            Console.WriteLine($"GAME OVER!");
            WaitForExit();
        }
    }
}

