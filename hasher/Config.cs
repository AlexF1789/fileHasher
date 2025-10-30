using System.Transactions;

namespace hasher
{
    
    public class Config
    {
        
        public bool IsDebug { get; }
        public bool IsSafe { get; }
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
                for(int i=0; i<args.Length-1; i++)
                {
                    Console.Write(args[i] + ", ");
                }

                Console.WriteLine(args[^1]);
            }
        }

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

        private void ExploreDirectoryRecursively(string path)
        {
            if (File.Exists(path))
            {
                Files.Add(path);
                return;
            }

            foreach (string file in  Directory.GetFiles(path).Concat(Directory.GetDirectories(path)))
            {
                ExploreDirectoryRecursively(file);
            }
        }

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