using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyClient
{   
    public class NavFunctions
    {
        public static int Navigation(List<string> songs, List<string> artists, string phrase)
        {
            int menuSelection = 0;

            // Loops until user presses enter on a choice
            while (true)
            {
                // Clears window and re-prints the sent in phrase on each loop
                Console.Clear();
                Console.WriteLine(phrase);

                // Forloop to print all the options 
                for (int i = 0; i < songs.Count; i++)
                {
                    // Changes color of the option we've currently selected so when menuSelection is for exemple "2" the second option will turn darkgrey
                    if (i == menuSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                    }

                    // Prints all the options in the array along with the pointer arrow if on the current selection
                    Console.Write($"Song: {songs.ElementAt(i)} Artist: {artists.ElementAt(i)}\n");

                    // Reset color to default
                    Console.ResetColor();
                }

                // If menu selection is 1 more than the list it points on exit so we need to change the color for the exit printout
                if (menuSelection == songs.Count)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                }
                Console.WriteLine("Exit");

                // Reset color to default
                Console.ResetColor();


                //"Listen" to keystrokes from the user
                ConsoleKeyInfo key = Console.ReadKey(true);

                //Handles the arrow keys to move up and down the menu
                if (key.Key == ConsoleKey.UpArrow)
                {
                    menuSelection--;

                    // If we go out of bounds up it goes to the bottom of the list
                    if (menuSelection == -1)
                        menuSelection = songs.Count;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    menuSelection++;

                    // If we go out of bounds down it goes to the top of the list (+1 for the exit not included in the list)
                    if (menuSelection == songs.Count + 1)
                        menuSelection = 0;

                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(); // New line for formatting - to look nicer
                    return menuSelection;
                }
            }
        }
    }
}