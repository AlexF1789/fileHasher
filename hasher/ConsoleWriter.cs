namespace hasher
{
    
    public class ConsoleWriter(bool isDebug, int numFiles)
    {
        private bool IsDebug = isDebug;
        private int NumFiles = numFiles;
        private int ProgressiveNumber = 0;

        /// <summary>
        /// Prints the current fileHasher instance handling if the file is duplicated
        /// or not and the current debug status
        /// </summary>
        /// <param name="fileHasher">is the file hasher instance</param>
        /// <param name="duplicated">indicates if the file is duplicated</param>
        public void Print(FileHasher fileHasher, bool duplicated)
        {
            if (IsDebug)
            {
                if (duplicated)
                    PrintDuplicatedHashDebug(fileHasher);
                else
                    PrintCalculatedHashDebug(fileHasher);
            }
            else
            {
                if (duplicated)
                    PrintDuplicatedHash();
                else
                    PrintCalculatedHash();
            }
        }

        /// <summary>
        /// Prints the calculated hash
        /// </summary>
        private void PrintCalculatedHash()
        {
            Console.WriteLine(string.Format("[{0}/{1}]", ProgressiveNumber++, NumFiles));
        }

        /// <summary>
        /// Prints the duplicated hash
        /// </summary>
        private void PrintDuplicatedHash()
        {
            Console.WriteLine(string.Format("[{0}/{1}] duplicated", ProgressiveNumber++, NumFiles));
        }

        /// <summary>
        /// Prints the calculated hash in debug mode
        /// </summary>
        /// <param name="fileHasher">is the file hasher instance</param>
        private void PrintCalculatedHashDebug(FileHasher fileHasher)
        {
            Console.WriteLine(string.Format("[{0}/{1}] {2}: {3}", ProgressiveNumber++, NumFiles, fileHasher.Path, fileHasher.GetHash()));
        }

        /// <summary>
        /// Prints the duplicated hash in debug mode
        /// </summary>
        /// <param name="fileHasher">is the file hasher instance</param>
        private void PrintDuplicatedHashDebug(FileHasher fileHasher)
        {
            Console.WriteLine(string.Format("[{0}/{1}] {2} duplicated", ProgressiveNumber++, NumFiles, fileHasher.Path));
        }
    
    }

};