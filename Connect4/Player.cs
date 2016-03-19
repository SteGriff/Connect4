﻿using System;
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
        public List<Counter> Counters { get; set; }

        public Player(int id)
        {
            Id = id;
            Counters = new List<Counter>();
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

    }
}
