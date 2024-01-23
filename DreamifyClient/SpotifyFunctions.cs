using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamifyClient
{
    public class SpotifyFunctions
    {
        private HttpClient httpClient = new HttpClient();
        private string apiUrl = "http://localhost:5094"; //change this to some global variable so we don't need to re-declare it in each class

        public async Task<string> SaveSongToDb(string search)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{apiUrl}/search/song/{search}");
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
        }



        



    }
}
