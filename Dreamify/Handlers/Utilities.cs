namespace Dreamify.Handlers
{
    public class Utilities
    {
        public static IResult ErrorHandling(Exception ex)
        {
            return Results.Text($"404: Not Found! {ex.Message}");
        }
    }
}
