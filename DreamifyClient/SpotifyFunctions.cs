using DreamifyClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DreamifyClient
{
    public static class SpotifyFunctions
    {   
         
        private static HttpClient httpClient = new HttpClient();
        private static string apiUrl = "http://localhost:5094"; //change this to some global variable so we don't need to re-declare it in each class

        public static async Task SaveSongToDb()
        {
            // Ensure correct search
            string search;
            while (true)
            {
                await Console.Out.WriteAsync("Enter search: ");
                search = Console.ReadLine();

                if (!string.IsNullOrEmpty(search))
                    break;

                Console.WriteLine("Invalid search. Try again.");
            }


            HttpResponseMessage response = await httpClient.GetAsync($"{apiUrl}/search/song/{search}");
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<SongSearchViewModel>? result = JsonSerializer.Deserialize<List<SongSearchViewModel>>(jsonResponse);


            await Console.Out.WriteLineAsync($"Here are the top results for \"{search}\"");


            //Dictionary<string, string> songs = new Dictionary<string, string>();

            List<string> songList = new List<string>();
            List<string> songIdList = new List<string>();
            List<string> alreadyShownArtists = new List<string>(); // List of all artists shown to keep track so no duplicate artists
            foreach (SongSearchViewModel song in result)
            {
                // Print out info if the list doesn't already contain current artist
                if (!alreadyShownArtists.Contains(song.Artist.ArtistName))
                {   
                    //await Console.Out.WriteLineAsync($"Song: {song.SongName} \nArtist: {song.Artist.ArtistName}\n");

                    alreadyShownArtists.Add(song.Artist.ArtistName);
                    songList.Add(song.SongName);
                }
            }

            //string[] choiceOptions = /*songList*/.ToArray();
            int selection = NavFunctions.Navigation(songList,alreadyShownArtists, "Press [Enter] on the song you want to save");



        }



        



    }
}
