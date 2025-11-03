namespace hasher
{
    
    public class Config
    {
        
        public bool IsDebug { get; }
        public bool IsSafe { get; }
        public bool IsIterative { get; }
        public bool IsRecursive;
        private HashSet<string> Paths = [];
        private HashSet<string>? Files;

        public Config(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg[0] == '-')
                {
                    // it's a flag, let's examine it
                    switch (arg[1])
                    {
                        case 'd':
                            IsDebug = true;
                            break;
                        case 'r':
                            IsRecursive = true;
                            break;
                        case 's':
                            IsSafe = true;
                            break;
                        case 'i':
                            IsIterative = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    // it's a path, let's add it to the paths
                    Paths.Add(arg);
                }
            }

            if (IsDebug)
            {
                Console.WriteLine("Provided arguments: ");
                for (int i = 0; i < args.Length - 1; i++)
                {
                    Console.Write(args[i] + ", ");
                }

                Console.WriteLine(args[^1]);
            }

            if (IsIterative)
            {
                Console.WriteLine("The program is now running in iterative mode, you will be asked to choose which file to keep for duplicated hashes once every hash has been computed");
            }
        }

        /// <summary>
        /// Explores the paths to determine the files to work on
        /// </summary>
        private void ExplorePaths()
        {
            Files = [];

            foreach (string path in Paths)
            {
                if (File.Exists(path))
                {
                    Files.Add(path);
                }
                else if (IsRecursive && Directory.Exists(path))
                {
                    ExploreDirectoryRecursively(path);
                }
            }
        }

        /// <summary>
        /// Explores recursively a directory to determine the files to compute the hash on
        /// </summary>
        /// <param name="path">is the directory path</param>
        private void ExploreDirectoryRecursively(string path)
        {
            if (File.Exists(path))
            {
                Files!.Add(path);
                return;
            }

            foreach (string file in Directory.GetFiles(path).Concat(Directory.GetDirectories(path)))
            {
                ExploreDirectoryRecursively(file);
            }
        }

        /// <summary>
        /// Returns the files the program will calculate the hash of
        /// </summary>
        /// <returns>a string array in which each entry represents a file to work on</returns>
        /// <exception cref="Exception"></exception>
        public string[] GetFiles()
        {
            // if we haven't explored the paths yet, let's do it now
            if (Files == null)
            {
                ExplorePaths();
            }

            // something went wrong...
            if (Files == null)
                throw new Exception("Error in exploring provided paths!");

            return [.. Files];
        }

    }

};