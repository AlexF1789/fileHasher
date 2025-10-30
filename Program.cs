using hasher;

public class Program
{
    static void Main(string[] args)
    {
        Config configuration = new(args);
        string[] files = configuration.GetFiles();
        Dictionary<string, string> uniqueFiles = [];
        Dictionary<string, string> duplicateFiles = [];
        ConsoleWriter consoleWriter = new(configuration.IsDebug, files.Length);

        Console.WriteLine(string.Format("Hashing {0} files", files.Length));

        foreach (string file in files)
        {
            FileHasher fs = new(file);
            string hash = fs.GetHash();

            if (uniqueFiles.ContainsKey(hash))
            {
                // the hash is already present
                consoleWriter.Print(fs, true);
                duplicateFiles.Add(fs.Path, uniqueFiles.GetValueOrDefault(fs.GetHash(), "error"));
            }
            else
            {
                consoleWriter.Print(fs, false);
                uniqueFiles.Add(fs.GetHash(), fs.Path);
            }
        }

        // let's print the results
        Console.WriteLine(string.Format("{0} unique files found - {1} duplicate files found", uniqueFiles.Count, duplicateFiles.Count));
        foreach (string duplicate in duplicateFiles.Keys)
        {
            Console.WriteLine(string.Format("{0} duplicates {1}", duplicate, duplicateFiles.GetValueOrDefault(duplicate, "error")));
        }

        // if we are operating in safe mode let's write everything in a text file and terminate the program
        new OutputWriter([.. duplicateFiles.Keys], files.Length).Write();

        if (configuration.IsSafe)
            return;

        // otherwise let's delete the duplicate files
        foreach (string duplicate in duplicateFiles.Keys)
        {
            File.Delete(duplicate);
            Console.WriteLine(string.Format("file {0} deleted", duplicate));
        }
    }
}