using System;
using System.IO;    // do Directory.GetFiles

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


            /*
            foreach (var subdirectory in allDirectories)       // wyświetlenie katalogów
            {
                Console.WriteLine("==============================");
                Console.WriteLine("number of FOLDERS in given path is {0}. All folders:", allDirectories.Length);




                Console.WriteLine(Path.GetFileName(subdirectory));         // to jest dobrze
                //var allDirectoriesInFolder = Directory.GetDirectories(subdirectory);
                Catalogs(subdirectory);     // rekurencja
            }

            Console.WriteLine("==============================");

            
            
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);      // todo tutaj zmienić
            Console.WriteLine("number of FILES in given folder is {0}. All files:", files.Length);
            
            for (int i = 0; i < files.Length; i++)
            {
                //Console.WriteLine(files[i]);
                Console.WriteLine("\t" + Path.GetFileName(files[i]));
                //Console.WriteLine(files.DirectorySeparatorChar);
                //Path.DirectorySeparatorChar(files[i]);
                //Console.WriteLine("type of files[i]: " + files[i].GetType());       // System.String

            }
           
            */
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

                    Console.WriteLine(fileName);
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
                    Console.WriteLine(subdirectory);
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

            if (args.Length > 0)
            {
                Console.WriteLine("ilość argumentów: " + args.Length);
                string startPath = args[0];      // ścieżka pobrana z parametru wywołania programu
                Console.WriteLine("Start path: " + startPath);
                //Console.WriteLine("Type of path:" + path.GetType());


                // todo tutaj dodać obsługę kolejnych parametrów funkcji


                int countBackslashStartPath = startPath.Split('\\').Length - 1;
                Console.WriteLine("countBackslash = " + countBackslashStartPath);

                TreeStructure(startPath, countBackslashStartPath);
            }
            else
            {
                Console.WriteLine("No application arguments");
            }
        }
    }
}
