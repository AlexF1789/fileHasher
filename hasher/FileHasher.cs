using System.Security.Cryptography;

namespace hasher
{

    public class FileHasher
    {

        public string Path { get; }
        private string? Hash;

        public FileHasher(string path)
        {
            if (!File.Exists(path))
                throw new Exception("The specified file doesn't exist!");

            Path = path;
        }

        /// <summary>
        /// Calculates the hash of the file
        /// </summary>
        private void CalculateHash()
        {
            using SHA1 sha1 = SHA1.Create();
            using FileStream stream = File.OpenRead(Path);

            Hash = Convert.ToHexStringLower(sha1.ComputeHash(stream));
        }

        /// <summary>
        /// Returns the hash of the file
        /// </summary>
        /// <returns>A string representing the file hash</returns>
        /// <exception cref="Exception"></exception>
        public string GetHash()
        {
            // if the hash has not been computed, let's compute it
            if (Hash == null)
                CalculateHash();

            // something went wrong...
            if (Hash == null)
                throw new Exception("An error occured while computing the hash!");

            return Hash;
        }

        /// <summary>
        /// Returns a string representing the file with its hash or the duplicate indication
        /// </summary>
        /// <param name="duplicate">indicates if the file si duplicated</param>
        /// <returns>a summary string for the file</returns>
        public string ToString(bool duplicate)
        {
            if (duplicate)
                return string.Format("[{0}] duplicate!", Path);
            else
                return string.Format("[{0}]: {1}", Path, GetHash());
        }

        public override string ToString()
        {
            return ToString(false);
        }

    }
    
};