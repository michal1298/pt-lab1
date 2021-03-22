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
            //var numberOfFiles = myDirectory.GetFiles().Count();

            return numberOfDirectories;
        }
        
        public static int NumberOfFiles(this DirectoryInfo myDirectory)         // liczba plików w podanym katalogu
        {
            var numberOfFiles = myDirectory.GetFiles().Count();
            return numberOfFiles;
        }

        public static DateTime DirectoryDate(this DirectoryInfo myDirectory)
        {
            var myDirectoryDate = myDirectory.CreationTime;
            myDirectoryDate = myDirectory.LastWriteTime;
            return myDirectoryDate;
        }

        /*
        public static int SizeOfFile(this DirectoryInfo myDirecetory, string path)
        {
            var sizeofFile = myDirecetory.GetFiles();
            long size = 0;
            //long singleFile = 0;
            
            
            foreach (int singleFile in sizeofFile)
            {
                size = singleFile.len
            }
            return (int)size;
            
        }
        */
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
                    // TODO nie wiem, czy tak może być:
                    // Console.WriteLine(fileName); // cała ścieżka
                    //var myDirectoryInfo = new DirectoryInfo(path);

                    //Console.Write(Path.GetFileName(fileName));
                    //Console.Write(myFileInfo.Name);
                    //Console.WriteLine("  |||  size: " + myFileInfo.Length);
                    
                    var myFileInfo = new FileInfo(fileName);
                    Console.WriteLine(ExtensionFileSystemInfo.FileName(myFileInfo) 
                        + "  |||  size: " + ExtensionFileSystemInfo.SizeFile(myFileInfo)
                        + " | date: " + ExtensionFileSystemInfo.FileDate(myFileInfo)
                        + " | attributes: " + ExtensionFileSystemInfo.PropertiesFileRead(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileHidden(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileArchive(myFileInfo)
                        + ExtensionFileSystemInfo.PropertiesFileSystem(myFileInfo));
                    /*
                    Console.Write("  |||  size: " + ExtensionFileSystemInfo.SizeFile(myFileInfo));
                    Console.Write(" | attributes: " + ExtensionFileSystemInfo.PropertiesFileRead(myFileInfo));
                    Console.Write(ExtensionFileSystemInfo.PropertiesFileHidden(myFileInfo));
                    Console.Write(ExtensionFileSystemInfo.PropertiesFileArchive(myFileInfo));
                    Console.WriteLine(ExtensionFileSystemInfo.PropertiesFileSystem(myFileInfo));
                    */

                    //Console.WriteLine(Path.GetFileName(fileName) + Path.);
                    //Console.Write(Path.GetFileName(fileName) + "  |||  size: " 
                    //    + ExtensionFileSystemInfo.SizeOfElement(myFileInfo));
                    //Console.Write(ExtensionDirectoryInfo.NumberOfDirectoriesFiles(myDirectoryInfo));



                    //var myDirectoryInfo = new DirectoryInfo(path);

                    // TODO to poprawnie wyświetla nazwę:
                    //Console.WriteLine(Path.GetFileName(fileName));

                    /*
                    Console.Write(Path.GetFileName(fileName) + "   |||   size: "
                        + ExtensionDirectoryInfo.SizeOfFile(myDirectoryInfo, path) + '\n');
                    */


                    // TODO rozmiar pliku
                    /*
                    var allDirectories = Directory.GetDirectories(path);
                    foreach (var subdirectory in allDirectories)
                    {
                        var myDirectoryInfo = new DirectoryInfo(subdirectory);
                        var myFiles.
                    }
                    */
                    /*
                    for (int i = 1; i < fileName.Length; i++)
                    {
                        FileInfo myFileInfo = new FileInfo(allFiles[i]);
                        var sizeofmyfileinfo = myFileInfo.Length;
                        Console.WriteLine(sizeofmyfileinfo);

                    }
                    */
                }

            }
        }
        static void Catalogs(string path, int countBackslashStartPath)
        {
            //Console.WriteLine("function Catalogs -------------------");
            var allDirectories = Directory.GetDirectories(path);
            int countBackslash = path.Split('\\').Length - 1;
            int numberOfTabs = countBackslash - countBackslashStartPath;
            //Console.WriteLine("numberOfTabs = " + numberOfTabs);

            if (allDirectories.Length > 0)
            {
                //Console.WriteLine("  number of FOLDERS in given path is {0}.", allDirectories.Length);
                foreach (var subdirectory in allDirectories)
                {
                    for (int i = 0; i < numberOfTabs; i++)
                    {
                        Console.Write("\t");
                    }
                    //var myDirectoryInfo = new DirectoryInfo(path);
                    var myDirectoryInfo = new DirectoryInfo(subdirectory);
                    //Console.WriteLine(Path.GetFileName(subdirectory));       // tylko nazwa folderu

                    Console.WriteLine(Path.GetFileName(subdirectory) 
                        + "  |||  catalogs: " + ExtensionDirectoryInfo.NumberOfDirectories(myDirectoryInfo) 
                        + "  files: " + ExtensionDirectoryInfo.NumberOfFiles(myDirectoryInfo)
                        + " | date: " + ExtensionDirectoryInfo.DirectoryDate(myDirectoryInfo));


                    TreeStructure(subdirectory, countBackslashStartPath);

                }
            }
        }


        static void Main(string[] args)
        {
            // Solution Explorer -> Right click on project name -> Properties -> Debug -> Application arguments:

            if (args.Length > 2)
            {
                //Console.WriteLine("number of arguments: " + args.Length);
                string startPath = args[0];      // ścieżka pobrana z parametru wywołania programu
                Console.WriteLine("Start path: " + startPath);
                //Console.WriteLine("Type of path:" + path.GetType());

                string typeOfSort = args[1];
                Console.WriteLine("typeOfSort: " + typeOfSort);

                string orderOfSort = args[2];
                Console.WriteLine("orderOfSort: " + orderOfSort + '\n');

                int countBackslashStartPath = startPath.Split('\\').Length - 1;
                //Console.WriteLine("countBackslash = " + countBackslashStartPath);

                TreeStructure(startPath, countBackslashStartPath);
            }
            else
            {
                Console.WriteLine("Not enough application arguments");
            }
        }
    }
}
