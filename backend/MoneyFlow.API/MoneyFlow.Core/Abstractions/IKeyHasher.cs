namespace MoneyFlow.Infrastructure
{
    public interface IKeyHasher
    {
        string Generate(string key);
        bool Verify(string key, string keyHash);
    }
}