﻿using Microsoft.AspNetCore.Mvc;
using Basic_Games_Shelf.DOMAINE;
using Basic_Games_Shelf.DATA.IServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Basic_Games_Shelf.DATA.Result;
using Basic_Games_Shelf.DATA.Dto;
using Basic_Games_Shelf.DATA.Response;

namespace Basic_Games_Shelf.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;

        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamesDTO>>> GetGames()
        {
            IEnumerable<Games> games = await _gamesService.GetGames();
            IEnumerable <GamesDTO> gamesDTO =
            games.Select(x => new GamesDTO
            {
                UserId = x.UserId,
                Game = x.Game,
                Genre = x.Genre,
                Platforms = x.Platforms,    
                PlayTime = x.PlayTime
            })
                            .ToList();
            return Ok(gamesDTO);
        }
        // GET: api/Games/GamesWithDetails
        [HttpGet("GamesWithDetails")]
        public async Task<ActionResult<IEnumerable<Games>>> GetGamesWithDetails()
        {
            return Ok(await _gamesService.GetGames());
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Games>> GetGames(int id)
        {
            var games = await _gamesService.GetGames(id);
            if (games == null)
            {
                return NotFound();
            }
            return games;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGames(int id, Games games)
        {
            if (id != games.Id)
            {
                return BadRequest();
            }
            if (!GamesExists(id))
            {
                return NotFound();
            }
            GamesResult gamesResult = await _gamesService.PutGames(id, games);
            if(gamesResult.Message== "Games Updated Successfully")
            {
                return NoContent();
            }
            return BadRequest();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Games>> PostGames(Games games)
        {
           
            GamesResult gamesResult = await _gamesService.PostGames(games);
            if(gamesResult.Message != "Games Added Successfully")
            {
                return BadRequest(gamesResult.Message);
            }
            return CreatedAtAction("GetGames", new { id = games.Id }, games);
        }
        // POST: api/Games/AddMultipleGames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddMultipleGames")]
        public async Task<ActionResult<IEnumerable<GamesResult>>> PostMultipleGames(IEnumerable<Games> games)
        {
            var gamesResults = new List<GamesResult>();
            foreach (var game in games)
            {
                gamesResults.Add(await _gamesService.PostGames(game));
            }
          
            return Ok(gamesResults);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGames(int id)
        {
            var games = await _gamesService.DeleteGames(id);
            if (games == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/Games/select_top_by_playtime?genre=FPS&platform=PC
        [HttpGet("select_top_by_playtime")]
        public async Task<ActionResult<IEnumerable<GamesResponse>>> GetTopPlayedGamesByPlayTime([BindRequired] string genre, [BindRequired] string platform)
        {
           
            var gamesPlayedHaveSameTotalPlayed= await _gamesService.GetTopPlayedGameByUsers(genre, platform);

            if (gamesPlayedHaveSameTotalPlayed == null)
            {
                return NotFound();
            }

            return Ok(gamesPlayedHaveSameTotalPlayed);
        }

        // GET: api/Games/select_top_by_players?genre=FPS&platform=PC
        [HttpGet("select_top_by_players")]
        public async Task<ActionResult<IEnumerable<GamesResponse>>> GetTopPlayedGameByUsers([BindRequired] string genre, [BindRequired] string platform)
        {
            var mostPlayedGames= await _gamesService.GetTopPlayedGameByUsers(genre, platform);

            if (mostPlayedGames == null)
            {
                return NotFound();
            }

            return Ok(mostPlayedGames);
        }
        private bool GamesExists(int id)
        {
            return _gamesService.GamesExists(id);
        }
    }
}
