using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Basic_Games_Shelf.DOMAINE;
using Basic_Games_Shelf.DATA;
using Basic_Games_Shelf.DATA.IServices;

namespace Basic_Games_Shelf.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;
        private readonly BasicGamesShelfContext _context;

        public GamesController(IGamesService gamesService, BasicGamesShelfContext _context)
        {
            _gamesService = gamesService;
            this._context = _context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Games>>> GetGames()
        {
            return Ok(await _gamesService.GetGames());
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Games>> GetGames(int id)
        {
            return Ok(await _gamesService.GetGames(id));
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGames(int id, Games games)
        {
            return Ok(await _gamesService.PutGames(id, games));
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IEnumerable<Games>> PostGames([FromBody] IEnumerable<Games> games)
        {
            foreach (var game in games)
            {
                await _gamesService.PostGames(game);
            }
            return (IEnumerable<Games>)Ok(games);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGames(int id)
        {
            return Ok(await _gamesService.DeleteGames(id));
        }

        private bool GamesExists(int id)
        {
            return _gamesService.GamesExists(id);
        }
    }
}
