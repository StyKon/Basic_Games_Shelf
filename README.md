# Clean Architecture

Creating maintainable application in ASP.NET Core requires a solid foundation So I've tried to create this project with clean architecture.

# Key Points

###### N-Tier Architecture
###### Testable
###### Maintainable

# Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [Code Generation](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design/)
* [Fluent Assertions](https://fluentassertions.com/)
* [xUnit](https://xunit.net/), [Autofixture](https://autofixture.github.io/) & [Moq](https://github.com/moq)

# Layers

**Domain:** This will contain all entities, types and logic specific to the domain layer.

**Data:** This layer contains all application logic and contains classes for accessing external resources.

**WebApi:** This layer is a web api application based on ASP.NET Core 6 .

# Database Migrations

You can use this Commande to create Migrations

```powershell
Add-Migration NewMigration
```

And this  Commande to create the database 

```powershell
Update-Database
```

# Database Configuration

If you would like to Connect to your SQL Server, you will need to update WebApi/appsettings.json

```json
  "ConnectionStrings": {
        "BasicGamesShelfContext": "SqlServer"
    }
```
# About My Solution

You can test the application using Swagger

These are all urls in the app and Their request method.

Get: BaseUrl/api/Games  => retrive all games without Id

GET: BaseUrl/api/Games/GamesWithDetails  => retrive all games with ids (if you want to delete a filed from data base and you dont know id) 

GET: BaseUrl/api/Games/{id} =>  retrive single game

PUT: BaseUrl/api/Games/{id} => update game with specefic id

POST: BaseUrl/api/Games =>  add single game 

POST: BaseUrl/api/Games/AddMultipleGames =>  adding multiple games you can use json file

Example :

```json
   [
    {
      "userId": 8,
      "game": "League of legends",
      "playTime": 500,
      "genre": "MOBA",
      "platforms": ["PC"]
    },
    {
      "userId": 7,
      "game": "World of warcraft",
      "playTime": 1500,
      "genre": "MMORPG",
      "platforms": ["PC"]
    },
    {
      "userId": 88,
      "game": "Dark Souls",
      "playTime": 109,
      "genre": "Action RPG",
      "platforms": [
        "PS3",
        "Xbox 360",
        "PC",
        "PS4",
        "Xbox One",
        "Nintendo Switch"
      ]
    },
   ]
```

DELETE: BaseUrl/api/Games/{id} => delete game 

GET: BaseUrl/api/Games/select_top_by_playtime?genre=FPS&platform=PC  => return the most played games 

GET: BaseUrl/api/Games/select_top_by_players?genre=FPS&platform=PC  => return the games with the highest number of unique players

I tried to make the application real, so I thought of potential edge cases and try to repair it.

# Contact

Email : khalilfrikha30@gmail.com
Phone : 54647661









