using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_07
{
    public class Fil
    {
        public string name;
        public bool isDir;
        public double size;
        public List<Fil> files;
        public Fil parentDir;

        public Fil(string inName, bool inDir, Fil parent, double inSize)
        {
            name = inName;
            isDir = inDir;
            parentDir = parent;
            size = inSize;
            if (isDir) files = new List<Fil>();
        }

        public void AddFile(Fil f)
        {
            files.Add(f);
        }

        public Fil GetFile(string name)
        {
            foreach (Fil f in files)
            {
                if (f.name == name) return f;
            }
            return null;
        }

        public override string ToString()
        {
            if (isDir) return $"{name} (dir)";
            else return $"{name} (file, {size})";
        }

        public double totalSize()
        {
            if (!isDir) return size;
            else
            {
                double result = 0;
                foreach (Fil f in files)
                {
                    result += f.totalSize();
                }
                return result;
            }
        }
    }
    internal class Program
    {
        static string GetSpaces(int num)
        {
            string result = "";

            for (int i = 0; i < num; i++)
            {
                result += " ";
            }
            return (result);
        }

        static void PrintTree(Fil root, int currentLevel)
        {
            Console.Write(GetSpaces(currentLevel) + " - ");
            Console.WriteLine(root);
            if (root.isDir)
            {
                foreach (Fil f in root.files)
                {
                    PrintTree(f, currentLevel + 1);
                }
            }

        }

        static double Part1(Fil root)
        {
            double result = 0;

            if (root.isDir)
            {
                Console.WriteLine($"{root.name}: {root.totalSize()}");
                if (root.totalSize() <= 100000)
                {
                    result += root.totalSize();
                }

                foreach (Fil d in root.files)
                {
                    result += Part1(d);
                }
            }

            return result;
        }

        static double Part2(Fil root, double target)
        {
            double result = double.MaxValue;

            if (root.isDir)
            {
                //Console.WriteLine($"{root.name}: {root.totalSize()}");
                if (root.totalSize() >= target)
                {
                    result = root.totalSize();
                }

                foreach (Fil d in root.files)
                {
                    if (Part2(d, target) < result)
                    {
                        result = Part2(d, target);
                    }
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").ToList();

            Fil currentFile = null, root = null;

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i][0] == '$')
                {
                    // command
                    if (input[i].Substring(2, 2) == "cd")
                    {
                        string dirName = input[i].Substring(5);

                        if (dirName == "/")
                        {
                            //Console.WriteLine($"Creating root");
                            root = new Fil("/", true, null, 0);
                            currentFile = root;
                        }
                        else if (dirName == "..")
                        {
                            currentFile = currentFile.parentDir;
                            //Console.WriteLine($"Changing up to dir {currentFile.name}");
                        }
                        else
                        {
                            currentFile = currentFile.GetFile(dirName);
                            //Console.WriteLine($"Changing down to {currentFile.name}");
                        }
                    }
                    else if (input[i].Substring(2, 2) == "ls")
                    {
                        //Console.WriteLine($"Adding Files");
                        i++;
                        while (i < input.Count && input[i][0] != '$')
                        {
                            var parts = input[i].Split(' ');
                            if (parts[0] == "dir") currentFile.AddFile(new Fil(parts[1], true, currentFile, 0));
                            else currentFile.AddFile(new Fil(parts[1], false, currentFile, double.Parse(parts[0])));
                            i++;
                        }
                        i--;
                    }
                }
                else
                {
                    // Not command
                }
            }

            PrintTree(root, 0);

            Console.WriteLine(Part1(root));
            Console.WriteLine($"target Space is {30000000 - (70000000 - root.totalSize())}");
            Console.WriteLine(Part2(root, 30000000 - (70000000 - root.totalSize())));

            Console.ReadKey();
        }
    }
}
