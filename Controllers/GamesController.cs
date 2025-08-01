using Microsoft.AspNetCore.Mvc;
using GamePetApi.Models;
using GamePetApi.Interfaces;
using System.Runtime.CompilerServices;

namespace GamePetApi.Controllers;

[ApiController]
[Route("controller")]
public class GamesController : ControllerBase
{
    private readonly ILogger<GamesController> _logger;
    private readonly ICRUDDAO<VideoGame> _context;

    public GamesController(ILogger<GamesController> logger, ICRUDDAO<VideoGame> context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("addgame")] //Create
    public IActionResult Post(VideoGame game)
    {
        var result = _context.AddItem(game);
        if (result > 0) return StatusCode(500, "An error occurred while attempting to add " + game.Title + " to the database: There is/are " + result + " existing team member(s) with those parameters.");
        if (result < 0) return StatusCode(500, "An error occurred while attempting to add " + game.Title + " to the database.");
        return Ok(game.Title + " (ID " + game.Id + ") added to database.");
    }

    [HttpGet("getallgames")] //Read
    public IActionResult Get()
    {
        return Ok(_context.GetAllItems());
    }

    [HttpGet("getgamebyid")] //Read
    public IActionResult GetById(int id)
    {
        if (id > 0)
        {
            var game = _context.GetItemById(id);
            if (game is null) return NotFound("Video game of ID " + id + " does not exist.");
            return Ok(game);
        }
        return Ok(_context.GetFirstFiveItems());
    }

    [HttpPut("updategame")] //Update
    public IActionResult Put(VideoGame game)
    {
        var result = _context.UpdateItem(game);
        if (result is null) return NotFound(game.Title + " (ID " + game.Id + ") does not exist.");
        if (result != 0) return StatusCode(500, "An error occurred while attempting to update " + game.Title + " (ID " + game.Id + ").");
        return Ok(game.Title + " (ID " + game.Id + ") updated.");
    }

    [HttpDelete("deletegamebyid")] //Delete
    public IActionResult Delete(int id)
    {
        var result = _context.RemoveItemById(id);
        if (result is null) return NotFound("Video game of ID " + id + " does not exist.");
        if (result != 0) return StatusCode(500, "An error occurred while attempting to delete video game of ID " + id + ").");
        return Ok("Video game of ID " + id + ") removed.");
    }
        
}