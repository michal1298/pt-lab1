using System;
using System.IO;    // do Directory.GetFiles
using System.Linq;  // do Count()
using System.Collections.Generic;   // do SortedSet 


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
        //int count = 0;
        static void TreeStructure(string path, int countBackslashStartPath, string typeOfSort, string orderOfSort)
        {
            var myDirectories = new DirectoryInfo(path);
            Files(myDirectories, countBackslashStartPath, typeOfSort, orderOfSort);
            Catalogs(myDirectories, countBackslashStartPath, typeOfSort, orderOfSort);
            
            // stare:
            //Files(path, countBackslashStartPath, typeOfSort, orderOfSort);
            //Catalogs(path, countBackslashStartPath, typeOfSort, orderOfSort);
        }
        
        
        static void Catalogs(DirectoryInfo path, int countBackslashStartPath, string typeOfSort, string orderOfSort)
        {
            List<DirectoryInfo> myDirectories = path.GetDirectories().ToList();
            foreach(var directory in myDirectories)
            {
                // wyświetla poprawinie katalogi w głównym folderze:
                Console.WriteLine(directory.Name);
            }
        }
        
        static void Files(DirectoryInfo path, int countBackslashStartPath, string typeOfSort, string orderOfSort)
        {
            List<FileInfo> myFiles = path.GetFiles().ToList();
            foreach (var file in myFiles)
            {
                // wyświetla poprawinie pliki w głównym folderze:
                Console.WriteLine(file.Name);
            }
            // wywołanie sortowania
            List<FileInfo> sort = SortFiles(myFiles, typeOfSort, orderOfSort);
            foreach(var sortedFile in sort)
            {
                Console.WriteLine("sorted file: " + sortedFile.Name);
            }
        }
        


        static public List<FileInfo> SortFiles(List<FileInfo> myFiles, string typeOfSort, string orderOfSort)
        {
            Console.WriteLine("----SortFiles method----");
            if (typeOfSort == "size")
            {
                if (orderOfSort == "normal")
                {
                    Console.WriteLine("size-normal");
                    var sortedFiles = myFiles.OrderBy(file => file.Length).ToList();
                    return sortedFiles;
                }
                else if (orderOfSort == "reverse")
                {
                    Console.WriteLine("size-reverse");
                    var sortedFiles = myFiles.OrderBy(file => file.Length).Reverse().ToList();
                    return sortedFiles;
                }
                else
                {
                    Console.WriteLine("else 2 work");
                    return myFiles;
                }
            }
            else if (typeOfSort == "alphabetical")
            {
                if (orderOfSort == "normal")
                {
                    Console.WriteLine("alphabetical-normal");
                    var sortedFiles = myFiles.OrderBy(file => file.Name).ToList();
                    return sortedFiles;
                }
                else if (orderOfSort == "reverse")
                {
                    Console.WriteLine("alphabetical-reverse");
                    var sortedFiles = myFiles.OrderBy(file => file.Name).Reverse().ToList();
                    return sortedFiles;
                }
                else
                {
                    Console.WriteLine("else 2 work");
                    return myFiles;
                }
            }

            else if (typeOfSort == "date")
            {
                if (orderOfSort == "normal")
                {
                    Console.WriteLine("date-normal");
                    var sortedFiles = myFiles.OrderBy(file => file.LastWriteTime).ToList();
                    return sortedFiles;
                }
                else if (orderOfSort == "reverse")
                {
                    Console.WriteLine("date-reverse");
                    var sortedFiles = myFiles.OrderBy(file => file.LastWriteTime).Reverse().ToList();
                    return sortedFiles;
                }
                else
                {
                    Console.WriteLine("else 2 work");
                    return myFiles;
                }
            }

            else
            {
                Console.WriteLine("else work");
                return myFiles;
            }
        }

        private List<DirectoryInfo> SortDirectories(List<DirectoryInfo> myDirectories)
        {
            return myDirectories;
        }
        
        
        static void Catalogs(string path, int countBackslashStartPath, string typeOfSort, string orderOfSort)
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


                    TreeStructure(subdirectory, countBackslashStartPath, typeOfSort, orderOfSort);
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
                Console.WriteLine("typeOfSort: '{0}'", typeOfSort);

                string orderOfSort = args[2];   // kolejność sortowania
                Console.WriteLine("orderOfSort: '{0}'", orderOfSort);

                int countBackslashStartPath = startPath.Split('\\').Length - 1;

                TreeStructure(startPath, countBackslashStartPath, typeOfSort, orderOfSort);
            }
            else
                Console.WriteLine("Not enough application arguments");
        }
    }
}



/*
// stare wyświetlanie plików, bez sortowania:
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
*/

/*
// stare wyświetlanie posortowanych plików:
static void Files(string path, int countBackslashStartPath, string typeOfSort, string orderOfSort)
{

    var allFiles = Directory.GetFiles(path);

    if (allFiles.Length > 0)
    {
        string[] filesInCatalog = Directory.GetFiles(path);
        //var filesInCatalogByDate = ExtensionFileSystemInfo.FileDate(path);
        int countBackslash = path.Split('\\').Length - 1;
        int numberOfTabs = countBackslash - countBackslashStartPath;


        var sortedFilesByName = new SortedSet<string>();                // to działa
        var reversedSortedFilesByName = sortedFilesByName.Reverse();    // to działa

        var sortedFilesByDate = new SortedSet<string>();
        var reversedSortedFilesByDate = sortedFilesByDate.Reverse();

        if (typeOfSort == "alphabetical")
        {
            foreach (string fileName in filesInCatalog)
            {
                sortedFilesByName.Add(fileName);       // działa, gdy var sortedFiles = new SortedSet<string>();
            }

            if (orderOfSort == "normal")
            {
                foreach (string fileInCatalog in sortedFilesByName)     // wyświetla poprawnie nieposortowane pliki w katalogu
                {
                    for (int i = 0; i < numberOfTabs; i++)
                    {
                        Console.Write("\t");
                    }

                    //Console.WriteLine(files1);
                    var myFileInfo = new FileInfo(fileInCatalog);
                    Console.WriteLine(ExtensionFileSystemInfo.FileName(myFileInfo)
                        + "    |||  size: " + ExtensionFileSystemInfo.SizeFile(myFileInfo)
                        + " | date: " + ExtensionFileSystemInfo.FileDate(myFileInfo)
                        + " | attributes: " + ExtensionFileSystemInfo.PropertiesFileRead(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileHidden(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileArchive(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileSystem(myFileInfo));
                }
            }
            else if (orderOfSort == "reverse")
            {
                foreach (string fileInCatalog in reversedSortedFilesByName)     // wyświetla poprawnie nieposortowane pliki w katalogu
                {
                    for (int i = 0; i < numberOfTabs; i++)
                    {
                        Console.Write("\t");
                    }

                    //Console.WriteLine(files1);
                    var myFileInfo = new FileInfo(fileInCatalog);
                    Console.WriteLine(ExtensionFileSystemInfo.FileName(myFileInfo)
                        + "    |||  size: " + ExtensionFileSystemInfo.SizeFile(myFileInfo)
                        + " | date: " + ExtensionFileSystemInfo.FileDate(myFileInfo)
                        + " | attributes: " + ExtensionFileSystemInfo.PropertiesFileRead(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileHidden(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileArchive(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileSystem(myFileInfo));
                }
            }
            else
                Console.WriteLine("Invalid argument '{0}'", orderOfSort);
        }
        else if (typeOfSort == "size")
        {
            if (orderOfSort == "normal")
            {
                Console.WriteLine("typeOfSort = size; orderOfSort = normal");
            }
            else if (orderOfSort == "reverse")
            {
                Console.WriteLine("typeOfSort = size; orderOfSort = reverse");
            }
            else
                Console.WriteLine("Invalid argument '{0}'", orderOfSort);
        }
        else if (typeOfSort == "date")
        {
            if (orderOfSort == "normal")
            {
                Console.WriteLine("typeOfSort = date; orderOfSort = normal");
            }
            else if (orderOfSort == "reverse")
            {
                Console.WriteLine("typeOfSort = date; orderOfSort = reverse");
            }
            else
                Console.WriteLine("Invalid argument '{0}'", orderOfSort);
        }
        else
            Console.WriteLine("Invalid argument '{0}'", typeOfSort);
    }
    
}
*/

/*
// stara metoda wywoływania:
static void Files(string path, int countBackslashStartPath, string typeOfSort, string orderOfSort)
{

}
*/
