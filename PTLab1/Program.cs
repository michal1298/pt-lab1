using System;
using System.IO;    // do Directory.GetFiles
using System.Linq;  // do Count()
using System.Collections.Generic;   // do List


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

        public static string PropertiesFileRead(this FileInfo file)
        {
            bool read = file.Attributes.HasFlag(FileAttributes.ReadOnly);
            if (read == true)
                return "r ";
            else
                return "";
        }

        public static string PropertiesFileHidden(this FileInfo file)
        {
            bool hidden = file.Attributes.HasFlag(FileAttributes.Hidden);
            if (hidden == true)
                return "h ";
            else
                return "";
        }

        public static string PropertiesFileArchive(this FileInfo file)
        {
            bool archive = file.Attributes.HasFlag(FileAttributes.Archive);
            if (archive == true)
                return "a ";
            else
                return "";
        }

        public static string PropertiesFileSystem(this FileInfo file)
        {
            bool system = file.Attributes.HasFlag(FileAttributes.System);
            if (system == true)
                return "s ";
            else
                return "";
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
                return "r ";
            else
                return "";
        }

        public static string PropertiesDirectoryHidden(this DirectoryInfo myDirectory)
        {
            bool hidden = myDirectory.Attributes.HasFlag(FileAttributes.Hidden);
            if (hidden == true)
                return "h ";
            else
                return "";
        }

        public static string PropertiesDirectoryArchive(this DirectoryInfo myDirectory)
        {
            bool archive = myDirectory.Attributes.HasFlag(FileAttributes.Archive);
            if (archive == true)
                return "a ";
            else
                return "";
        }

        public static string PropertiesDirectorySystem(this DirectoryInfo myDirectory)
        {
            bool system = myDirectory.Attributes.HasFlag(FileAttributes.System);
            if (system == true)
                return "s ";
            else
                return "";
        }
    }
}


namespace PTLab1
{
    class Program
    {
        static void TreeStructure(string path, int countBackslashStartPath, string typeOfSort, string orderOfSort, int numberOfTabs)
        {
            var myDirectories = new DirectoryInfo(path);
            Files(myDirectories, path, countBackslashStartPath, typeOfSort, orderOfSort, numberOfTabs);
            Catalogs(myDirectories, path, countBackslashStartPath, typeOfSort, orderOfSort, numberOfTabs);
        }


        static void Catalogs(DirectoryInfo path, string pathString, int countBackslashStartPath, string typeOfSort, string orderOfSort, int numberOfTabs)
        {
            // Wszystkie katalogi w danym folderze do listy:
            List<DirectoryInfo> myDirectories = path.GetDirectories().ToList();

            // Sortowanie folderów:
            List<DirectoryInfo> sort = SortDirectories(myDirectories, typeOfSort, orderOfSort);

            foreach (var directory in sort)
            {
                var subdirectoryPath = directory.FullName;      // pełna ścieżka podfolderu

                var subdirectoryPathDirectoryInfo = new DirectoryInfo(subdirectoryPath);    // do informacji o podfolderze

                int countBackslash = subdirectoryPath.Split('\\').Length - 2;   // ilość backslashów w pełnej ścieżce do podfolderu
                numberOfTabs = countBackslash - countBackslashStartPath;        // ilość tabulatorów, jakie należy wstawić


                // wyświetlenie posortowanych folderów:
                for (int i = 0; i < numberOfTabs; i++)
                {
                    Console.Write("\t");
                }

                Console.WriteLine(ExtensionDirectoryInfo.DirectoryName(directory)
                    + "    |||  catalogs: " + ExtensionDirectoryInfo.NumberOfDirectories(directory)
                    + "  files: " + ExtensionDirectoryInfo.NumberOfFiles(directory)
                    + " | date: " + ExtensionDirectoryInfo.DirectoryDate(directory)
                    + " | attributes: " + ExtensionDirectoryInfo.PropertiesDirectoryRead(directory)
                    + ExtensionDirectoryInfo.PropertiesDirectoryHidden(directory)
                    + ExtensionDirectoryInfo.PropertiesDirectoryArchive(directory)
                    + ExtensionDirectoryInfo.PropertiesDirectorySystem(directory));
                

                Files(subdirectoryPathDirectoryInfo, subdirectoryPath, countBackslashStartPath, typeOfSort, orderOfSort, numberOfTabs);
                Catalogs(subdirectoryPathDirectoryInfo, pathString, countBackslashStartPath, typeOfSort, orderOfSort, numberOfTabs);
            }
        }

        static void Files(DirectoryInfo path, string pathString, int countBackslashStartPath, string typeOfSort, string orderOfSort, int numberOfTabs)
        {
            string[] filesInCatalog = Directory.GetFiles(pathString);   // wszystkie pliki w danym podfolderze
            int countBackslash = 0;
            if (filesInCatalog.Length != 0)
            {
                countBackslash = filesInCatalog[0].Split('\\').Length - 2;  // zliczanie ilości backslashów w ścieżce do pliku
            }

            numberOfTabs = countBackslash - countBackslashStartPath;        // ilość tabulatorów, jakie należy wstawić

            List<FileInfo> myFiles = path.GetFiles().ToList();              // stworzenie listy z plikami, jakie znajdują się w podfolderze

            // wywołanie sortowania plików:
            List<FileInfo> sort = SortFiles(myFiles, typeOfSort, orderOfSort);  // posortowanie plików w danym podfolderze

            // wyświetlenie posortowanych plików:
            foreach(var sortedFile in sort)
            {
                for (int i = 0; i < numberOfTabs; i++)
                {
                    Console.Write("\t");
                }

                Console.WriteLine(ExtensionFileSystemInfo.FileName(sortedFile)
                    + "    |||  size: " + ExtensionFileSystemInfo.SizeFile(sortedFile)
                    + " | date: " + ExtensionFileSystemInfo.FileDate(sortedFile)
                    + " | attributes: " + ExtensionFileSystemInfo.PropertiesFileRead(sortedFile)
                    + ExtensionFileSystemInfo.PropertiesFileHidden(sortedFile)
                    + ExtensionFileSystemInfo.PropertiesFileArchive(sortedFile)
                    + ExtensionFileSystemInfo.PropertiesFileSystem(sortedFile));
            }
        }

        static private List<FileInfo> SortFiles(List<FileInfo> myFiles, string typeOfSort, string orderOfSort)
        {
            if (typeOfSort == "size")
            {
                if (orderOfSort == "normal")
                {
                    var sortedFiles = myFiles.OrderBy(file => file.Length).ToList();
                    return sortedFiles;
                }
                else if (orderOfSort == "reverse")
                {
                    var sortedFiles = myFiles.OrderBy(file => file.Length).Reverse().ToList();
                    return sortedFiles;
                }
                else
                    return myFiles;
            }
            else if (typeOfSort == "alphabetical")
            {
                if (orderOfSort == "normal")
                {
                    var sortedFiles = myFiles.OrderBy(file => file.Name).ToList();
                    return sortedFiles;
                }
                else if (orderOfSort == "reverse")
                {
                    var sortedFiles = myFiles.OrderBy(file => file.Name).Reverse().ToList();
                    return sortedFiles;
                }
                else
                    return myFiles;
            }

            else if (typeOfSort == "date")
            {
                if (orderOfSort == "normal")
                {
                    var sortedFiles = myFiles.OrderBy(file => file.LastWriteTime).ToList();
                    return sortedFiles;
                }
                else if (orderOfSort == "reverse")
                {
                    var sortedFiles = myFiles.OrderBy(file => file.LastWriteTime).Reverse().ToList();
                    return sortedFiles;
                }
                else
                    return myFiles;
            }

            else
                return myFiles;
        }

        static private List<DirectoryInfo> SortDirectories(List<DirectoryInfo> myDirectories, string typeOfSort, string orderOfSort)
        {
            if (typeOfSort == "size")
            {
                if (orderOfSort == "normal")
                {
                    var sortedDirectories = myDirectories.OrderBy(directory => directory.GetFiles().Count()).ToList();
                    return sortedDirectories;
                }
                else if (orderOfSort == "reverse")
                {
                    var sortedDirectories = myDirectories.OrderBy(directory => directory.GetFiles().Count()).Reverse().ToList();
                    return sortedDirectories;
                }
                else
                    return myDirectories;
            }
            else if (typeOfSort == "alphabetical")
            {
                if (orderOfSort == "normal")
                {
                    var sortedDirectories = myDirectories.OrderBy(directory => directory.Name).ToList();
                    return sortedDirectories;
                }
                else if (orderOfSort == "reverse")
                {
                    var sortedDirectories = myDirectories.OrderBy(directory => directory.Name).Reverse().ToList();
                    return sortedDirectories;
                }
                else
                    return myDirectories;
            }

            else if (typeOfSort == "date")
            {
                if (orderOfSort == "normal")
                {
                    var sortedDirectories = myDirectories.OrderBy(directory => directory.LastWriteTime).ToList();
                    return sortedDirectories;
                }
                else if (orderOfSort == "reverse")
                {
                    var sortedDirectories = myDirectories.OrderBy(directory => directory.LastWriteTime).Reverse().ToList();
                    return sortedDirectories;
                }
                else
                    return myDirectories;
            }

            else
                return myDirectories;
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
                Console.WriteLine("typeOfSort: '{0}'", typeOfSort);

                string orderOfSort = args[2];   // kolejność sortowania
                Console.WriteLine("orderOfSort: '{0}'", orderOfSort);

                int countBackslashStartPath = startPath.Split('\\').Length - 1;
                int numberOfTabs = 0;

                TreeStructure(startPath, countBackslashStartPath, typeOfSort, orderOfSort, numberOfTabs);
            }
            else
                Console.WriteLine("Not enough application arguments");
        }
    }
}
