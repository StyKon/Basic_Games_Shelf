using Basic_Games_Shelf.DOMAINE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Games_Shelf.DATA.IServices
{
    public interface IGamesService
    {
        Task<IEnumerable<Games>> GetGames();
        Task<Games> GetGames(int id);
        Task<GamesResult> PutGames(int id, Games games);
        Task<GamesResult> PostGames(Games games);
        Task<Games> DeleteGames(int id);
        bool GamesExists(int id);
    }
}
