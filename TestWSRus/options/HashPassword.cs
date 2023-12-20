using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestWSRus.options
{
    public  class HashPassword
    {

        public byte[] GeneralSalt()
        {
            const int saltSize = 64;
            byte[] salt = new byte[saltSize];
            var random = new RNGCryptoServiceProvider();
            random.GetBytes(salt);
            return salt;
        }

        public byte[] GenerationHash(string password,byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saledPass = new byte[salt.Length + passwordBytes.Length];

            var hashed = new SHA256CryptoServiceProvider();

            return hashed.ComputeHash(saledPass);
        }
    }

}

