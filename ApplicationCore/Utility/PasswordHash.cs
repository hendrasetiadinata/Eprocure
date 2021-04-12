using System;
using System.Security.Cryptography;
using System.Text;

namespace ApplicationCore.Utility
{
    public static class PasswordHash
    {
        /// <SUMMARY>
		/// CREATE PASSWORD HASH AND SALT BY TEXT
        /// PASSWORD PARAMETER IS ORIGIN VALUE FROM USER INPUT EX: Galindo123, kasa123
		/// </SUMMARY>
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty string.");

            try
            {
                using var hmac = new HMACSHA512();
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <SUMMARY>
		/// MAKE SURE THAT USER PASSWORD IS MATCH WITH PASSWORD & SALT STORE FROM DATABASE
        /// PASSWORD PARAMETER IS ORIGIN VALUE FROM USER INPUT EX: Galindo123, kasa123
		/// </SUMMARY>
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty string.");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            try
            {
                using var hmac = new HMACSHA512(storedSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++) 
                    if (computedHash[i] != storedHash[i]) return false;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
    }
}
