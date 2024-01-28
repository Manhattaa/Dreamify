using DreamifyClient.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;

namespace DreamifyClient
{
    internal class Program
    {
        //private static readonly HttpClient _httpClient = new HttpClient();
        //private static readonly string _apiUrl = "http://localhost:5094/";
        //private static int _selectedOption = 1;
        
        //temp userid for testing
        private static int _userId = 1;

        public static async Task Main(string[] args)
        {
            MenuFunctions.main_header();

            // Before user gets to select option, give user 2 options. Login or create user. 
            // Login if user exists else create user before we do anything. We need the user and its id to 
            // Connect it to the db. This should just be text that is sent in a api post to our api add user

            string user;

            while (true)
            {
                string[] options = { "Choose existing user", "Create new user" };
                int selection = NavFunctions.NavigationGenericArray(options, "Select an option:");

                // Show all users and let use choose (DOES NOT WORK CONNECTION TIMED OUT ERROR!!!)
                if (selection == 0)
                {
                    List<UsersViewModel> allUsersList = await ApiFunctions.GetAllUsers();
                    if (allUsersList != null)
                    {
                        List<string> usersString = new List<string>();
                        foreach (UsersViewModel ul in allUsersList)
                        {
                            usersString.Add(ul.Username);
                            await Console.Out.WriteLineAsync($"Username: {ul.Username}");
                        }

                        string[] userOptions = usersString.ToArray();
                        int userSelection = NavFunctions.NavigationGenericArray(userOptions, "Select a user:");

                        // DEBUG PRINTS
                        Console.WriteLine("INFO:");
                        Console.WriteLine(userOptions);
                        Console.WriteLine(userSelection);
                        Thread.Sleep(2000);

                        // If selection is 1 more than the options it is Exit - exit to previous menu
                        if (userSelection == allUsersList.Count + 1)
                            break;
                        
                        user = allUsersList.ElementAt(userSelection).Username;
                        await Console.Out.WriteLineAsync(user);
                        break;
                    }
                }
                // Create new user
                else if (selection == 1)
                {
                    ApiFunctions.AddUser();
                }
                else
                    break; // for testing purposes remove when it works
                    //Environment.Exit(1);
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
                    ApiFunctions.ListSongs(); // NOT IMPLEMENTED
                    break;
                case 1:
                    ApiFunctions.ListArtists(); // NOT IMPLEMENTED
                    break;
                case 2:
                    ApiFunctions.ListGenres(); // NOT IMPLEMENTED
                    break;
                case 3:
                    await SpotifyFunctions.AddSongViaSearch(_userId);
                    break;
                case 4:
                    await SpotifyFunctions.AddArtistViaSearch(_userId); // NOT IMPLEMENTED
                    break;
                case 5:
                    Console.WriteLine("\t\t  Exiting the client. Goodbye!");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}