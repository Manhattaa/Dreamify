using DreamifyClient.Dtos;
using DreamifyClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DreamifyClient
{
    public static class SpotifyFunctions
    {   
         
        private static HttpClient _httpClient = new HttpClient();
        private static string _apiUrl = "http://localhost:5094"; //change this to some global variable so we don't need to re-declare it in each class

        public static async Task AddSongViaSearch(int userId)
        {
            // Ensure correct search
            string search;
            while (true)
            {
                Console.Clear();
                await Console.Out.WriteAsync("\t\t  Enter search: ");
                search = Console.ReadLine();

                if (!string.IsNullOrEmpty(search))
                    break;

                Console.WriteLine("\t\t  Invalid search. Try again.");
            }


            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/search/song/{search}");
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<SongSearchViewModel>? result = JsonSerializer.Deserialize<List<SongSearchViewModel>>(jsonResponse);


            await Console.Out.WriteLineAsync($"\t\t  Here are the top results for \"{search}\"");
            MenuFunctions.divider();


            // List of songs and artist to be displayed for user
            List<SongSearchViewModel> songList = new List<SongSearchViewModel>();
            List<SongArtistViewModel> artistList = new List<SongArtistViewModel>();


            // List of all artists shown to keep track so no duplicate artists is shown
            List<string> alreadyShownArtists = new List<string>();
            foreach (SongSearchViewModel song in result)
            {
                // Print out info if the list doesn't already contain current artist
                if (!alreadyShownArtists.Contains(song.Artist.ArtistName))
                {
                    //await Console.Out.WriteLineAsync($"Song: {song.SongName} \nArtist: {song.Artist.ArtistName}\n");

                    alreadyShownArtists.Add(song.Artist.ArtistName);
                    artistList.Add(song.Artist);
                    songList.Add(song);
                }
            }

            // Save selected song number
            int selection = NavFunctions.NavigationSongSearch(songList, artistList, "Press [Enter] on the song you want to save");
            MenuFunctions.footer();

            // If selection is 1 out of range of the list, then exit was chosen. Exit the method
            if (selection == songList.Count)
            {
                await Console.Out.WriteLineAsync("\t\t  Exiting song search");
                Thread.Sleep(1000);
                return;
            }

            // Get the selected song
            SongSearchViewModel selectedSong = songList.ElementAt(selection);


            // Create DTO to send to the DreamifyAPI
            SpotifySongDto songDto = new SpotifySongDto
            {
                UserId = userId,
                SongName = selectedSong.SongName,
                SpotifySongId = selectedSong.SpotifySongId,
                ArtistName = selectedSong.Artist.ArtistName,
                SpotifyArtistId = selectedSong.Artist.ArtistId,
            };


            // Serialize the object to JSON
            string jsonRequestData = JsonSerializer.Serialize(songDto);

            // Create StringContent with the serialized JSON data
            var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

            // Send content to the API
            response = await _httpClient.PostAsync($"{_apiUrl}/users/add-spotify-song", content);

            // Log the response content
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"\t\t  API Response: {responseContent}");

            response.EnsureSuccessStatusCode();
        }

        public static async Task AddArtistViaSearch(int userId)
        {
            // Ensure correct search
            string search;
            while (true)
            {
                Console.Clear();
                await Console.Out.WriteAsync("\t\t  Enter search: ");
                search = Console.ReadLine();

                if (!string.IsNullOrEmpty(search))
                    break;

                Console.WriteLine("\t\t  Invalid search. Try again.");
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/search/artist/{search}");
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<SpotifyArtistsSearchViewModel>? result = JsonSerializer.Deserialize<List<SpotifyArtistsSearchViewModel>>(jsonResponse);

            await Console.Out.WriteLineAsync($"\t\t  Here are the top results for \"{search}\"");
            MenuFunctions.divider();

            // List of artists and genres to be displayed for the user ((WIP)
            List<SpotifyArtistsSearchViewModel> artistList = new List<SpotifyArtistsSearchViewModel>();

            // List of all artists shown to keep track so no duplicate artists are shown
            List<string> alreadyShownArtists = new List<string>();
            foreach (SpotifyArtistsSearchViewModel artist in result)
            {
                // Print out info if the list doesn't already contain the current artist
                if (!alreadyShownArtists.Contains(artist.ArtistName))
                {
                    alreadyShownArtists.Add(artist.ArtistName);
                    artistList.Add(artist);
                }
            }



            // Save selected artist number
            int selection = NavFunctions.NavigationArtistSearch(artistList, "Press [Enter] on the artist you want to save");
            MenuFunctions.footer();

            // If selection is out of the range of the list, then exit was chosen. Exit the method
            if (selection == artistList.Count)
            {
                await Console.Out.WriteLineAsync("\t\t  Exiting artist search");
                Thread.Sleep(1000);
                return;
            }

            Console.Write($"\t\t  Description (optional press [Enter] to skip): ");
            string description = Console.ReadLine();

            // Ensure that artistList is not empty before trying to access elements from it
            if (artistList.Count > 0)
            {
                // Get the selected artist
                SpotifyArtistsSearchViewModel selectedArtist = artistList.ElementAtOrDefault(selection);

                SpotifyArtistDto artistDto = new SpotifyArtistDto
                {
                    UserId = userId,
                    ArtistName = selectedArtist.ArtistName,
                    SpotifyArtistId = selectedArtist.SpotifyArtistId,
                    Description = description,
                    Popularity = selectedArtist.Popularity,
                    Genres = selectedArtist.Genre,
                };

                // Serialize the object to JSON
                string jsonRequestData = JsonSerializer.Serialize(artistDto);

                // Create StringContent with the serialized JSON data
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                // Send content to the API
                response = await _httpClient.PostAsync($"{_apiUrl}/users/add-spotify-artist", content);

                // Log the response content
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\t\t  API Response: {responseContent}");

                response.EnsureSuccessStatusCode();

                // Ensure that selectedArtist and its associated properties are not null before using them
                if (selectedArtist != null)
                {
                    Console.WriteLine($"Artist: {selectedArtist.ArtistName}");
                    Console.WriteLine($"Description: {description}");
                    Console.WriteLine($"Popularity: {selectedArtist.Popularity}%");
                    Console.WriteLine($"Followers: {selectedArtist.Followers}");
                    Console.WriteLine($"Genre: {(selectedArtist.Genre.Any() ? string.Join(", ", selectedArtist.Genre) : "No Genre Available")}");

                    Console.WriteLine($"\nThe Artist: {selectedArtist.ArtistName} has been added to the Database.");
                }
                else
                    {
                        Console.WriteLine("Selected artist or artist details are null. Exiting artist search.");
                        Thread.Sleep(3000);
                        Console.ReadKey();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("No artists to select. Exiting artist search.");
                    Thread.Sleep(3000);
                    return;
                }
            }
        }
    }
