using System.Text.RegularExpressions;

namespace AOC2022._07;

public class Day07A : Day
{
    public override string Run()
    {
        var input = GetInputAsStringArray();

        var root = new FileOrDirectory
        {
            IsDirectory = true,
            Name = "/"
        };

        var currentDirectory = root;

        for (var i = 1; i < input.Length; i++)
        {
            var row = input[i];
            
            if (row == "$ ls")
            {
                // Ignore for now
            }
            else if (cdRegex.IsMatch(row))
            {
                var match = cdRegex.Match(row);
                var dir = match.Groups[1].Value;

                if (dir == "..")
                {
                    currentDirectory = currentDirectory.Parent;
                }
                else
                {
                    currentDirectory = currentDirectory.Children.Single(c => c.IsDirectory && c.Name.Equals(dir));
                }
            }
            else if (fileRegex.IsMatch(row))
            {
                var match = fileRegex.Match(row);
                var file = new FileOrDirectory
                {
                    Size = match.Groups[1].Value.ToLong(),
                    Name = match.Groups[2].Value,
                    Parent = currentDirectory
                };
                
                currentDirectory.Children.Add(file);
            }
            else if (dirRegex.IsMatch(row))
            {
                var match = dirRegex.Match(row);
                var dir = new FileOrDirectory
                {
                    IsDirectory = true,
                    Name = match.Groups[1].Value,
                    Parent = currentDirectory
                };
                
                currentDirectory.Children.Add(dir);
            }
        }

        root.Size = CalculateSizeOnDirectories(root);
        
        return "" + DirectorySum(root);
    }

    private static long CalculateSizeOnDirectories(FileOrDirectory fileOrDirectory)
    {
        foreach (var child in fileOrDirectory.Children)
        {
            fileOrDirectory.Size += CalculateSizeOnDirectories(child);
        }

        return fileOrDirectory.Size;
    }

    private static long DirectorySum(FileOrDirectory fileOrDirectory)
    {
        var sum = fileOrDirectory.Size <= 100000 && fileOrDirectory.IsDirectory ? fileOrDirectory.Size : 0;
        foreach (var child in fileOrDirectory.Children)
        {
            sum += DirectorySum(child);
        }

        return sum;
    }

    private static Regex cdRegex = new Regex(Regex.Escape("$") + " cd (.+)");
    private static Regex fileRegex = new Regex("([0-9]+) (.+)");
    private static Regex dirRegex = new Regex("dir (.+)");

    private class FileOrDirectory
    {
        public bool IsDirectory { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }

        public FileOrDirectory? Parent { get; set; }
        public List<FileOrDirectory> Children { get; set; } = new();
    }
}
