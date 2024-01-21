using Dreamify.Models.ViewModels.DreamifyViewModels;
using static Dreamify.Models.Spotify;

namespace Dreamify.Models.ViewModels.SpotifyViewModels.Player_ViewModels;

public class CurrentlyPlayingTrackResponseViewModel
{
    public Device Device { get; set; }
    public string RepeatState { get; set; }
    public bool ShuffleState { get; set; }
    public int Timestamp { get; set; }
    public int ProgressMs { get; set; }
    public bool IsPlaying { get; set; }
    public ItemViewModel Item { get; set; }
    public string CurrentlyPlayingType { get; set; }
    public ActionsViewModel Actions { get; set; }
    }


