using Microsoft.AspNetCore.Identity;

namespace FamilyApplication.CommonServices
{
    public class PasswordHasedService
    {
        private readonly PasswordHasher<string> _hasher = new();

        public string PasswordHased (string password)
        {
            return _hasher.HashPassword(null, password);
        }
        public bool VerifyPasswordHashed(string HashedPassword, string Providedpwd)
        {
            var result = _hasher.VerifyHashedPassword(null, HashedPassword, Providedpwd);
            return result == PasswordVerificationResult.Success;
        }
    }
}
