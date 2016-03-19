using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Counter
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public Player Owner { get; set; }

        public Counter(int left, int top, Player owner)
        {
            Left = left;
            Top = top;
            Owner = owner;
        }

        public Direction GetDirection(Counter[,] board)
        {
            var directionIndicators = GetNeighbours(board);

            foreach (var d in directionIndicators)
            {
                if (d.Value != null && Owner.Owns(d.Value))
                {
                    return d.Key;
                }
            }

            return Direction.Single;
        }

        public Counter GetNeighbour(Direction direction, Counter[,] board)
        {
            var directionIndicators = GetNeighbours(board);
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

        private Dictionary<Direction, Counter> GetNeighbours(Counter[,] board)
        {
            var options = new Dictionary<Direction, Counter>();
            try
            {
                options.Add(Direction.Right, board[Left + 1, Top]);
            }
            catch (Exception) { }

            try
            {
                options.Add(Direction.RightDown, board[Left + 1, Top + 1]);
            }
            catch (Exception) { }

            try
            {
                options.Add(Direction.Down, board[Left, Top + 1]);
            }
            catch (Exception) { }

            try
            {
                options.Add(Direction.LeftDown, board[Left - 1, Top + 1]);
            }
            catch (Exception) { }

            return options;
        }
    }
}
