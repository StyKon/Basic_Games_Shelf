
using Basic_Games_Shelf.DATA.IServices;
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
        public async Task<Games> DeleteGames(int id)
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

            if (games == null)
            {
                return games;
            }

            return games;
        }

        public async Task<IEnumerable<Games>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Games> PostGames(Games games)
        {
            _context.Games.Add(games);
            await _context.SaveChangesAsync();

            return games;
        }

        public async Task<Games> PutGames(int id, Games games)
        {
            if (id != games.Id)
            {
                return games;
            }
            _context.Entry(games).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamesExists(id))
                {
                    return games;
                }
                else
                {
                    throw;
                }
            }

            return games;
        }
    }
}
