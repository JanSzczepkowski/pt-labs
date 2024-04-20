#pragma warning disable SYSLIB0011
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters.Binary;


public static class Program
{
    static void Main(string[] args)
    {
        string directoryPath = args[0];
        Console.WriteLine(GetOldestFileDate(directoryPath));
        printFilesRecursive(directoryPath, 0);
        printSortedFiles(directoryPath);

    }

    public static void printFilesRecursive(string directory, int depth)
    {
        try
        {
            string[] files = Directory.GetFiles(directory);
            string[] directories = Directory.GetDirectories(directory);
            foreach (string dir in directories)
            {
                for (int i = 0; i < depth; i++)
                {
                    Console.Write("\t");
                }
                Console.WriteLine(Path.GetFileName(dir) + " (" + countFiles(dir) + ")");
                printFilesRecursive(dir, depth + 1);
            }

            foreach (string file in files)
            {
                for (int i = 0; i < depth; i++)
                {
                    Console.Write("\t");
                }
                FileInfo fileInfo = new FileInfo(file);
                string dosAttributes = fileInfo.getDosAttributes();
                Console.WriteLine(Path.GetFileName(file) + $" {fileInfo.Length} bajtów " + dosAttributes);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }

    public static int countFiles(string directory)
    {
        int count = 0;
        string[] files = Directory.GetFiles(directory);
        string[] directories = Directory.GetDirectories(directory);
        foreach (string dir in directories)
        {
            count += countFiles(dir);
        }
        count += files.Length;
        return count;
    }

    public static string getDosAttributes(this FileSystemInfo fileInfo)
    {
        string attributes = "";
        if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            attributes += "r";
        else
            attributes += "-";
        if ((fileInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive)
            attributes += "a";
        else
            attributes += "-";
        if ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
            attributes += "h";
        else
            attributes += "-";
        if ((fileInfo.Attributes & FileAttributes.System) == FileAttributes.System)
            attributes += "s";
        else
            attributes += "-";
        return attributes;
    }

    public static DateTime GetOldestFileDate(string directory)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            FileInfo[] files = directoryInfo.GetFiles();
            DateTime oldestDate = files.Select(f => f.LastWriteTime).DefaultIfEmpty(DateTime.MaxValue).Min();
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            foreach (var dir in directories)
            {
                DateTime subOldestDate = GetOldestFileDate(dir.FullName);
                if (subOldestDate < oldestDate)
                {
                    oldestDate = subOldestDate;
                }
            }
            return oldestDate;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return DateTime.MaxValue;
        }
    }

    public static void printSortedFiles(string directory)
    {
        SortedDictionary<string, long> sortedElements = null;
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            FileSystemInfo[] filesAndDirectories = directoryInfo.GetFileSystemInfos();
            sortedElements = new SortedDictionary<string, long>(new FilesComparer());
            foreach (var info in filesAndDirectories)
            {
                if (info is FileInfo)
                {
                    FileInfo fileInfo = (FileInfo)info;
                    sortedElements.Add(fileInfo.Name, fileInfo.Length);
                }
                else if (info is DirectoryInfo)
                {
                    DirectoryInfo subDirectoryInfo = (DirectoryInfo)info;
                    sortedElements.Add(subDirectoryInfo.Name, subDirectoryInfo.GetFileSystemInfos().Length);
                }
            }
            foreach (var element in sortedElements)
            {
                Console.WriteLine($"{element.Key} -> {element.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
        Console.WriteLine("SERIALIZED AND DESERIALIZED:");
        printSerializeAndDeserialize(sortedElements);
    }

    static void printSerializeAndDeserialize(SortedDictionary<string, long> collection)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream("collection.bin", FileMode.Create))
        {
            formatter.Serialize(fileStream, collection);
        }

        using (FileStream fileStream = new FileStream("collection.bin", FileMode.Open))
        {
            var deserializedCollection = (SortedDictionary<string, long>)formatter.Deserialize(fileStream);
            foreach (var element in deserializedCollection)
            {
                Console.WriteLine($"{element.Key} -> {element.Value}");
            }
        }
    }
    [Serializable]
    class FilesComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return x.CompareTo(y);
        }
    }
}
