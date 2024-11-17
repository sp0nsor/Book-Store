namespace BookStore.API.Contracts.Books
{
    public record BookResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price);
}
