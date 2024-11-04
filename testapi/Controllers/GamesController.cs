using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace testapi.Controllers;

[ApiController]
[Route("[controller]")]

public class GamesController : ControllerBase
{
    private static List<Game> games = new List<Game>();

// demo has this bellow methods  I cant imagine why thoe order would matter though keep it in mind
   public class Game{
        public int id { get; set; }
        public string? teamOneName { get; set; }
        public string? teamTwoName { get; set; }
        public int winner { get; set; } 

   } 
    List<Game> PopulateGames(){
        return new List<Game>
        {
            new Game{
               id = 1,
               teamOneName="London",
               teamTwoName="Cardif",
               winner =1  
            },
             new Game{
               id = 2,
               teamOneName="Leeds",
               teamTwoName="London",
               winner =2  
            },
             new Game{
               id = 3,
               teamOneName="London",
               teamTwoName="Manchester",
               winner =1  
            },
        };
    }

    private readonly ILogger<GamesController> _logger;

    public GamesController(ILogger<GamesController> logger)
    {
        games = PopulateGames();
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Game>> Get()
    {
        if (games != null && games.Count > 0)
        {
            return Ok(games);  // Return list of games
        }
        return NotFound(); 
    }
    
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Game>> Delete(int id) {
            var game = games.FirstOrDefault(g => g.id == id);
            if (game != null)
            {
                games.Remove(game); // Remove the game if it exists
                return Ok(games);   // Return updated list of games
            }
            return NotFound(); 
        }
        
        [HttpPost]
        public ActionResult<IEnumerable<Game>> AddGame(Game game){
            if (game != null)
            {
                games.Add(game); // Add the new game to the list
                return CreatedAtAction(nameof(Get), new { id = game.id }, games); // Return 201 with updated games list
            }
            return BadRequest();
        }
}
