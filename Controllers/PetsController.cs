using Microsoft.AspNetCore.Mvc;
using GamePetApi.Models;
using GamePetApi.Interfaces;


namespace GamePetApi.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PetsController : ControllerBase
    {
        private readonly ILogger<PetsController> _logger;
        private readonly ICRUDDAO<Pet> _context;

        public PetsController(ILogger<PetsController> logger, ICRUDDAO<Pet> context)
        {
            _logger = logger;
            _context = context;
        }

        // Create
        [HttpPost("addpet")]
        public IActionResult Post(Pet pet)
        {
            var result = _context.AddItem(pet);
            if (result > 0) return StatusCode(500, "An error occurred: There is/are " + result + " existing pet(s) with those parameters.");
            if (result < 0) return StatusCode(500, "An error occurred while attempting to add " + pet.Name);
            return Ok(pet.Name + " (ID " + pet.Id + ") added to database.");
        }

        // Read
        [HttpGet("getallpets")]
        public IActionResult Get()
        {
            return Ok(_context.GetAllItems());
        }
        // Read
        [HttpGet("getpetbyid")]
        public IActionResult GetById(int? id)
        {
            if (id is null || id == 0) return Ok(_context.GetFirstFiveItems());
            var pet = _context.GetItemById((int)id);
            if (pet is null) return NotFound("There is no pet at ID " + id + ".");
            return Ok(pet);
        }

        // Update
        [HttpPut("updatepet")]
        public IActionResult Put(Pet pet)
        {
            var result = _context.UpdateItem(pet);
            if (result is null) return NotFound(pet.Name + " (ID " + pet.Id + ") does not exist.");
            if (result != 0) return StatusCode(500, "An error occurred while attempting to update " + pet.Name);
            return Ok(pet.Name + " (ID " + pet.Id + ") updated.");
        }

        // Delete
        [HttpDelete("deletepetbyid")]
        public IActionResult Delete(int id)
        {
            var result = _context.RemoveItemById(id);
            if (result is null) return NotFound("Pet of ID " + id + " does not exist.");
            if (result != 0) return StatusCode(500, "An error occurred while attempting to delete pet of ID " + id);
            return Ok("Pet of ID " + id + " removed.");
        }
    }
}
