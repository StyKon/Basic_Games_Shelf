using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Basic_Games_Shelf.DATA.IServices;
using Basic_Games_Shelf.DATA.Result;
using Basic_Games_Shelf.DATA.Services;
using Basic_Games_Shelf.DOMAINE;
using FluentAssertions;
using Moq;
using Xunit;

namespace Basic_Games_Shelf.DATATEST
{
    public class GamesServiceTests
    {
        [Theory, AutoMoqData]
        public void GetGames_Should_Return_Task_IEnumerable_Games([Frozen] Mock<IGamesService> service,  List<Games> games, GamesService sut)
        {
            service.Setup(c => c.GetGames()).ReturnsAsync(games.AsQueryable);
            Action action =  () =>
            {
                var result =  sut.GetGames(); 
                Assert.IsType<Task<IEnumerable<Games>>>(result);


            };
            action.Should().NotThrow<Exception>();

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