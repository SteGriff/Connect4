using System;
using System.Collections.Generic;
using System.Threading;

namespace Connect4
{
    public class Game
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Dictionary<int, Player> Players { get; set; }
        private int _totalPlayers { get; set; }

        private int _messageTop;

        private const int OriginTop = 2;
        private const int OriginLeft = 1;

        private int _min;
        private int _max;

        public Counter[,] Board { get; private set; }

        public Game(int width, int height)
        {
            Width = width;
            Height = height;

            Board = new Counter[Width, Height];
            _messageTop = OriginTop + Height + 2;

            _min = 1;
            _max = Width;

            _totalPlayers = 2;
            
        }

        public void SetupPlayers()
        {
            Players = new Dictionary<int, Player>();

            for (int i = 0; i < _totalPlayers; i++)
            {
                Player thisPlayer = null;

                while (true)
                {
                    Console.Write("Player {0}, press H for human, R for robot: ", i+1);
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.H)
                    {
                        thisPlayer = new Human(i, this);
                        break;
                    }
                    else if (key.Key == ConsoleKey.R)
                    {
                        thisPlayer = new Computer(i, this);
                        break;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Invalid selection!");
                }

                Console.WriteLine();
                thisPlayer.Setup();
                Players.Add(i, thisPlayer);

            }         

        }

        public void DrawBoard()
        {
            Console.Clear();

            //Draw numbers
            for (int left = 0; left < Width; left++)
            {
                int columnNum = left + 1;

                //At the top
                Console.SetCursorPosition(OriginLeft + left, OriginTop - 1);
                Console.Write(columnNum);

                //At the bottom
                Console.SetCursorPosition(OriginLeft + left, OriginTop + Height);
                Console.Write(columnNum);
            }

            for (int top = 0; top < Height; top++)
            {
                for (int left = 0; left < Width; left++)
                {
                    //Draw board content (blank or player counter)
                    Console.SetCursorPosition(OriginLeft + left, OriginTop + top);

                    Counter c = Board[left, top];
                    if (c == null)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        c.Owner.DrawPiece();
                    }
                }
            }

        }

        public void Play()
        {
            int turn = 0;

            DrawBoard();

            Player winner = null;

            while (winner == null)
            {
                turn += 1;
                turn %= _totalPlayers;

                var p = Players[turn];

                AnnounceTurn(p);

                int place = p.GetMove(_min, _max);
                Drop(p, place);

                DrawBoard();

                winner = Winner();

                Thread.Sleep(1000);
            }

            GoToMessages();
            Console.WriteLine();
            Console.WriteLine("{0} wins! Woooo!", winner.Name);
            Console.WriteLine();

            Console.ReadLine();

        }

        public Counter Drop(Player p, int x)
        {
            //Fix offset
            x -= 1;

            for (int y = Height - 1; y >= 0; y--)
            {
                if (Board[x, y] == null)
                {
                    var counter = new Counter(x, y, p);

                    Board[x, y] = counter;
                    p.GiveCounter(counter);

                    return counter;
                }
            }

            return null;
        }

        public Player Winner()
        {
            for (int top = 0; top < Height; top++)
            {
                for (int left = 0; left < Width; left++)
                {
                    if (Board[left, top] != null)
                    {
                        var c = Board[left, top];
                        int length = c.LineLength(Board);
                        
                        if (length == 4)
                        {
                            return c.Owner;
                        }
                    }
                }
            }

            return null;
        }
        
        private void AnnounceTurn(Player p)
        {
            GoToMessages();
            Console.WriteLine("{0}'s turn.", p.Name);
        }

        private void GoToMessages()
        {
            Console.SetCursorPosition(OriginLeft, _messageTop);
        }
    }
}
