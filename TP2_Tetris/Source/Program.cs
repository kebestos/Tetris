using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Source
{
    class Program
    {
        static int ROWS = 22;
        static int COLS = 10;
        static Board board = new Board(ROWS, COLS);

        static ShuffleBag<Tetromino> bag = new ShuffleBag<Tetromino>();

        static int TIME = 500;
        static Timer timer = new Timer(TIME);

        static void Main(string[] args)
        {
            Init();
            while (true)
            {
                ProcessInput();
                Render();
            }
        }

        static void Init()
        {
            FillBag();
            Console.SetWindowSize(2 * COLS + 10, ROWS + 6);
            timer.Elapsed += new ElapsedEventHandler(Update);
            timer.Start();
        }

        static void FillBag()
        {
            bag.Add(Tetromino.I_SHAPE, 4);
            bag.Add(Tetromino.J_SHAPE, 4);
            bag.Add(Tetromino.L_SHAPE, 4);
            bag.Add(Tetromino.O_SHAPE, 4);
            bag.Add(Tetromino.S_SHAPE, 4);
            bag.Add(Tetromino.T_SHAPE, 4);
            bag.Add(Tetromino.Z_SHAPE, 4);
        }

        static void Update(object source, ElapsedEventArgs e)
        {
            lock(board)
            {
                if (board.IsFallingBlock())
                    board.MoveDown();
                else
                    board.Drop(bag.Next());
            }
        }

        static void ProcessInput()
        {
            lock(board)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.LeftArrow)
                    {
                        board.MoveLeft();
                    }
                    else if (cki.Key == ConsoleKey.RightArrow)
                    {
                        board.MoveRight();
                    }
                    else if (cki.Key == ConsoleKey.DownArrow)
                    {
                        board.MoveDown();
                    }
                    else if (cki.Key == ConsoleKey.UpArrow)
                    {
                        board.RotateRight();
                    }
                }
            }
        }

        static void Render()
        {
            lock (board)
            {
                string state = Convert(board.ToString());
                Console.SetCursorPosition(0, 0);
                Console.Write(state);
            }
        }

        static string Convert(string state)
        {
            StringBuilder new_state = new StringBuilder("\n<!");

            foreach(char c in state)
            {
                if (c == '.')
                    new_state.Append(". ");
                else if (c == '\n')
                    new_state.Append("!>\n<!");
                else
                    new_state.Append("[]");
            }
            for(int i=0; i< COLS; i++)
                new_state.Append("**");
            new_state.Append("!>\n\n");

            new_state.Append("Arrows to play\nCTRL+C to exit\n");

            return new_state.ToString();
        }
    }

    class ShuffleBag<T>
    {
        Random random = new Random();
        List<T> data;

        T currentItem;
        int currentPosition = -1;

        public int Size { get { return data.Count; } }

        public ShuffleBag(int capacity = 0)
        {
            data = new List<T>(capacity);
        }

        public void Add(T item, int amount)
        {
            for (int i = 0; i < amount; i++)
                data.Add(item);

            currentPosition = Size - 1;
        }

        public T Next()
        {
            if (currentPosition < 1)
            {
                currentPosition = Size - 1;
                currentItem = data[0];
                return currentItem;
            }

            int position = random.Next(currentPosition);
            Swap(position);
            currentPosition--;

            return currentItem;
        }

        void Swap(int position)
        {
            currentItem = data[position];
            data[position] = data[currentPosition];
            data[currentPosition] = currentItem;
        }
    }
}
