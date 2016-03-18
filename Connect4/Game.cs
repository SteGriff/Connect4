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

        private Counter[,] _board;

        public Game(int width, int height)
        {
            Width = width;
            Height = height;

            _board = new Counter[Width, Height];
            _messageTop = OriginTop + Height + 2;

            _min = 1;
            _max = Width;

            _totalPlayers = 2;

            SetupPlayers();
        }

        private void SetupPlayers()
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
                        thisPlayer = new Human(i);
                        break;
                    }
                    else if (key.Key == ConsoleKey.R)
                    {
                        thisPlayer = new Computer(i);
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

                    Counter c = _board[left, top];
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

        public void Drop(Player p, int x)
        {
            //Fix offset
            x -= 1;

            for (int y = Height - 1; y >= 0; y--)
            {
                if (_board[x, y] == null)
                {
                    _board[x, y] = new Counter(x, y, p);
                    return;
                }
            }
        }

        public Player Winner()
        {
            for (int top = 0; top < Height; top++)
            {
                for (int left = 0; left < Width; left++)
                {
                    if (_board[left, top] != null)
                    {
                        var c = _board[left, top];
                        int length = LineLength(c, Direction.Single);
                        //Debug.WriteLine("{0} has a line of {1}", c.Owner.Name, length);
                        if (length == 4)
                        {
                            return c.Owner;
                        }
                    }
                }
            }

            return null;
        }

        private Direction GetDirection(Counter thisCounter)
        {
            var directionIndicators = GetNeighbours(thisCounter);

            foreach (var d in directionIndicators)
            {
                if (d.Value != null && thisCounter.Owner.Owns(d.Value))
                {
                    return d.Key;
                }
            }

            return Direction.Single;
        }

        private Counter GetNeighbour(Counter thisCounter, Direction direction)
        {
            var directionIndicators = GetNeighbours(thisCounter);
            Counter neighbour;
            try
            {
                neighbour = directionIndicators[direction];
            }
            catch (Exception)
            {
                neighbour = null;
            }
            return neighbour;
        }

        private Dictionary<Direction, Counter> GetNeighbours(Counter thisCounter)
        {
            var options = new Dictionary<Direction, Counter>();
            try
            {
                options.Add(Direction.Right, _board[thisCounter.Left + 1, thisCounter.Top]);
            }
            catch (Exception) { }

            try
            {
                options.Add(Direction.RightDown, _board[thisCounter.Left + 1, thisCounter.Top + 1]);
            }
            catch (Exception) { }

            try
            {
                options.Add(Direction.Down, _board[thisCounter.Left, thisCounter.Top + 1]);
            }
            catch (Exception) { }

            try
            {
                options.Add(Direction.LeftDown, _board[thisCounter.Left - 1, thisCounter.Top + 1]);
            }
            catch (Exception) { }

            return options;
        }

        public int LineLength(Counter thisCounter, Direction direction)
        {
            if (direction == Direction.Single)
            {
                //Direction not known
                direction = GetDirection(thisCounter);
                if (direction == Direction.Single)
                {
                    //Not connected to anything
                    return 1;
                }
                return LineLength(thisCounter, direction);
            }
            else
            {
                Counter next = GetNeighbour(thisCounter, direction);
                //if the neighbour is empty or owned by a rival, end the line
                if (next == null || !thisCounter.Owner.Owns(next))
                {
                    return 1;
                }
                return 1 + LineLength(next, direction);

            }

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
