using Microsoft.AspNetCore.Mvc;
using GamePetApi.Models;
using GamePetApi.Interfaces;
using System.Runtime.CompilerServices;

namespace GamePetApi.Controllers;

[ApiController]
[Route("controller")]
public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    private readonly ICRUDDAO<TeamMember> _context;

    public TeamController(ILogger<TeamController> logger, ICRUDDAO<TeamMember> context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("addmember")] //Create
    public IActionResult Post(TeamMember member)
    {
        var result = _context.AddItem(member);
        if (result > 0) return StatusCode(500, "An error occurred while attempting to add " + member.FirstName + " " + member.LastName + " to the database: There is/are " + result + " existing team member(s) with those parameters.");
        if (result < 0) return StatusCode(500, "An error occurred while attempting to add " + member.FirstName + " " + member.LastName + " to the database.");
        return Ok(member.FirstName + " " + member.LastName + " (ID " + member.Id + ") added to database.");
    }

    [HttpGet("getallmembers")] //Read
    public IActionResult Get()
    {
        return Ok(_context.GetAllItems());
    }

    [HttpGet("getmemberbyid")] //Read
    public IActionResult GetById(int? id)
    {
        if (id is null || id == 0) return Ok(_context.GetFirstFiveItems());
        var member = _context.GetItemById((int)id);
        if (member is null) return NotFound("There is no team member at ID " + id + ".");
        return Ok(member);
        
    }

    [HttpPut("updatemember")] //Update
    public IActionResult Put(TeamMember member)
    {
        var result = _context.UpdateItem(member);
        if (result is null) return NotFound(member.FirstName + " " + member.LastName + " (ID " + member.Id + ") does not exist.");
        if (result != 0) return StatusCode(500, "An error occurred while attempting to update " + member.FirstName + " " + member.LastName + " (ID " + member.Id + ").");
        return Ok(member.FirstName + " " + member.LastName + " (ID " + member.Id + ") updated.");
    }

    [HttpDelete("deletememberbyid")] //Delete
    public IActionResult Delete(int id)
    {
        var result = _context.RemoveItemById(id);
        if (result is null) return NotFound("Team member of ID " + id + " does not exist.");
        if (result != 0) return StatusCode(500, "An error occurred while attempting to delete team member of ID " + id + ".");
        return Ok("Member of ID " + id + " removed.");
    }
        
}