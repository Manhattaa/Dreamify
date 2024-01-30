using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyClient
{
    internal class MenuFunctions
    {
        public static void header()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n\t\t====================================");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t     ╔╦╗┬─┐┌─┐┌─┐┌┬┐┬┌─┐┬ ┬\r\n\t\t      ║║├┬┘├┤ ├─┤││││├┤ └┬┘\r\n\t\t     ═╩╝┴└─└─┘┴ ┴┴ ┴┴└   ┴");
            Console.ResetColor();
            Console.WriteLine("\t\t====================================");
        }

        public static void headerNoClear()
        {
            Console.WriteLine("\n\n\n\n\t\t====================================");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t     ╔╦╗┬─┐┌─┐┌─┐┌┬┐┬┌─┐┬ ┬\r\n\t\t      ║║├┬┘├┤ ├─┤││││├┤ └┬┘\r\n\t\t     ═╩╝┴└─└─┘┴ ┴┴ ┴┴└   ┴");
            Console.ResetColor();
            Console.WriteLine("\t\t====================================");
        }

        public static void main_header()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t\t   Welcome to \n\t\t==================================");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t     ╔╦╗┬─┐┌─┐┌─┐┌┬┐┬┌─┐┬ ┬\r\n\t\t      ║║├┬┘├┤ ├─┤││││├┤ └┬┘\r\n\t\t     ═╩╝┴└─└─┘┴ ┴┴ ┴┴└   ┴");
            Console.ResetColor();
            Console.WriteLine("\t\t==================================");
            footer();
            Console.WriteLine("\t\t    Press [Enter] to continue");

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (keyPressed.Key != ConsoleKey.Enter)
            {
                keyPressed = Console.ReadKey(true);
            }

        }

        public static void footer()
        {
            Console.WriteLine("\t\t===================================");
        }

        public static void divider()
        {
            Console.WriteLine("\t\t-----------------------------------");
        }

        public static void bigboydivider()
        {
            Console.WriteLine("\t----------------------------------------------------");
        }


        // Promts user to press enter key doesn't accept any other input
        public static void PressEnter()
        {
            Console.Write("\t\t  Press [Enter] to continue");
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (keyPressed.Key != ConsoleKey.Enter)
            {
                keyPressed = Console.ReadKey(true);
            }
        }

    }
}
