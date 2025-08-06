using Microsoft.AspNetCore.Mvc;
using GamePetApi.Models;
using GamePetApi.Interfaces;

namespace GamePetApi.Controllers
{
    [ApiController]
    [Route("controller")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly ICRUDDAO<Book> _context;

        public BooksController(ILogger<BooksController> logger, ICRUDDAO<Book> context)
        {
            _logger = logger;
            _context = context;
        }

        // Create
        [HttpPost("addbook")]
        public IActionResult Post(Book book)
        {
            var result = _context.AddItem(book);
            if (result > 0) return StatusCode(500, "An error occurred: There is/are " + result + " existing book(s) with those parameters.");
            if (result < 0) return StatusCode(500, "An error occurred while attempting to add " + book.Title);
            return Ok(book.Title + " (ID " + book.Id + ") added to database.");
        }

        // Read
        [HttpGet("getallbooks")]
        public IActionResult Get()
        {
            return Ok(_context.GetAllItems());
        }
        
        // Read
        [HttpGet("getbookbyid")]
        public IActionResult GetById(int id)
        {
            if (id > 0)
            {
                var book = _context.GetItemById(id);
                if (book is null) return NotFound("Book of ID " + id + " does not exist.");
                return Ok(book);
            }
            return Ok(_context.GetFirstFiveItems());
        }

        // Update
        [HttpPut("updatebook")]
        public IActionResult Put(Book book)
        {
            var result = _context.UpdateItem(book);
            if (result is null) return NotFound(book.Title + " (ID " + book.Id + ") does not exist.");
            if (result != 0) return StatusCode(500, "An error occurred while attempting to update " + book.Title);
            return Ok(book.Title + " (ID " + book.Id + ") updated.");
        }

        // Delete
        [HttpDelete("deletebookbyid")]
        public IActionResult Delete(int id)
        {
            var result = _context.RemoveItemById(id);
            if (result is null) return NotFound("Book of ID " + id + " does not exist.");
            if (result != 0) return StatusCode(500, "An error occurred while attempting to delete book of ID " + id);
            return Ok("Book of ID " + id + " removed.");
        }
    }
}