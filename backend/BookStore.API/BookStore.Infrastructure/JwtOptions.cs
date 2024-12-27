namespace BookStore.Infrastructure
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExporesHours { get; set; }  
    }
}
