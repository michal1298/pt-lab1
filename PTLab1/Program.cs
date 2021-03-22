using System;
using System.IO;    // do Directory.GetFiles
using System.Linq;  // do Count()
using System.Collections.Generic;


// metody rozszerzające (extension methods):
namespace PTLab1
{
    // rozszerzyć System.IO.FileSystemInfo o zwrócenie napisu reprezentującego
    // atrybuty dostępowe danego pliku/katalogu w postaci sformatowanej:
    public static class ExtensionFileSystemInfo
    {
        public static string FileName(this FileInfo file)
        {
            string myFileName = file.Name;
            return myFileName;
        }
        
        public static long SizeFile(this FileInfo file)
        {
            long mySizeFile = file.Length;
            return mySizeFile;
        }

        public static DateTime FileDate(this FileInfo file)
        {
            var myFileDate = file.CreationTime;
            myFileDate = file.LastWriteTime;
            return myFileDate;
        }

        public static string PropertiesFileRead(this FileInfo file)    // właściwości pliku
        {
            bool read = file.Attributes.HasFlag(FileAttributes.ReadOnly);
            if (read == true)
            {
                return "read ";
            }
            else
            {
                return "";
            }
        }

        public static string PropertiesFileHidden(this FileInfo file)
        {
            bool hidden = file.Attributes.HasFlag(FileAttributes.Hidden);
            if (hidden == true)
            {
                return "hidden ";
            }
            else
            {
                return "";
            }
        }

        public static string PropertiesFileArchive(this FileInfo file)
        {
            bool archive = file.Attributes.HasFlag(FileAttributes.Archive);
            if (archive == true)
            {
                return "archive ";
            }
            else
            {
                return "";
            }
        }

        public static string PropertiesFileSystem(this FileInfo file)
        {
            bool system = file.Attributes.HasFlag(FileAttributes.System);
            if (system == true)
            {
                return "system ";
            }
            else
            {
                return "";
            }
        }
    }


    // rozszerzyć System.IO.DirectoryInfo o liczbę elementów w katalogu:
    public static class ExtensionDirectoryInfo
    {
        public static int NumberOfDirectories(this DirectoryInfo myDirectory)   // liczba katalogów w podanym folderze
        {
            var numberOfDirectories = myDirectory.GetDirectories().Count();
            return numberOfDirectories;
        }
        
        public static int NumberOfFiles(this DirectoryInfo myDirectory)         // liczba plików w podanym katalogu
        {
            var numberOfFiles = myDirectory.GetFiles().Count();
            return numberOfFiles;
        }

        public static DateTime DirectoryDate(this DirectoryInfo myDirectory)
        {
            DateTime myDirectoryDate = myDirectory.CreationTime;
            myDirectoryDate = myDirectory.LastWriteTime;
            return myDirectoryDate;
        }

        public static string DirectoryName(this DirectoryInfo myDirectory)
        {
            string myDirectoryName = myDirectory.Name;
            return myDirectoryName;
        }

        public static string PropertiesDirectoryRead(this DirectoryInfo myDirectory)
        {
            bool read = myDirectory.Attributes.HasFlag(FileAttributes.ReadOnly);
            if (read == true)
            {
                return "read ";
            }
            else
            {
                return "";
            }
        }

        public static string PropertiesDirectoryHidden(this DirectoryInfo myDirectory)
        {
            bool hidden = myDirectory.Attributes.HasFlag(FileAttributes.Hidden);
            if (hidden == true)
            {
                return "hidden ";
            }
            else
            {
                return "";
            }
        }

        public static string PropertiesDirectoryArchive(this DirectoryInfo myDirectory)
        {
            bool archive = myDirectory.Attributes.HasFlag(FileAttributes.Archive);
            if (archive == true)
            {
                return "archive ";
            }
            else
            {
                return "";
            }
        }

        public static string PropertiesDirectorySystem(this DirectoryInfo myDirectory)
        {
            bool system = myDirectory.Attributes.HasFlag(FileAttributes.System);
            if (system == true)
            {
                return "system ";
            }
            else
            {
                return "";
            }
        }
    }
}


namespace PTLab1
{
    class Program
    {
        //int count = 0;
        static void TreeStructure(string path, int countBackslashStartPath)
        {
            Files(path, countBackslashStartPath);
            Catalogs(path, countBackslashStartPath);
        }

        static void Files(string path, int countBackslashStartPath)
        {
            var allFiles = Directory.GetFiles(path);
            if (allFiles.Length > 0)
            {
                var filesInCatalog = Directory.GetFiles(path);
                int countBackslash = path.Split('\\').Length - 1;
                int numberOfTabs = countBackslash - countBackslashStartPath;

                foreach (var fileName in filesInCatalog)
                {
                    for (int i = 0; i < numberOfTabs; i++)
                    {
                        Console.Write("\t");
                    }
                    
                    var myFileInfo = new FileInfo(fileName);
                    Console.WriteLine(ExtensionFileSystemInfo.FileName(myFileInfo) 
                        + "    |||  size: " + ExtensionFileSystemInfo.SizeFile(myFileInfo)
                        + " | date: " + ExtensionFileSystemInfo.FileDate(myFileInfo)
                        + " | attributes: " + ExtensionFileSystemInfo.PropertiesFileRead(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileHidden(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileArchive(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileSystem(myFileInfo));
                }
            }
        }

        static void Catalogs(string path, int countBackslashStartPath)
        {
            var allDirectories = Directory.GetDirectories(path);
            int countBackslash = path.Split('\\').Length - 1;
            int numberOfTabs = countBackslash - countBackslashStartPath;

            if (allDirectories.Length > 0)
            {
                foreach (var subdirectory in allDirectories)
                {
                    for (int i = 0; i < numberOfTabs; i++)
                    {
                        Console.Write("\t");
                    }
                   
                    var myDirectoryInfo = new DirectoryInfo(subdirectory);

                    Console.WriteLine(ExtensionDirectoryInfo.DirectoryName(myDirectoryInfo) 
                        + "    |||  catalogs: " + ExtensionDirectoryInfo.NumberOfDirectories(myDirectoryInfo) 
                        + "  files: " + ExtensionDirectoryInfo.NumberOfFiles(myDirectoryInfo)
                        + " | date: " + ExtensionDirectoryInfo.DirectoryDate(myDirectoryInfo)
                        + " | attributes: " + ExtensionDirectoryInfo.PropertiesDirectoryRead(myDirectoryInfo)
                        + ExtensionDirectoryInfo.PropertiesDirectoryHidden(myDirectoryInfo)
                        + ExtensionDirectoryInfo.PropertiesDirectoryArchive(myDirectoryInfo)
                        + ExtensionDirectoryInfo.PropertiesDirectorySystem(myDirectoryInfo));


                    TreeStructure(subdirectory, countBackslashStartPath);
                }
            }
        }

        static void Main(string[] args)
        {
            // Solution Explorer -> Right click on project name -> Properties -> Debug -> Application arguments:

            if (args.Length > 2)
            {
                // Pobranie argumentów z application arguments:
                string startPath = args[0];      // ścieżka pobrana z parametru wywołania programu
                Console.WriteLine("Start path: " + startPath);

                string typeOfSort = args[1];    // typ sortowania
                Console.WriteLine("typeOfSort: " + typeOfSort);

                string orderOfSort = args[2];   // kolejność sortowania
                Console.WriteLine("orderOfSort: " + orderOfSort + '\n');

                int countBackslashStartPath = startPath.Split('\\').Length - 1;

                TreeStructure(startPath, countBackslashStartPath);
            }
            else
            {
                Console.WriteLine("Not enough application arguments");
            }
        }
    }
}
