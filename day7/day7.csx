#load "../tools.csx"

using System.Text.RegularExpressions;

var lines = File.ReadAllLines("day7/input.txt");

int totalDiskSpace = 70000000;

int spaceRequiredByUpdate = 30000000;

public enum FileType
{
    RegularFile,
    Directory
}

public class FsFile
{
    public static FsFile Root = new FsFile("/", 0, FileType.Directory, new HashSet<FsFile>());

    public FsFile(string path, int size, FileType fileType, HashSet<FsFile> files)
    {
        Path = path;
        Size = size;
        FileType = fileType;
        Files = files;
    }

    public string Path { get; }
    public int Size { get; set; }
    public FileType FileType { get; }
    public HashSet<FsFile> Files { get; }
    public void AddFile(FsFile fsFile) => Files.Add(fsFile);
}

public class FileSystem
{
    public FileSystem(FsFile currentDirectory, Dictionary<string, FsFile> files)
    {
        CurrentDirectory = currentDirectory;
        Files = files;
    }

    public FsFile CurrentDirectory { get; private set; }

    public Dictionary<string, FsFile> Files { get; }

    public FsFile ChangeDirectory(string path)
    {
        if (!Files.ContainsKey(path))
        {
            Files.Add(path, new FsFile(path, 0, FileType.Directory, new HashSet<FsFile>()));
        }
        CurrentDirectory = Files[path];
        return CurrentDirectory;
    }

    public void AddFile(string name, int size)
    {
        var file = new FsFile(Path.Combine(CurrentDirectory.Path, name), size, FileType.RegularFile, new HashSet<FsFile>());
        CurrentDirectory.AddFile(file);
        Files[file.Path] = file;

        string parentPath = CurrentDirectory.Path;
        while (parentPath != FsFile.Root.Path)
        {
            Files[parentPath].Size += size;
            parentPath = Path.GetFullPath(Path.Combine(parentPath, ".."));
        }
        Files[parentPath].Size += size;
    }

    public void AddDirectory(string name)
    {
        var file = new FsFile(Path.Combine(CurrentDirectory.Path, name), 0, FileType.Directory, new HashSet<FsFile>());
        CurrentDirectory.AddFile(file);
        Files[file.Path] = file;
    }
}

var fileSystem = new FileSystem(FsFile.Root, new Dictionary<string, FsFile>());
string currentDirectory = string.Empty;

foreach (var line in lines)
{
    if (line.StartsWith("$ cd"))
    {
        var argument = line.Substring(4).Trim();
        switch (argument)
        {
            case "/":
                currentDirectory = "/";
                break;
            case "..":
                currentDirectory = Path.GetFullPath(Path.Combine(currentDirectory, ".."));
                break;
            default:
                currentDirectory = Path.GetFullPath(Path.Combine(currentDirectory, argument));
                break;
        }
        fileSystem.ChangeDirectory(currentDirectory);
    }

    var fileMatch = Regex.Match(line, @"(\d+)\s(.*)");
    if (fileMatch.Success)
    {
        var size = fileMatch.Groups[1].Value.Parse();
        var fileName = fileMatch.Groups[2].Value;
        fileSystem.AddFile(fileName, size);
    }

    if (line.StartsWith("dir"))
    {
        var directoryName = line.Substring(3).Trim();
        fileSystem.AddDirectory(directoryName);
    }
}


var totalSizesOfDirectoriesWithSizeBelow1000000 = fileSystem.Files.Values.Where(file => file.Size < 100000 && file.FileType == FileType.Directory).Sum(file => file.Size);
WriteLine($"The sum of the total sizes of directories with size less than 1000000: {totalSizesOfDirectoriesWithSizeBelow1000000}");

var freeSpace = (totalDiskSpace - fileSystem.Files["/"].Size);
var spaceNeededForUpdate = spaceRequiredByUpdate - freeSpace;

var sizeOfSmallestDirectoryToDelete = fileSystem.Files.Values.Where(file => file.FileType == FileType.Directory).Where(file => file.Size >= spaceNeededForUpdate).OrderBy(file => file.Size).Select(file => file.Size).First();
WriteLine($"Size of the smallest directory to delete to free up space for update: {sizeOfSmallestDirectoryToDelete}");





