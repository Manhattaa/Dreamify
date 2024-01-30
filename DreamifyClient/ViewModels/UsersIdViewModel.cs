using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DreamifyClient.ViewModels
{
    public class UsersIdViewModel
    {
        [JsonPropertyName("id")]
        public int UserId { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
