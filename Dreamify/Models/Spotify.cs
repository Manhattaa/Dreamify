using static Dreamify.Models.Spotify;

namespace Dreamify.Models
{
    public class Spotify
    {
        public class Device
        {
            public string Id { get; set; }
            public bool IsActive { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public int VolumePercent { get; set; }
        }

        public class Artist
        {
            public Followers Followers { get; set; }
            public string[] Genres { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public int Popularity { get; set; }
        }

        public class Followers
        {
            public int total { get; set; }
        }

        public class Item
        {
            public Artist[] Artists { get; set; }
            public string[] AvailableMarkets { get; set; }

            public string Id { get; set; }
            public bool IsPlayable { get; set; }
            public string Name { get; set; }
            public int Popularity { get; set; }
            public string Type { get; set; }
        }

        public class Actions
        {
            public bool Pausing { get; set; }
            public bool Resuming { get; set; }
            public bool Seeking { get; set; }
            public bool SkippingNext { get; set; }
            public bool SkippingPrev { get; set; }
            public bool TogglingShuffle { get; set; }
            public bool TogglingRepeatTrack { get; set; }
        }

        public class CurrentlyPlayingTrackResponse
        {
            public Device Device { get; set; }
            public string RepeatState { get; set; }
            public int Timestamp { get; set; }
            public int ProgressMs { get; set; }
            public bool IsPlaying { get; set; }
            public Item Item { get; set; }
            public string CurrentlyPlayingType { get; set; }
            public Actions Actions { get; set; }
        }
    }
}
