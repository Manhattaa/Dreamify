using Dreamify.Data;

namespace Dreamify.Services
{
    public interface IArtistsHelper
    {
        public IResult AddArtistToDb(ApplicationContext context);

        public IResult GetArtists(ApplicationContext context);
    }

    public class ArtistsHelper : IArtistsHelper
    {
        public IResult AddSongtoDb(ApplicationContext context)
        {
            return Results.Ok();
        }
    }
}
