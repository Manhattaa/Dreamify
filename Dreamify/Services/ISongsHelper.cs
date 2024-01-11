using Dreamify.Data;

namespace Dreamify.Services
{
  
    public interface ISongsHelper
    {
        public IResult AddSongToDb(ApplicationContext context);
            
    }

    public class SongsHelper : ISongsHelper
    {
        public IResult AddSongToDb(ApplicationContext context)
        {
            return Results.Ok();
        }

    }
    
}
