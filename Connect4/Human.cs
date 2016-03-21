using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Human : Player
    {
        public Human(int id, Game game) : base(id, game)
        { }

        public override int GetMove(int min, int max)
        {
            Console.WriteLine();
            while (true)
            {
                Console.Write("Enter column number:");
                var colNumber = Console.ReadLine();

                int validColumn;
                if (Int32.TryParse(colNumber, out validColumn))
                {
                    if (validColumn >= min && validColumn <= max)
                    {
                        return validColumn;
                    }
                    
                }
            }
        }

        public override void Setup()
        {
            Console.Write("Enter name: ");
            Name = Console.ReadLine();

            Console.WriteLine("Select a colour:");
            Colour = ConsoleTools.ChooseColour();

        }
    }
}
