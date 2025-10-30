namespace hasher
{
    
    public class ConsoleWriter(bool isDebug, int numFiles)
    {
        private bool IsDebug = isDebug;
        private int NumFiles = numFiles;
        private int ProgressiveNumber = 0;

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

        private void PrintCalculatedHash()
        {
            Console.WriteLine(string.Format("[{0}/{1}]", ProgressiveNumber++, NumFiles));
        }

        private void PrintDuplicatedHash()
        {
            Console.WriteLine(string.Format("[{0}/{1}] duplicated", ProgressiveNumber++, NumFiles));
        }

        private void PrintCalculatedHashDebug(FileHasher fileHasher)
        {
            Console.WriteLine(string.Format("[{0}/{1}] {2}: {3}", ProgressiveNumber++, NumFiles, fileHasher.Path, fileHasher.GetHash()));
        }

        private void PrintDuplicatedHashDebug(FileHasher fileHasher)
        {
            Console.WriteLine(string.Format("[{0}/{1}] {2} duplicated", ProgressiveNumber++, NumFiles, fileHasher.Path));
        }
    }

};