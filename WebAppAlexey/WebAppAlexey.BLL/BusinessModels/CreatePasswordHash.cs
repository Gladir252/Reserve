using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace WebAppAlexey.BLL.BusinessModels
{
    class CreatePasswordHash
    {
        private string _password;
        public CreatePasswordHash(string pass)
        {
            _password = pass;
        }

        byte[] salt = new byte[128 / 8];



        public string GetHash()
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: _password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }

}

