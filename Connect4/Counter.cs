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
    }
}
