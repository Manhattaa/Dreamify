using System.Net.Http.Json;

namespace DreamifyClient
{
    internal class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5094/";
        private static int selectedOption = 1;

        public static void Main()
        {
            MenuFunctions.main_header();

            while (true)
            {

                Console.Clear();
                MenuFunctions.header();
                Console.WriteLine("\t\t  Choose an option:");
                MenuFunctions.footer();

                for (int i = 1; i <= 5; i++)
                {
                    Console.ForegroundColor = (i == selectedOption) ? ConsoleColor.DarkMagenta : ConsoleColor.White;
                    Console.WriteLine($"{GetMenuText(i)}");
                }

                Console.ResetColor();

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                HandleKeyPress(keyInfo);
            }
        }

        static string GetMenuText(int option)
        {
            switch (option)
            {
                case 1:
                    return "\t\t  Get a user from the API";
                case 2:
                    return "\t\t  Add new user";
                case 3:
                    return "\t\t  List and add genres, artists, and songs";
                case 4:
                    return "\t\t  Search for song";
                case 5:
                    return "\t\t  Exit";
                default:
                    return "\t\t  Invalid Option";
            }
        }

        static void HandleKeyPress(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = Math.Max(1, selectedOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = Math.Min(5, selectedOption + 1);
                    break;
                case ConsoleKey.Enter:
                    HandleSelection(selectedOption);
                    break;
            }
        }

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

        static async void HandleSelection(int selectedOption)
        {
            switch (selectedOption)
            {
                case 1:
                    GetUser().Wait();
                    break;
                case 2:
                    AddUser().Wait();
                    break;
                case 3:
                    ListAndAddGenresArtistsSongs();
                    break;
                case 4:
                    await SpotifyFunctions.SaveSongToDb();
                    break;
                case 5:
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




