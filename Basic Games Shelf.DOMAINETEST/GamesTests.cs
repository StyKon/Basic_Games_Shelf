
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Basic_Games_Shelf.DOMAINE;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Basic_Games_Shelf.DOMAINETEST
{
    public class GamesTests
    {
        [Theory, AutoMoqData]
        public void Create_Games_Should_Not_Work_When_Game_Is_Empty(Games game)
        {
            Games games = new Games { UserId = game.UserId, Game = string.Empty, PlayTime = game.PlayTime, Genre = game.Genre, Platforms = game.Platforms };
            var listErrors = ValidateModel(games);
            Assert.False(listErrors.Count() == 0);
        }
        [Theory, AutoMoqData]
        public void Create_Games_Should_Not_Work_When_Genre_Is_Empty(Games game)
        {
            Games games = new Games { UserId = game.UserId, Game = game.Game, PlayTime = game.PlayTime, Genre = string.Empty, Platforms = game.Platforms };
            var listErrors = ValidateModel(games);
            Assert.False(listErrors.Count() == 0);
        }
        [Theory, AutoMoqData]
        public void Create_Games_Should_Not_Work_When_Platforms_Is_Empty(Games game)
        {
            Games games = new Games { UserId = game.UserId, Game = game.Game, PlayTime = game.PlayTime, Genre = game.Genre, Platforms = new string[] { } };
            var listErrors = ValidateModel(games);
            Assert.False(listErrors.Count() == 0);
        }
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }

    }

    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}