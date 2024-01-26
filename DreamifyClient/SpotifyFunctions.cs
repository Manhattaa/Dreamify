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

        private static HttpClient httpClient = new HttpClient();
        private static string apiUrl = "http://localhost:5094"; //change this to some global variable so we don't need to re-declare it in each class

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


            HttpResponseMessage response = await httpClient.GetAsync($"{apiUrl}/search/song/{search}");
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
            response = await httpClient.PostAsync($"{apiUrl}/users/add-spotify-song", content);

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


            HttpResponseMessage response = await httpClient.GetAsync($"{apiUrl}/search/artist/{search}");
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<SongSearchViewModel>? result = JsonSerializer.Deserialize<List<SongSearchViewModel>>(jsonResponse);


            await Console.Out.WriteLineAsync($"\t\t  Here are the top results for \"{search}\"");
            MenuFunctions.divider();


            // List of artists to be displayed for user
            List<SongSearchViewModel> genreList = new List<SongSearchViewModel>();
            List<SongSearchViewModel> artistList = new List<SongSearchViewModel>();


            // List of all artists shown to keep track so no duplicate artists is shown
            List<string> alreadyShownArtists = new List<string>();
            foreach (SongSearchViewModel artist in result)
            {
                // INCLUDE Genre
                if (!alreadyShownArtists.Contains(artist.Artist.ArtistName))
                {

                    alreadyShownArtists.Add(artist.Artist.ArtistName);
                    artistList.Add(artist.Artist);
                    //genreList.Add(artist.Genre);
                }
            }

            // Save selected artist number
            int selection = NavFunctions.NavigationSongSearch(genreList, artistList, "Press [Enter] on the song you want to save");
            MenuFunctions.footer();

            // If selection is 1 out of range of the list, then exit was chosen. Exit the method
            if (selection == genreList.Count)
            {
                await Console.Out.WriteLineAsync("\t\t  Exiting song search");
                Thread.Sleep(1000);
                return;
            }

            // Get the selected song
            SongSearchViewModel selectedArtist = genreList.ElementAt(selection);


            // Create DTO to send to the DreamifyAPI
            SpotifyArtistDto artistDto = new SpotifyArtistDto
            {
                UserId = userId,
                 = selectedArtist.Artist.ArtistName,
                SpotifyArtistId = selectedArtist.Artist.ArtistId,
                Genres = selectedArtist.Genre.Title
            };


            // Serialize the object to JSON
            string jsonRequestData = JsonSerializer.Serialize(artistDto);

            // Create StringContent with the serialized JSON data
            var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

            // Send content to the API
            response = await httpClient.PostAsync($"{apiUrl}/users/add-spotify-artist", content);

            // Log the response content
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"\t\t  API Response: {responseContent}");

            response.EnsureSuccessStatusCode();
        }
    }
}
