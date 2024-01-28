using DreamifyClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyClient
{   
    public class NavFunctions
    {
        public static int NavigationGenericArray(string[] options, string phrase)
        {
            int menuSelection = 0;

            // Loops until user presses enter on a choice
            while (true)
            {
                // Clears window and re-prints the sent in phrase on each loop
                Console.Clear();
                Console.WriteLine($"\t\t  {phrase}");
                MenuFunctions.divider();

                for (int i = 0; i < options.Length; i++)
                {
                    // Changes color of the option we've currently selected so when menuSelection is for exemple "2" the second option will turn darkgrey
                    if (i == menuSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }

                    Console.Write($"\t\t  {options[i]}\n");
                    Console.ResetColor();
                }

                // If menu selection is 1 more than the list it points on exit so we need to change the color for the exit printout
                if (menuSelection == options.Length)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                }

                Console.WriteLine("\t\t  Exit");
                Console.ResetColor();


                //"Listen" to keystrokes from the user
                ConsoleKeyInfo key = Console.ReadKey(true);

                //Handles the arrow keys to move up and down the menu
                if (key.Key == ConsoleKey.UpArrow)
                {
                    menuSelection--;

                    // If we go out of bounds up it goes to the bottom of the list
                    if (menuSelection == -1)
                        menuSelection = options.Length;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    menuSelection++;

                    // If we go out of bounds down it goes to the top of the list (+1 for the exit not included in the list)
                    if (menuSelection == options.Length + 1)
                        menuSelection = 0;

                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(); // New line for formatting - to look nicer
                    return menuSelection;
                }
            }
        }


        public static int NavigationSongSearch(List<SongSearchViewModel> songs, List<SongArtistViewModel> artists, string phrase)
        {
            int menuSelection = 0;

            while (true)
            {
                // Clears window and re-prints the sent in phrase on each loop
                Console.Clear();                
                Console.WriteLine($"\t\t  {phrase}");
                MenuFunctions.divider();
 
                for (int i = 0; i < songs.Count; i++)
                {
                    // Changes color of the option we've currently selected so when menuSelection is for exemple "2" the second option will turn darkgrey
                    if (i == menuSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }

                    // Prints all the options
                    Console.Write($"\t\t  Song: {songs.ElementAt(i).SongName} Artist: {artists.ElementAt(i).ArtistName}\n");
                    Console.ResetColor();
                }

                // If menu selection is 1 more than the list it points on exit so we need to change the color for the exit printout
                if (menuSelection == songs.Count)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                }

                Console.WriteLine("\t\t  Exit");
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

        public static int NavigationArtistSearch(List<SpotifyArtistsSearchViewModel> artists, string phrase)
        {
            int menuSelection = 0;

            while (true)
            {
                // Clears window and re-prints the sent in phrase on each loop
                Console.Clear();
                Console.WriteLine($"\t\t  {phrase}");
                MenuFunctions.divider();

                for (int i = 0; i < artists.Count; i++)
                {
                    // Changes color of the option we've currently selected so when menuSelection is for example "2" the second option will turn dark grey
                    if (i == menuSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }

                    // Prints all the options
                    Console.Write($"\t\t  Artist: {artists.ElementAt(i).ArtistName}\n");
                    Console.ResetColor();
                }

                // If menu selection is 1 more than the list it points on exit so we need to change the color for the exit printout
                if (menuSelection == artists.Count)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                }

                Console.WriteLine("\t\t  Exit");
                Console.ResetColor();

                //"Listen" to keystrokes from the user
                ConsoleKeyInfo key = Console.ReadKey(true);

                //Handles the arrow keys to move up and down the menu
                if (key.Key == ConsoleKey.UpArrow)
                {
                    menuSelection--;

                    // If we go out of bounds up it goes to the bottom of the list
                    if (menuSelection == -1)
                        menuSelection = artists.Count;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    menuSelection++;

                    // If we go out of bounds down it goes to the top of the list (+1 for the exit not included in the list)
                    if (menuSelection == artists.Count + 1)
                        menuSelection = 0;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(); // New line for formatting - to look nicer
                    return menuSelection;
                }
            }
        }

        public static int NavigationMain()
        {
            string[] options = { "List all songs", "List all artists", "List all genres",
            "Add song via search", "Add artist via search", "Exit"};

            int menuSelection = 0;

            while (true)
            {
                Console.Clear();
                MenuFunctions.header();
                Console.WriteLine("\t\t  Choose an option:" );
                MenuFunctions.divider();

                for (int i = 0; i < options.Length; i++)
                {
                    // Changes color of the option we've currently selected so when menuSelection is for exemple "2" the second option will turn darkgrey
                    if (i == menuSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }

                    // Prints all the options
                    Console.WriteLine($"\t\t  {options[i]}");
                    Console.ResetColor();
                }

                // If menu selection is 1 more than the list it points on exit so we need to change the color for the exit printout
                if (menuSelection == options.Length)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                }

                Console.ResetColor();

                MenuFunctions.footer();

                //"Listen" to keystrokes from the user
                ConsoleKeyInfo key = Console.ReadKey(true);

                //Handles the arrow keys to move up and down the menu
                if (key.Key == ConsoleKey.UpArrow)
                {
                    menuSelection--;

                    // If we go out of bounds up it goes to the bottom of the list
                    if (menuSelection == -1)
                        menuSelection = options.Length - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    menuSelection++;

                    // If we go out of bounds down it goes to the top of the list
                        if (menuSelection == options.Length)
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