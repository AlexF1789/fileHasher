File Hasher
===

Simple program to hash files in directories from the terminal, written using C# to acheive multiplatform intercompatibility.

## Build
Due to its development nature no binaries have been released yet. To compile the software you need .NET SDK version 9 and run the command:
```
dotnet build
```
from the terminal in the cloned directory. It should work even with lower version but it's strongly recommended to use it with .NET version 9.

Using only .NET Libraries or custom defined one it runs on every platform .NET runs. So far it has been developed and tested on Linux.

## Usage
To use the program just call it from the command line with the following syntax:
```
fileHasher path1 [path2, ...] [-flag1, -flag2, ...]
```

Where arguments can both be flags or paths. The flags are:

- **-d** for debug, it prints the file name and hash while calculated (it makes the console output heavier)
- **-s** for safe mode which doesn't delete the duplicate files once finished performing the hash
- **-r** to proceed to identify files recursively if any of the path provided is a directory.
- **-i** for iterative mode which mean you will be asked which files to keep for each duplicated hash (otherwise it will only consider the first file to appear with the hash)

The paths can both be files or directory in an undefined number (at least one). The dot works as a valid path (obviously it must be used with the recursive mode).

## Performance
The program doesn't have a graphical user interface and uses internal data structures to perform the hashing operations. The program itself is not particularly heavy even if used on very large amount of files (even thousands with the recursive mode).

## Disclaimer
The software is still in development, and **IT'S NOT READY TO REAL WORLD USAGE**. Please don't use it on files you care for or haven't backed up yet because it may result in unwanted deletion. The developers **ARE NOT RESPONSIBLE** for any improper usage which may lead to file losing problems.
