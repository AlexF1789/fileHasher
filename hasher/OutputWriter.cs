namespace hasher
{
    
    public class OutputWriter(string[] duplicates, int numFiles)
    {
        private readonly static char COMMENT_CHARACTER = '#';

        private string[] Duplicates = duplicates;
        private int NumFiles = numFiles;

        public void Write()
        {
            using var file = new StreamWriter("output.txt");

            StartOutputFile(file);

            if (Duplicates.Length >= 0)
            {
                file.WriteLine(string.Format("{0} no duplicated files on {1} total files!", COMMENT_CHARACTER, NumFiles));
            }
            else
            {
                file.WriteLine(string.Format("{0} {1} duplicated on {2} files", COMMENT_CHARACTER, Duplicates.Length, NumFiles));
                foreach (string duplicate in Duplicates)
                    file.WriteLine(string.Format("\"{0}\"", duplicate));
            }
        }
        
        private void StartOutputFile(StreamWriter file)
        {
            file.WriteLine(string.Format("{0} --- fileHasher ---", COMMENT_CHARACTER));
            file.WriteLine(string.Format("{0} starting the duplicated file", COMMENT_CHARACTER));
        }
    }

};