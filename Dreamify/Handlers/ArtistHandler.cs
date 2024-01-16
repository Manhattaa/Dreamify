﻿using Dreamify.Models.Dtos;
using Dreamify.Services;
using System.Net;
using Dreamify.Data;
using Dreamify.Models;

namespace Dreamify.Handlers
{
    public class ArtistHandler
    {
        // Genres

        // Artists

        public static IResult AddArtist(ApplicationContext context, string personId, ArtistDto artistDto)
        {
            try
            {
                Artist artist = new Artist
                {
                    Name = artistDto.Name,
                    Description = artistDto.Description
                };
                context.Artists.Add(artist);
                context.SaveChanges();
                return Results.Ok($"Artist {artist.Name} has been added to the database.");
            }
            catch (Exception ex)
            {
                return Results.Text($"404: Not found! {ex}");
            }
        }
        public static IResult GetArtist(IArtistsHelper artistHelper)
        {
            artistHelper.GetArtists();
            return Results.StatusCode((int)HttpStatusCode.OK);
        }

        // Post songs
        public static IResult AddSong(int artistId, int genreId, SongDto song, ISongsHelper songHelper)
        {
            songHelper.AddSong(artistId, genreId, song);
            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult GetSongs(ISongsHelper songHelper)
        {
            return Results.Json(songHelper.GetSongs());
        }

        public static IResult GetUserSongs(int userId, ISongsHelper songHelper)
        {
            return Results.Json(songHelper.GetUserSongs(userId));
        }
    }
}
