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
  * EntityFrameworkCore (version 6.0.24)
  * EntityFrameworkCore.SqlServer (version 6.0.24)
  * EntityFrameworkCore.Tools (version 6.0.24)

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
**GET** endpoints

* /users - **Get All Users**
* /user/{userId} - **Get a Specific User**
* /user/{userId}/artists - **Get a Specific User and Artists**
* /user/{userId}/genres - **Get a Specific User and Interests**
* /user/{userId}/songs - **Get a Specific User and Tracks**

  
* /artists - **Get all Artists**
* /songs - **Get all Songs**

**POST** endpoints

* /user/{userId}/artist/{artistId} - **Link a Specific User and Artist**
* /user/{userId}/genre/{genreId} - **Link a Specific User to a Genre**
* /user/{userId}/tracks/{songId} - **Link a Specific User and Song**


* /artists/{artistId}/genre/{genreId}/songs
* /artists/

## References
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
