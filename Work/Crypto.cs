using System.Security.Cryptography;

namespace Work
{
    /// <summary>
    /// Easily accessible methods for computing cryptographic values
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// instantiate a new Random Number Generator
        /// </summary>
        private static RandomNumberGenerator provider = new RNGCryptoServiceProvider();

        /// <summary>
        /// Instantiate a new Hash Algorithm
        /// </summary>
        private static HashAlgorithm algorithm = new SHA256Managed();

        /// <summary>
        /// Generate a byte array with cryptographic random numbers
        /// </summary>
        /// <param name="size">The byte array size</param>
        /// <returns>A byte array filled with a cryptographically strong sequence of random values.</returns>
        public static byte[] GenerateRandom(int size)
        {
            // instantiate a new byte array of specified size
            byte[] buffer = new byte[size];
            // generate a cryptographically strong sequence of random values.
            provider.GetBytes(buffer);

            // return the buffer filled with random values
            return buffer;
        }

        /// <summary>
        /// Computes the hash value for the specified byte array.
        /// </summary>
        /// <param name="value">The input to compute the hash code for.</param>
        /// <returns>The computed hash code.</returns>
        public static byte[] Hash(byte[] value)
        {
            // compute hash of the salted byte array
            return algorithm.ComputeHash(value);
        }
    }
}
