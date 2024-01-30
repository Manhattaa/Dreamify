using DreamifyClient.Dtos;
using DreamifyClient.ViewModels;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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
            MenuFunctions.PressEnter();

        }

        public static async Task<List<UsersIdViewModel>> GetAllUsers()
        {
            List<UsersIdViewModel> users = null;

            try
            {
                // Make an API request to get all user details
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/users-and-id");
                response.EnsureSuccessStatusCode();
                
                var jsonResponse = await response.Content.ReadAsStreamAsync();
                users = await JsonSerializer.DeserializeAsync<List<UsersIdViewModel>>(jsonResponse);

                if (users.Count == 0 || users == null)
                {
                    await Console.Out.WriteLineAsync("\t\t  No users were found");
                    throw new ArgumentNullException("No users were found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t  ERROR! {ex.Message}");
                MenuFunctions.PressEnter();
            }
            return users;
        }

        public static async Task AddUser()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.CursorVisible = true;

                Console.Write("\t\t  Enter the username for the new user: ");
                string username = Console.ReadLine();

                Console.CursorVisible = false;
                Console.ResetColor();
                MenuFunctions.divider();


                // Make an API request to create a new user
                var newUser = new { Username = username };
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/users", newUser);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\t\t  User created successfully.");
                }
                else
                {
                    Console.WriteLine($"\t\t  Failed to create user. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t  An error occurred: {ex.Message}");
            }

            MenuFunctions.PressEnter();
        }

        public static async Task ListGenres()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/genres");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStreamAsync();

                List<GenresViewModel>? genres = await JsonSerializer.DeserializeAsync<List<GenresViewModel>>(jsonResponse);

                if (genres == null || genres.Count == 0)
                {
                    await Console.Out.WriteLineAsync("\t\t  No genres were found");
                    throw new ArgumentNullException("No genres were found");
                }

                // Print out all genres
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                await Console.Out.WriteLineAsync("\t\t  Genres:");
                Console.ResetColor();

                MenuFunctions.divider();
                foreach (GenresViewModel genre in genres)
                {
                    await Console.Out.WriteLineAsync($"\t\t  {genre.Title}");
                }
                MenuFunctions.footer();

                MenuFunctions.PressEnter();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t  An error occurred: {ex.Message}");
                MenuFunctions.PressEnter();
            }
        }

        public static async Task AddGenre()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"\t\t  Enter the following details for a new genre: ");
                Console.ResetColor();
                MenuFunctions.divider();

                Console.CursorVisible = true;

                Console.Write($"\t\t  Title: ");
                string title = Console.ReadLine();

                Console.CursorVisible = false;

                // Create DTO to send to the DreamifyAPI
                GenreDto genreDto = new GenreDto()
                {
                    Title = title
                };

                // Serialize the object to JSON
                string jsonRequestData = JsonSerializer.Serialize(genreDto);

                // Create StringContent with the serialized JSON data
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                // Send content to the API
                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiUrl}/genres", content);
                response.EnsureSuccessStatusCode();


                Console.WriteLine("\t\t  Genre created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t  An error occurred: {ex.Message}");
            }

            MenuFunctions.PressEnter();
        }

        public static async Task ListArtists()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/artists");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStreamAsync();

                List<ArtistsViewModel>? artists = await JsonSerializer.DeserializeAsync<List<ArtistsViewModel>>(jsonResponse);

                if (artists == null || artists.Count == 0)
                {
                    await Console.Out.WriteLineAsync("\t\t  No artists were found");
                    throw new ArgumentNullException("No artists were found");
                }

                // Print out all artists
                Console.Clear();
                //Console.ForegroundColor = ConsoleColor.DarkMagenta;
                //await Console.Out.WriteLineAsync("\t\t  Artists: \t Description: \t Popularity:");
                Console.Write("\t");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Artists:");
                Console.ResetColor();
                Console.Write("\t");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Description:");
                Console.ResetColor();
                Console.Write("\t");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Popularity:");
                Console.ResetColor();
                
                Console.Write("\n");
                MenuFunctions.bigboydivider();
                foreach (ArtistsViewModel artist in artists)
                {
                    Console.WriteLine($"\t\t  {artist.ArtistName} - {artist.Description} - {artist.Popularity}\n ");

                    //If there is a description, print it out
                    if (artist.Description != null)
                    {
                        await Console.Out.WriteLineAsync($"\t\t  {artist.Description}");
                        await Console.Out.WriteLineAsync();
                    }

                }
                MenuFunctions.footer();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t  An error occurred: {ex.Message}");
            }

            MenuFunctions.PressEnter();
        }

        public static async Task AddArtist()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"\t\t  Enter the following details for a new artists: ");
                Console.ResetColor();
                MenuFunctions.divider();

                Console.CursorVisible = true;

                Console.Write($"\t\t  Name: ");
                string name = Console.ReadLine();

                Console.Write($"\t\t  Description (optional press [Enter] to skip): ");
                string description = Console.ReadLine();

                Console.CursorVisible = false;

                // Create DTO to send to the DreamifyAPI
                ArtistDto artistDto = new ArtistDto()
                {
                    ArtistName = name,
                    Description = description,
                    Popularity = null,
                    SpotifyArtistId = null,
                    Genre = null,
                };

                // Serialize the object to JSON
                string jsonRequestData = JsonSerializer.Serialize(artistDto);

                // Create StringContent with the serialized JSON data
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                // Send content to the API
                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiUrl}/artists", content);
                response.EnsureSuccessStatusCode();


                Console.WriteLine("\t\t  Artist created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t  An error occurred: {ex.Message}");
            }

            MenuFunctions.PressEnter();
        }

        public static async Task ListSongs()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/songs");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStreamAsync();

                List<SongsViewModel>? songs = await JsonSerializer.DeserializeAsync<List<SongsViewModel>>(jsonResponse);

                if (songs == null || songs.Count == 0)
                {
                    await Console.Out.WriteLineAsync("\t\t  No songs were found");
                    throw new ArgumentNullException("No songs were found");
                }

                // Print out all songs in the list
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                await Console.Out.WriteLineAsync("\t\t  Songs:");
                Console.ResetColor();

                MenuFunctions.divider();
                foreach (SongsViewModel song in songs)
                {
                    await Console.Out.WriteLineAsync($"\t\t  { song.Title}");
                }
            
                MenuFunctions.footer();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"\t\t  An error occurred: {ex.Message}");
            }

            MenuFunctions.PressEnter();
        }

        public static async Task AddSong()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"\t\t  Enter the following details for a new song: ");
                Console.ResetColor();
                MenuFunctions.divider();

                Console.CursorVisible = true;

                Console.Write($"\t\t  Title: ");
                string title = Console.ReadLine();

                Console.CursorVisible = false;

                // Create DTO to send to the DreamifyAPI
                SongsDto songsDto = new SongsDto()
                {
                    Title = title,
                };

                // Serialize the object to JSON
                string jsonRequestData = JsonSerializer.Serialize(songsDto);

                // Create StringContent with the serialized JSON data
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                // Send content to the API
                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiUrl}/songs", content);
                response.EnsureSuccessStatusCode();


                Console.WriteLine("\t\t  Song created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t  An error occurred: {ex.Message}");
            }

            MenuFunctions.PressEnter();
        }
    }
}
