using hasher;

public class Program
{
    static void Main(string[] args)
    {
        Config configuration = new(args);
        string[] files = configuration.GetFiles();
        Dictionary<string, string> uniqueFiles = [];
        Dictionary<string, string> duplicateFiles = [];

        Console.WriteLine(string.Format("Hashing {0} files", files.Length));

        foreach (string file in files)
        {
            FileHasher fs = new(file);
            string hash = fs.GetHash();

            if (uniqueFiles.ContainsKey(hash))
            {
                // the hash is already present
                Console.WriteLine(fs.ToString(true));
                duplicateFiles.Add(fs.Path, uniqueFiles.GetValueOrDefault(fs.GetHash(), "error"));
            }
            else
            {
                // it's the first time this hash has been found
                Console.WriteLine(fs.ToString(false));
                uniqueFiles.Add(fs.GetHash(), fs.Path);
            }
        }

        // let's print the results
        Console.WriteLine(string.Format("{0} unique files found - {1} duplicate files found", uniqueFiles.Count, duplicateFiles.Count));
        foreach(string duplicate in duplicateFiles.Keys)
        {
            Console.WriteLine(string.Format("{0} duplicates {1}", duplicate, duplicateFiles.GetValueOrDefault(duplicate, "error")));
        }
    }
}