namespace Dreamify.Models.ViewModels
{
    public class ArtistsViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<SongsViewModel> Title { get; set;}
    }
}
