using DreamifyClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DreamifyClient
{
    public class ApiFunctions
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _apiUrl = "http://localhost:5094";

        public static async Task GetUser()
        {
            Console.Write("\t\t  Enter the user ID to select: ");
            int userId = int.Parse(Console.ReadLine());

            // Make an API request to get user details
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                string userJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\t\t  Selected user details:\n{userJson}");
            }
            else
            {
                Console.WriteLine($"\t\t  Failed to get user details. Status code: {response.StatusCode}");
            }
        }

        // ERROR! NEED TO FIX. TIMES OUT DOESN'T WORK ATM
        public static async Task<List<UsersViewModel>> GetAllUsers()
        {
            List<UsersViewModel> users = null;

            try
            {
                // Make an API request to get all user details
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/users");
                response.EnsureSuccessStatusCode();
                
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Here users are correct like this:

                /*[
	                    {
		                    "username": "user1"
	                    },
	                    {
		                    "username": "user2"
	                    },
	                    {
		                    "username": "user3"
	                    }
                  ]*/


                users = JsonSerializer.Deserialize<List<UsersViewModel>>(jsonResponse);

                // DEBUG PRINT
                foreach (UsersViewModel u in users)
                {
                    await Console.Out.WriteLineAsync($"USERS CLIENT: {u.Username}");
                }
                Thread.Sleep(2000);
                await Console.Out.WriteLineAsync("End of user list from client");

                // Here users are null (or at least prints out blank)
                // But the users list is correct amount when doing users.count 

                if (users.Count == 0 || users == null)
                {
                    await Console.Out.WriteLineAsync("\t\t  No users were found");
                    throw new ArgumentNullException("No users were found");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t  ERROR! {ex.Message}");
            }
            return users;
        }

        public static async Task AddUser()
        {
            Console.Write("\t\t  Enter the username for the new user: ");
            string username = Console.ReadLine();

            // Make an API request to create a new user
            var newUser = new { Username = username };
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/users", newUser);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\t\t  User created successfully.");
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine($"\t\t  Failed to create user. Status code: {response.StatusCode}");
            }
        }

        public static void ListAndAddGenresArtistsSongs()
        {
            // Implement logic for listing and adding genres, artists, and songs
            Console.WriteLine("List and add genres, artists, and songs functionality goes here.");
        }

        public static void CallOtherApiEndpoints()
        {
            // Implement logic for calling other API endpoints
            Console.WriteLine("Call other API endpoints functionality goes here.");
        }

        internal static void ListGenres()
        {
            throw new NotImplementedException();
        }

        internal static void ListArtists()
        {
            throw new NotImplementedException();
        }

        internal static void ListSongs()
        {
            throw new NotImplementedException();
        }
    }
}
