using DreamifyClient.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;

namespace DreamifyClient
{
    public class Program
    {       
        // Declaring user id to be set and used later
        private static int _userId;

        public static async Task Main(string[] args)
        {
            // Removes so the blinking cursor isn't visible
            Console.CursorVisible = false;

            MenuFunctions.main_header();

            while (true)
            {
                string[] options = { "Choose existing user", "Create new user" };
                int selection = NavFunctions.NavigationGeneric(options, "Select an option:");

                if (selection == 0) // Choose existing user
                {
                    List<UsersIdViewModel> allUsersList = await ApiFunctions.GetAllUsers();
                    if (allUsersList != null)
                    {
                        List<string> usersString = new List<string>();
                        foreach (UsersIdViewModel ul in allUsersList)
                        {
                            usersString.Add(ul.Username);
                            await Console.Out.WriteLineAsync($"Username: {ul.Username}");
                        }

                        string[] userOptions = usersString.ToArray();
                        int userSelection = NavFunctions.NavigationGeneric(userOptions, "Select a user:");

                        // If selection is 1 more than the options it is Exit - exit to previous menu
                        if (userSelection == allUsersList.Count)
                            continue;
                        
                        _userId = allUsersList.ElementAt(userSelection).UserId;
                        break;
                    }
                }
                else if (selection == 1) // Create new user
                {
                    ApiFunctions.AddUser();
                }
                else // Exit
                    Environment.Exit(1);
            }

            // Loop until user exits
            while (true)
            {
                int selection = NavFunctions.NavigationMain();
                await HandleSelection(selection);
            }
        }


        static async Task HandleSelection(int selectedOption)
        {
            switch (selectedOption)
            {
                case 0:
                    await ApiFunctions.ListSongs(); 
                    break;
                case 1:
                    await ApiFunctions.ListArtists(); 
                    break;
                case 2:
                    await ApiFunctions.ListGenres();
                    break;
                case 3:
                    await ApiFunctions.AddSong(); // Error occurs
                    break;
                case 4:
                    await ApiFunctions.AddArtist(); 
                    break;
                case 5:
                    await ApiFunctions.AddGenre(); // Press enter missing
                    break;
                case 6:
                    await SpotifyFunctions.AddSongViaSearch(_userId);
                    break;
                case 7:
                    await SpotifyFunctions.AddArtistViaSearch(_userId); // NOT IMPLEMENTED
                    break;
                case 8:
                    Console.WriteLine("\t\t  Exiting the client. Goodbye!");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}