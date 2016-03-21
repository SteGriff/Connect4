using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connect4
{
    public class Computer : Player
    {
        private Random _random;

        public Computer(int id, Game game) : base(id, game)
        { }

        public override int GetMove(int min, int max)
        {
            int column = 1;
            var bestLine = FindBestLine();
            var potentialMoves = PossibleNextMoves(bestLine);

            if (potentialMoves != null && potentialMoves.Count > 0)
            {
                //Randomise between options
                // not ideal - won't actually complete diagonals properly
                column = potentialMoves[_random.Next(0, potentialMoves.Count - 1)];
            }
            else
            { 
                if (bestLine.Length == 0)
                {
                    //First turn - random
                    column = _random.Next(min, max);
                }
                else
                {
                    //Second turn
                    //Left, on top, or right of existing counter
                    int currentLocation = bestLine.StartCounter.Left;
                    column = _random.Next(currentLocation - 1, currentLocation + 1);
                }

            }

            //AI fail
            if (column < min || column > max)
            {
                return _random.Next(min, max);
            }

            Thread.Sleep(1000);
            return column;
        }

        private List<int> PossibleNextMoves(LineDescription line)
        {
            var moves = new List<int>();

            switch (line.Direction)
            {
                case Direction.Down:
                    moves.Add(line.StartCounter.Left);
                    break;
                case Direction.Right | Direction.LeftDown | Direction.RightDown:
                    moves.Add(line.StartCounter.Left - line.Length);
                    moves.Add(line.StartCounter.Left + 1);
                    break;
            }

            return moves;
        }

        private struct LineDescription
        {
            public Counter StartCounter;
            public Direction Direction;
            public int Length;
        }

        private LineDescription FindBestLine()
        {
            LineDescription best = new LineDescription();

            foreach (var c in Counters)
            {
                var d = Direction.Single;
                int length = c.LineLength(d, Game.Board);
                if (length > best.Length)
                {
                    best.Length = length;
                    best.StartCounter = c;
                    best.Direction = d;
                }
            }

            return best;
        }

        public override void Setup()
        {
            Name = "Robot";
            Colour = ConsoleColor.Red;
            _random = new Random();
        }
    }
}
