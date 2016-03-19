using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(7, 6);

            game.SetupPlayers();
            game.Play();

            Console.ReadLine();
        }
    }
}
