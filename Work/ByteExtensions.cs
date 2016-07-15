using System;
using System.Security.Cryptography;
using System.Linq;

namespace Work
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Computes the hash value for the specified string value using the specified salt
        /// </summary>
        /// <param name="value">The input to compute hash for</param>
        /// <param name="salt">The salt used to add complexity to the hash</param>
        /// <returns>The computed hash code</returns>
        public static byte[] Hash(this byte[] value, byte[] salt)
        {
            // create a salted byte array
            byte[] saltedValue = value.Concat(salt).ToArray();

            // return computed hash value
            return Crypto.Hash(saltedValue);
        }
    }
}
