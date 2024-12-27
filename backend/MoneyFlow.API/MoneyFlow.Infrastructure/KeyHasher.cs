namespace MoneyFlow.Infrastructure
{
    public class KeyHasher : IKeyHasher
    {
        public string Generate(string key) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(key);

        public bool Verify(string key, string keyHash) =>
            BCrypt.Net.BCrypt.EnhancedVerify(key, keyHash);
    }
}
