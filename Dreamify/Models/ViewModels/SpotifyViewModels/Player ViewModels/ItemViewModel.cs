using Dreamify.Models.ViewModels.DreamifyViewModels;

namespace Dreamify.Models.ViewModels.SpotifyViewModels
{
    public class ItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ArtistsViewModel> Artists { get; set; }


        public ItemViewModel(string id, string name, List<ArtistsViewModel> artists)
        {
            // Initialize properties based on parameters
            Id = id;
            Name = name;
            Artists = artists;
        }
    }
}
