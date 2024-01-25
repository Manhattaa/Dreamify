using System.Net.Http.Json;

namespace DreamifyClient
{
    internal class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5094/";
        private static int selectedOption = 1;

        public static async Task Main(string[] args)
        {
            MenuFunctions.main_header();

            // Before user gets to select option, give user 2 options. Login or create user. 
            // Login if user exists else create user before we do anything. We need the user and its id to 
            // Connect it to the db. This should just be text that is sent in a api post to our api add user
            int selection = NavFunctions.NavigationMain();
            await HandleSelection(selection);
            
        }

        // Move these to separate method
        static async Task GetUser()
        {
            Console.Write("Enter the user ID to select: ");
            int userId = int.Parse(Console.ReadLine());

            // Make an API request to get user details
            HttpResponseMessage response = await httpClient.GetAsync($"{apiUrl}users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                string userJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Selected user details:\n{userJson}");
            }
            else
            {
                Console.WriteLine($"Failed to get user details. Status code: {response.StatusCode}");
            }
        }

        static async Task AddUser()
        {
            Console.Write("Enter the username for the new user: ");
            string username = Console.ReadLine();

            // Make an API request to create a new user
            var newUser = new { Username = username };
            HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{apiUrl}users", newUser);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User created successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to create user. Status code: {response.StatusCode}");
            }
        }

        static void ListAndAddGenresArtistsSongs()
        {
            // Implement logic for listing and adding genres, artists, and songs
            Console.WriteLine("List and add genres, artists, and songs functionality goes here.");
        }

        static async Task HandleSelection(int selectedOption)
        {
            switch (selectedOption)
            {
                case 0:
                    GetUser().Wait();
                    break;
                case 1:
                    AddUser().Wait();
                    break;
                case 2:
                    ListAndAddGenresArtistsSongs();
                    break;
                case 3:
                    await SpotifyFunctions.SongSearch();
                    break;
                case 4:
                    Console.WriteLine("Exiting the client. Goodbye!");
                    Environment.Exit(0);
                    break;
            }
        }

        static void CallOtherApiEndpoints()
        {
            // Implement logic for calling other API endpoints
            Console.WriteLine("Call other API endpoints functionality goes here.");
        }
    }
}