using BookStore.Core.Models;
using BookStore.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using BookStore.Core.Abstractions;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _bookService;

        public BooksController(IBooksService booksService)
        {
            _bookService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _bookService.GetAllBooks();

            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BookRequest request)
        {
            var (book, error) = Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price);

            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);

            var bookId = await _bookService.CreateBook(book);

            return Ok(bookId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBooks(Guid id, [FromBody] BookRequest request)
        {
            var bookId = await _bookService.UpdateBook(id, request.Title, request.Description, request.Price);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            var bookId = await _bookService.DeleteBook(id);

            return Ok(bookId);
        }
    }
}
