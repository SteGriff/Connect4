using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public abstract class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ConsoleColor Colour { get; set; }

        protected List<Counter> Counters { get; private set; }
        protected Game Game { get; set; }

        public Player(int id, Game game)
        {
            Id = id;
            Counters = new List<Counter>();
            Game = game;
        }

        public abstract int GetMove(int min, int max);

        public abstract void Setup();

        public void DrawPiece()
        {
            Console.ForegroundColor = Colour;
            Console.Write("O");
            Console.ResetColor();
        }

        public bool Owns(Counter counter)
        {
            return counter.Owner == this;
        }

        public void GiveCounter(Counter counter)
        {
            Counters.Add(counter);
        }

    }
}
