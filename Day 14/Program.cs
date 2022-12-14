using System;
using System.Collections.Generic;
using System.IO;

namespace Day_14
{
    public struct Coord
    {
        public int X, Y;
    }

    internal class Program
    {
        static List<List<Coord>> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            List<List<Coord>> formations = new List<List<Coord>>();

            foreach (string line in lines)
            {
                List<Coord> coords = new List<Coord>();
                formations.Add(coords);
                // each line is a rock structure
                var coordStrings = line.Split(new string[] { " -> " }, StringSplitOptions.None);

                foreach (var coord in coordStrings)
                {
                    var parts = coord.Split(',');
                    coords.Add(new Coord { X = int.Parse(parts[0]), Y = int.Parse(parts[1]) });
                }
            }

            return formations;
        }

        static void Main(string[] args)
        {
            int[,] grid = new int[1000, 1000];
            // 0 - Air
            // 1 - Rock
            // 2 - Sand

            var formations = ReadInput();

            // Add rock formations to grid
            foreach (var formation in formations)
            {
                for (int i = 0; i < formation.Count - 1; i++)
                {
                    if (formation[i].X == formation[i + 1].X)
                    {
                        // Vertical
                        if (formation[i].Y > formation[i + 1].Y)
                        {
                            // Up
                            for (int y = formation[i].Y; y >= formation[i + 1].Y; y--)
                            {
                                grid[formation[i].X, y] = 1;
                            }
                        } else
                        {
                            // Down
                            for (int y = formation[i].Y; y <= formation[i + 1].Y; y++)
                            {
                                grid[formation[i].X, y] = 1;
                            }
                        }
                    } else
                    {
                        // Horizontal
                        if (formation[i].X < formation[i + 1].X)
                        {
                            // Right
                            for (int x = formation[i].X; x <= formation[i + 1].X; x++)
                            {
                                grid[x, formation[i].Y] = 1;
                            }
                        } else
                        {
                            // Left 
                            for (int x = formation[i].X; x >= formation[i + 1].X; x--)
                            {
                                grid[x, formation[i].Y] = 1;
                            }
                        }
                    }
                }
            }

            // Find abyss y
            int abyss = 0;
            for (int y = 0; y < 1000; y++)
            {
                for (int x = 0; x < 1000; x++)
                {
                    if (grid[x, y] > 0) abyss = y + 1;
                }
            }
            Console.WriteLine($"Abyss starts at: {abyss}");

            // Part 2 : add floor
            for (int x = 0; x < 1000; x++)
            {
                grid[x, abyss + 1] = 1;
            }

            // Start sand falling at 500, 0
            Coord currentSand;
            bool done = false;
            int numSand = 0;
            while (!done)
            {
                // Spawn new sand
                numSand++;
                currentSand = new Coord() { X = 500, Y = 0 };

                bool rest = false;
                while (!rest)
                {
                    //if (currentSand.Y == abyss) // Part 1
                    //{
                    //    // Done
                    //    done = true;
                    //    numSand--;
                    //    break;
                    if (grid[currentSand.X, currentSand.Y + 1] == 0)
                    {
                        currentSand.Y++;
                    } else if (grid[currentSand.X - 1, currentSand.Y + 1] == 0)
                    {
                        currentSand.Y++;
                        currentSand.X--;
                    } else if (grid[currentSand.X + 1, currentSand.Y + 1] == 0)
                    {
                        currentSand.Y++;
                        currentSand.X++;
                    } else
                    {
                        rest = true;
                        grid[currentSand.X, currentSand.Y] = 2;
                    }
                }

                if (currentSand.X == 500 && currentSand.Y == 0)
                {
                    done = true;
                }
            }

            Console.WriteLine($"Num grains spawned: {numSand}");

            Console.ReadKey();
        }
    }
}
