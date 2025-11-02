using System.Collections;
using hasher;

public class Program
{
    static void Main(string[] args)
    {
        Config configuration = new(args);
        string[] files = configuration.GetFiles();
        ConsoleWriter consoleWriter = new(configuration.IsDebug, files.Length);

        Dictionary<string, string[]> hashedFiles = [];
        int duplicatedFiles = 0;
        int uniqueFiles = 0;

        Console.WriteLine(string.Format("Hashing {0} files", files.Length));

        foreach (string file in files)
        {
            FileHasher fs = new(file);
            string hash = fs.GetHash();

            if (hashedFiles.ContainsKey(hash))
            {
                // the hash is already present -> let's add the file to the array
                consoleWriter.Print(fs, true);
                hashedFiles[hash] = [.. hashedFiles.GetValueOrDefault(hash, []), fs.Path];
                duplicatedFiles++;
            }
            else
            {
                // we found the hash for the first time -> let's put it in the dictionary
                consoleWriter.Print(fs, false);
                hashedFiles.Add(hash, [fs.Path]);
                uniqueFiles++;
            }
        }

        if (uniqueFiles + duplicatedFiles != files.Length)
        {
            // for some reason we elaborated less files than supposed
            throw new Exception("The elaborated files don't match the total files!");
        }

        // let's print the results
        Console.WriteLine(string.Format("{0} unique files found - {1} duplicate files found", uniqueFiles, duplicatedFiles));

        // if we're operating in iterative mode let's make the user select the files he wants to keep and populate the
        // deletion file queue, otherwise let's do it automatically by only keeping the first one for each hash
        string[] filesToDelete = new string[duplicatedFiles];
        int currentFileToDeleteIndex = 0;

        foreach (string hash in hashedFiles.Keys)
        {
            // let's fetch the elements and skip the iteration if it's just one file
            string[] duplicated = hashedFiles[hash];
            int indexFileToKeep = configuration.IsIterative && !(duplicated.Length == 1) ? ConsoleWriter.ChooseWhichFile(duplicated) : 0;

            for (int i = 0; i < duplicated.Length; i++)
            {
                if (i != indexFileToKeep)
                    filesToDelete[currentFileToDeleteIndex++] = duplicated[i];
            }
        }

        // let's generate the output file
        new OutputWriter(filesToDelete, files.Length).Write();

        // if we are in safe mode let's terminate the program
        if (configuration.IsSafe)
            return;

        // let's delete the files
        foreach(string file in filesToDelete)
        {
            File.Delete(file);
            Console.WriteLine(string.Format("file {0} deleted", file));
        }
    }
}