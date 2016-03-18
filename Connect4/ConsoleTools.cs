using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public static class ConsoleTools
    {
        public static ConsoleColor ChooseColour()
        {
            for (int colourNumber = 1; colourNumber < 15; colourNumber++)
            {
                Console.ForegroundColor = (ConsoleColor)colourNumber;
                Console.Write(colourNumber + " ");
            }

            Console.ResetColor();
            Console.WriteLine();

            while (true)
            {
                Console.Write("Enter number: ");
                string selection = Console.ReadLine();

                int validNumber;
                if (Int32.TryParse(selection, out validNumber))
                {
                    if (validNumber > 0 && validNumber < 16)
                    {
                        return (ConsoleColor)validNumber;
                    }
                }

                Console.WriteLine("Invalid selection, try again.");
            }
        }
    }
}
