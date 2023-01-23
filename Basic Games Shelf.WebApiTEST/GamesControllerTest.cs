using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Basic_Games_Shelf.DATA.Dto;
using Basic_Games_Shelf.DATA.IServices;
using Basic_Games_Shelf.DOMAINE;
using Basic_Games_Shelf.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Basic_Games_Shelf.WebApiTEST
{
    public class GamesControllerTest
    {
        private readonly Mock<IGamesService> _mockServ;
        private readonly GamesController _controller;
        public GamesControllerTest()
        {
            _mockServ = new Mock<IGamesService>();
            _controller = new GamesController(_mockServ.Object);
        }
        [Theory, AutoMoqData]
        public void Valid_Game_Post_Should_Not_Return_BadRequest(Games games)
        {
            Task<ActionResult<Games>> result = _controller.PostGames(games);
            Assert.IsNotType<BadRequestObjectResult>(result);

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