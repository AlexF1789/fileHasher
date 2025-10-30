namespace hasher
{
    
    public class OutputWriter(string[] duplicates)
    {
        private string[] Duplicates = duplicates;

        public void Write()
        {
            using var file = new StreamWriter("output.txt");

            foreach (string duplicate in Duplicates)
            {
                file.WriteLine(string.Format("\"{0}\"", duplicate));
            }
        }
    }

};