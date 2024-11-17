namespace BookStore.API.Contracts.Books
{
    public record BookRequest(
        string Title,
        string Description,
        decimal Price);
}
