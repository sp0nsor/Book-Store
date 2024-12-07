using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DataAccess.Entites
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
