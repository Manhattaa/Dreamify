using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyClient
{
    internal class Utilities
    {
        // Hides pin and handles hiding it from view
        public static string EnterPin()
        {
            string pin = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);

                // If user doesn't press enter, backspace or esc -> PIN gets entered in console with '*'
                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Escape)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    pin += key.KeyChar; // Every character in PIN gets hidden with help of '*'
                    Console.Write("*");
                    Console.ResetColor();
                }

                // If user clicks backspace, it erases a character in password
                else if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                    pin = pin.Remove(pin.Length - 1);
                    Console.Write("\b \b");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return pin;
        }

        public static void PressEnter(string phrase)
        {
            Console.Write(phrase);
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (keyPressed.Key != ConsoleKey.Enter)
            {
                keyPressed = Console.ReadKey(true);
            }
        }
    }
}
