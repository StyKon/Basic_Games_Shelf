using Basic_Games_Shelf.DATA.Response;
using Basic_Games_Shelf.DATA.Result;
using Basic_Games_Shelf.DOMAINE;

namespace Basic_Games_Shelf.DATA.IServices
{
    public interface IGamesService
    {
        Task<IEnumerable<Games>> GetGames();
        Task<Games?> GetGames(int id);
        Task<GamesResult> PutGames(int id, Games games);
        Task<GamesResult> PostGames(Games games);
        Task<Games?> DeleteGames(int id);
        Task<IEnumerable<GamesResponse>> GetTopPlayedGamesByPlayTime(string genre, string platform);
        Task<IEnumerable<GamesResponse>> GetTopPlayedGameByUsers(string genre, string platform);
        bool GamesExists(int id);
    }
}
