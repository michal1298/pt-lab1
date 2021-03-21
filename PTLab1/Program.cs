using System;
using System.IO;    // do Directory.GetFiles
using System.Linq;  // do Count()
using System.Collections.Generic;


// metody rozszerzające (extension methods):
namespace PTLab1
{
    // rozszerzyć System.IO.FileSystemInfo o zwrócenie napisu reprezentującego
    // atrybuty dostępowe danego pliku/katalogu w postaci sformatowanej:
    public static class ExtensionFileSytemInfo
    {
        public static void PrintAttributes(this FileSystemInfo path)
        {
            Console.WriteLine("PrintAttributes function");
        }
    }


    // rozszerzyć System.IO.DirectoryInfo o liczbę elementów w katalogu:
    public static class ExtensionDirectoryInfo
    {
        public static int NumberOfDirectories(this DirectoryInfo myDirectory)
        {
            var numberOfDirectories = myDirectory.GetDirectories().Count();
            //var numberOfFiles = myDirectory.GetFiles().Count();

            return numberOfDirectories;
        }
        
        public static int NumberOfFiles(this DirectoryInfo myDirectory)
        {
            var numberOfFiles = myDirectory.GetFiles().Count();
            return numberOfFiles;
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
            //Console.WriteLine("Current path: " + path);
            //var allFiles = Directory.GetFiles(path);
            //Console.WriteLine("  number of FILES in given path is {0}.", allFiles.Length);
            Files(path, countBackslashStartPath);
            Catalogs(path, countBackslashStartPath);
        }

        static void Files(string path, int countBackslashStartPath)
        {
            //Console.WriteLine("function Files --------------------");
            var allFiles = Directory.GetFiles(path);
            if (allFiles.Length > 0)
            {
                //Console.WriteLine("  number of FILES in given path is {0}.", allFiles.Length);
                var filesInCatalog = Directory.GetFiles(path);
                int countBackslash = path.Split('\\').Length - 1;
                int numberOfTabs = countBackslash - countBackslashStartPath;
                //Console.WriteLine("numberOfTabs = " + numberOfTabs);

                foreach (var fileName in filesInCatalog)
                {
                    for (int i = 0; i < numberOfTabs; i++)
                    {
                        Console.Write("\t");
                    }
                    // TODO nie wiem, czy tak może być:
                    // Console.WriteLine(fileName); // cała ścieżka
                    //var myDirectoryInfo = new DirectoryInfo(path);

                    Console.WriteLine(Path.GetFileName(fileName) + "  ");
                    //Console.Write(ExtensionDirectoryInfo.NumberOfDirectoriesFiles(myDirectoryInfo));
                    //Console.WriteLine();
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

                    Console.WriteLine(Path.GetFileName(subdirectory) + "  |||  catalogs: "
                        + ExtensionDirectoryInfo.NumberOfDirectories(myDirectoryInfo) + "  files: " 
                        + ExtensionDirectoryInfo.NumberOfFiles(myDirectoryInfo)
                        );


                    TreeStructure(subdirectory, countBackslashStartPath);

                }
            }
        }


        static void Main(string[] args)
        {
            // Solution Explorer -> Right click on project name -> Properties -> Debug -> Application arguments:
            //for (int i = 0; i < args.Length; i++)
            //{
            //    Console.WriteLine($"Arg[{i}] = [{args[i]}]");
            //}

            if (args.Length > 2)
            {
                //Console.WriteLine("number of arguments: " + args.Length);
                string startPath = args[0];      // ścieżka pobrana z parametru wywołania programu
                Console.WriteLine("Start path: " + startPath);
                //Console.WriteLine("Type of path:" + path.GetType());


                // todo tutaj dodać obsługę kolejnych parametrów funkcji
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
