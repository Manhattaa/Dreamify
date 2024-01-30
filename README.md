<img src="https://github.com/Manhattaa/Dreamify/blob/master/Dreamify.jpg" alt="Dreamify Logo" width="900" height="700">

# Dreamify™
Welcome to Dreamify. A project that involves creating a music themed Minimal API that uses the [Spotify Open Access API](https://developer.spotify.com/documentation/web-api)

## Table of Contents
 - [Introduction](#dreamify)
 - [Features](#features)
 - [Getting started](#getting-started)
 - [Endpoints](#endpoints)
 - [References](#references)
 - [Built with](#built-with)
 - [Meet Dreamify](#meet-dreamify)


## Features

- **User Management**: *Store users with unique usernames*.
- **Interest Tracking**: *Users can express their interests in genres, artists and songs. This information is stored locally, prompting for a personalized music experience*.
- **Data Retrieval**: *The API offers various endpoints for retrieving information. see [Endpoints](#endpoints) for further information*.
- **Connection Establishment**: *Users can connect themselves to genres, artists and songs by using dedicated endpoints*.

## Getting started

Using the following instructions will get you the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version .NET 6)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/):
  * EntityFrameworkCore (version 6.0.25)
  * EntityFrameworkCore.SqlServer (version 6.0.25)
  * EntityFrameworkCore.Tools (version 6.0.25)

- An API development program such as [Postman](https://www.postman.com//). [Docker](https://www.docker.com/), [Swagger](https://swagger.io/), or our personal choice: [Insomnia](https://insomnia.rest/)

> [!NOTE]
> _The project was developed using Visual Studio, but you can use your preferred IDE.
> Similarly, you can use your own SQL Server instance, ensuring compatibility with Entity Framework Core._


## Installing the project

A step by step series of examples that tell you how to get the app upp and running.

1. Clone the repository (either with git bash or using the repository link).

```bash
git clone https://github.com/Manhattaa/Dreamify.git

```

2. Navigate to the project directory (if using git bash)

```bash
cd Dreamify
```

3. Build the project (if using git bash)

```bash
dotnet build
```

## Endpoints
### GET endpoints

**Users endpoints**
* /users - **Get all Users**

_Ex: http://localhost:5094/users_
* /users/{userId} - **Get a specific user**

_Ex: http://localhost:5094/users/4_
* /users-and-id - **Gets all users names and id's**

_Ex: http://localhost:5094/users-and-id_

 **Songs endpoints**
* /songs - **Get all songs**

_Ex: http://localhost:5094/songs_
* /users/{userId}/songs - **Get all songs connected to a specific user**

 _Ex: http://localhost:5094/users/4_

 **Artists endpoints**
 * /artists - **Get all artists**

_Ex: http://localhost:5094/artists_

 **Genres endpoints**
* /genres - **Get all genres**

_Ex: http://localhost:5094/genres_

* /search/song/{search}/{offset?}/{countryCode?} - **Get a list of songs via search query from Spotify's database**

_Ex: http://localhost:5094/search/song/Thunderstruck_

  Output: Top 10 search results
  
  JSON structure:
  
  "string" : "SpotifySongId"
  
  "string" : "SongName"
  
  "List<Artists>" : "Artists" \[
  
  "string" : "SpotifyArtistId"
  
  "string" : "ArtistName" \]
* /search/artist/{search}/{offset?}/{countryCode?} - **Get a list of artists via search query from Spotify's database**

  _Ex:_(http://localhost:5094/search/artist/Taylor Swift/3/SE)

  Output: Top 10 search results skipping the top 3 filtered for the Swedish market
  
  JSON structure:
  
  "string" : "SpotifySongId"
  
  "string" : "SongName"
  
  "List<Artists>" : "Artists" \[
  
  "string" : "SpotifyArtistId"
  
  "string" : "ArtistName" \]
  
### POST endpoints
* /users - **Add a user with JSON**

  _Ex: http://localhost:5094/users_

  JSON structure: "string" : "Username"
* /songs - **Add song with JSON**

   _Ex: http://localhost:5094/songs_

  JSON structure: "string" : "Title"
* /artists - **Add artists with JSON**

  _Ex: http://localhost:5094/artsts_

  JSON structure:

  
  "string" : "Title"

  
  "string?" : "Description"

  
  "List\<Genre\?" : "Genres"

* /artists - **Add artists with JSON** _Ex: http://localhost:5094/artsts_

    JSON structure: "string" : "Title"
  
* /users/{userId}/artists/{artistId} - **Link a Specific User and Artist**

_Ex: http://localhost:5094/users/4/artists/6_
* /users/{userId}/genres/{genreId} - **Link a Specific User to a Genre**

_Ex: http://localhost:5094/users/11/genres/7_
* /users/{userId}/songs/{songId} - **Link a Specific User and Song*** 

_Ex: http://localhost:5094/users/3/songs/9_
* /users/add-spotify-song - **Connects a song with the spotify song and artist id to the users**


  _Ex: http://localhost:5094/users/add-spotify-song_

  Json structure:
  
  "int" : "UserID"
  
  "string" : "SongName"
  
  "string" : "SpotifySongId"
  
  "string" : "ArtistName"
  
  "string" : "SpotifyArtistId"

## Credit & References
- [Spotify Open Access API](https://developer.spotify.com/documentation/web-api)

## Built with
* C# - Programming language
* MS SQL - Database
* Entity Framework - ORM
* ASP.NET Core

## Meet Dreamify
* **Fady Hatta** - [Manhattaa](https://github.com/Manhattaa)
* **Adrian Rozsahegyi** - [Adrozs](https://github.com/Adrozs)
* **Malin Nyberg** - [MalinNyberg](https://github.com/MalinNyberg)
* **Fredrich Benedetti** - [Shakejelly](https://github.com/Shakejelly)
