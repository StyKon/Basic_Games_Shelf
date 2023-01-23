
using Basic_Games_Shelf.DATA.IServices;
using Basic_Games_Shelf.DATA.Response;
using Basic_Games_Shelf.DATA.Result;
using Basic_Games_Shelf.DOMAINE;
using Microsoft.EntityFrameworkCore;

namespace Basic_Games_Shelf.DATA.Services
{
    public class GamesService : IGamesService
    {
        private readonly BasicGamesShelfContext _context;
        public GamesService(BasicGamesShelfContext _context)
        {
            this._context = _context;
        }
        public async Task<Games?> DeleteGames(int id)
        {
            var games = await _context.Games.FindAsync(id);
            if (games == null)
            {
                return games;
            }

            _context.Games.Remove(games);
            await _context.SaveChangesAsync();

            return games;
        }

        public bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

        public async Task<Games> GetGames(int id)
        {
            var games = await _context.Games.FindAsync(id);

            return games;
        }

        public async Task<IEnumerable<Games>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<GamesResult> PostGames(Games games)
        {
            bool ValidInput = true;
            GamesResult gameResult = new GamesResult();
            if (GamesNameExist(games))
            {
                ValidInput = PlatformsAreTheSame(games);
                gameResult.Message = "You Cannot Add this game with different platforms";
                if (ValidInput)
                {
                    ValidInput = !UserAlreadyPlayingTheGame(games);
                    gameResult.Message = "You Cannot Add User that Already Playing This Game";
                }
            }
            if (ValidInput)
            {
                _context.Games.Add(games);
                await _context.SaveChangesAsync();
                gameResult.Games = games;
                gameResult.Message = "Games Added Successfully";
                return gameResult;
            }
            gameResult.Games = null;

            return gameResult;
        }



        public async Task<GamesResult> PutGames(int id, Games games)
        {
            _context.Entry(games).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            GamesResult gameResult = new GamesResult()
            {
                Games = games,
                Message = "Games Updated Successfully"
            };
            return gameResult;
        }
        private bool UserAlreadyPlayingTheGame(Games games)
        {
            bool FoundedUserPlaying = _context.Games.Where(g => g.UserId == games.UserId && g.Game.ToLower() == games.Game.ToLower()).Any();

            return FoundedUserPlaying;
        }

        private bool PlatformsAreTheSame(Games games)
        {
            Games? FoundedGames = _context.Games.Where(g => g.Game.ToLower() == games.Game.ToLower()).FirstOrDefault();
            bool arraysAreEqual = Enumerable.SequenceEqual(games.Platforms, FoundedGames.Platforms);
            return arraysAreEqual;
        }

        private bool GamesNameExist(Games games)
        {
            bool FoundedGames = _context.Games.Where(g => g.Game.ToLower() == games.Game.ToLower()).Any();
            return FoundedGames;
        }

        public async Task<IEnumerable<GamesResponse>> GetTopPlayedGamesByPlayTime(string genre, string platform)
        {
            var games = await _context.Games.ToListAsync();

            IEnumerable<Games> gamesFiltred = FilterGamesByGenreAndPlatform(games, genre, platform);
            if (!gamesFiltred.Any())
            {
                return null;
            }
            IEnumerable<GamesResponse> gamesGroupedByNames = GroupGamesByNamesAndReturnItAsListOfGameResponse(gamesFiltred, genre);

            int maxGamePlayedTime = gamesGroupedByNames.MaxBy(g => g.TotalPlayTime).TotalPlayTime;

            IEnumerable<GamesResponse> gamesResponse = SelectedGamesResultWithMaxTotalPlaytimeList(maxGamePlayedTime, gamesGroupedByNames);

            return gamesResponse;
        }



        public async Task<IEnumerable<GamesResponse>> GetTopPlayedGameByUsers(string genre, string platform)
        {
            var games = await _context.Games.ToListAsync();
            IEnumerable<Games> gamesFiltred = FilterGamesByGenreAndPlatform(games, genre, platform);
            if (!gamesFiltred.Any())
            {
                return null;
            }
            IEnumerable<GamesResponse> gamesGroupedByNames = GroupGamesByNamesAndReturnItAsListOfGameResponse(gamesFiltred, genre);

            int maxGamePlayers = gamesGroupedByNames.MaxBy(u => u.TotalPlayers).TotalPlayers;

            IEnumerable<GamesResponse> gamesResponse = SelectedGamesResultWithMaxTotalPlayersList(maxGamePlayers, gamesGroupedByNames);

            return gamesResponse;


        }

        private IEnumerable<GamesResponse> SelectedGamesResultWithMaxTotalPlayersList(int maxGamePlayers, IEnumerable<GamesResponse> gamesGroupedByNames)
        {
            IEnumerable<GamesResponse> gamesResponse = gamesGroupedByNames.Where(s => s.TotalPlayers == maxGamePlayers).Select(x => new GamesResponse
            {
                Game = x.Game,
                Genre = x.Genre,
                Platforms = x.Platforms,
                TotalPlayers = x.TotalPlayers,
                TotalPlayTime = x.TotalPlayTime,
            });
            return gamesResponse;
        }

        private IEnumerable<GamesResponse> SelectedGamesResultWithMaxTotalPlaytimeList(int maxGamePlayedTime, IEnumerable<GamesResponse> gamesGroupedByNames)
        {
            IEnumerable<GamesResponse> gamesResponse = gamesGroupedByNames.Where(x => x.TotalPlayTime == maxGamePlayedTime).Select(x => new GamesResponse
            {
                Game = x.Game,
                Genre = x.Genre,
                Platforms = x.Platforms,
                TotalPlayers = x.TotalPlayers,
                TotalPlayTime = x.TotalPlayTime,
            });
            return gamesResponse;
        }

        private IEnumerable<GamesResponse> GroupGamesByNamesAndReturnItAsListOfGameResponse(IEnumerable<Games> gamesfiltred, string genre)
        {
            return gamesfiltred.GroupBy(i => i.Game.ToLower()).Select(g => new GamesResponse
            {
                Game = g.Key,
                Genre = genre,
                Platforms = g.Select(p => p.Platforms).First(),
                TotalPlayTime = g.Sum(w => w.PlayTime),
                TotalPlayers = g.Count(),
            });
        }

        private IEnumerable<Games> FilterGamesByGenreAndPlatform(List<Games> games, string genre, string platform)
        {
            return games.Where(x => (x.Genre.ToLower() == genre.ToLower()) && (x.Platforms.Contains(platform)));
        }
    }
}
