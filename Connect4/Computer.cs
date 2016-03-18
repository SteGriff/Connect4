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

        public Computer(int id) : base(id)
        { }

        public override int GetMove(int min, int max)
        {
            int column = _random.Next(min, max);
            Thread.Sleep(1000);
            return column;
        }

        public override void Setup()
        {
            Name = "Robot";
            Colour = ConsoleColor.Red;
            _random = new Random();
        }
    }
}
