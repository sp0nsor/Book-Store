using BookStore.API.Contracts;
using BookStore.API.Contracts.Books;
using BookStore.Application.Services;
using BookStore.Core.Abstractions.Services;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BookStore.API.Endpoints
{
    public static class BooksEndpoints
    {
        public static IEndpointRouteBuilder MapBooksEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/Books").RequireAuthorization();

            group.MapGet("/", GetBooks);
            group.MapPost("/", CreateBook);
            group.MapPut("/{id:guid}", UpdateBook);
            group.MapDelete("/{id:guid}", DeleteBook);
            group.MapPost("by", ByBook);

            return app;
        }

        private static async Task<IResult> GetBooks(IBooksService booksService)
        {
            var books = await booksService.GetAllBooks();
            var response = books.Select(b => new BookResponse(b.Id, b.Title, b.Description, b.Price));

            return Results.Ok(response);
        }

        private static async Task<IResult> CreateBook([FromBody] BookRequest request, IBooksService booksService)
        {
            var (book, error) = Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price);

            if(!string.IsNullOrEmpty(error))
                return Results.BadRequest(error);

            var bookId = await booksService.CreateBook(book);

            return Results.Ok(book.Id);
        }

        private static async Task<IResult> UpdateBook(Guid id, [FromBody] BookRequest request, IBooksService booksService)
        {
            var bookId = await booksService.UpdateBook(id, request.Title, request.Description, request.Price);

            return Results.Ok(bookId);
        }

        private static async Task<IResult> DeleteBook(Guid id, IBooksService booksService)
        {
            var bookId = await booksService.DeleteBook(id);

            return Results.Ok(bookId);
        }

        private static async Task<IResult> ByBook([FromBody] TransferRequest request, IPaymentService paymentService)
        {
            var isOk = await paymentService.MakeTransfer(request);

            if (isOk)
            {
                return Results.Ok();
            }

            return Results.BadRequest();
        }
    }
}
