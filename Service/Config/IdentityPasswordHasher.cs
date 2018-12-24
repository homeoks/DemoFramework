using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using Infrastructure.Extensitions;
using Microsoft.AspNetCore.Identity;

namespace Service.Config
{
    public class IdentityPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            user.PasswordHash = password.Encrypt(EncryptType.Md5);
            return user.PasswordHash;
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if (providedPassword.Encrypt(EncryptType.Md5) == hashedPassword)
                return PasswordVerificationResult.Success;
            return PasswordVerificationResult.Failed;
        }
    }
}
