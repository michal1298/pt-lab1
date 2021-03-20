﻿using System;
using System.IO;    // do Directory.GetFiles

namespace PTLab1
{
    class Program
    {
        static void Task1(string path)
        {
            // znaleźć wszystkie katalogi w folderze głównym
            // wejść do katalogu
            // wyświetlić pliki z tego katalogu
            // wyświetlić nazwę katalogu
            // wejść do katalogu
            // wyświetlić pliki z katalogu
            
            //Console.WriteLine("Task1 function");
            var allDirectories = Directory.GetDirectories(path);

            Console.WriteLine("==============================");
            Console.WriteLine("number of folders in give path: " + allDirectories.Length);

            Console.WriteLine("all directories:");
            foreach (var i in allDirectories)       // wyświetlenie katalogów
            {
                //Console.WriteLine("directory foreach: " + i);
                Console.WriteLine(Path.GetFileName(i));         // to jest dobrze
                //Console.WriteLine(i);
            }
            Console.WriteLine("==============================");

            //Console.WriteLine("allDirectories: " + allDirectories); // źle wyświetla
            //Console.WriteLine(Directory.GetFiles(path));    // źle działa
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            Console.WriteLine("The number of files in given directory is {0}. All files:", files.Length);

            for (int i = 0; i < files.Length; i++)
            {
                //Console.WriteLine(files[i]);
                Console.WriteLine("Without path: " + Path.GetFileName(files[i]));
                //Console.WriteLine(files.DirectorySeparatorChar);
                //Path.DirectorySeparatorChar(files[i]);
                //Console.WriteLine("type of files[i]: " + files[i].GetType());       // System.String

            }
        }

        static void Main(string[] args)
        {
            // Solution Explorer -> Right click on project name -> Properties -> Debug -> Application arguments:
            //Console.WriteLine("Wyświetlanie przekazanych parametrów w `Application arguments`:");
            //Console.WriteLine($"parameter count = {args.Length}");
            //Console.WriteLine("parameter count second version = " + args.Length);
            
            //for (int i = 0; i < args.Length; i++)
            //{
            //    Console.WriteLine($"Arg[{i}] = [{args[i]}]");
            //}

            string path = args[0];      // ścieżka pobrana z parametru wywołania programu
            Console.WriteLine("path: " + path);
            //Console.WriteLine("Type of path:" + path.GetType());

            Task1(path);

        }
    }
}
