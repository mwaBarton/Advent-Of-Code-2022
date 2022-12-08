using System;
using System.IO;

namespace Day_08
{
    internal class Program
    {
        static void PrintGrid(int[,] g)
        {
            for (int y = 0; y < g.GetLength(1); y++)
            {
                for (int x = 0; x < g.GetLength(0); x++)
                {
                    Console.Write(g[x, y]);
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int[,] trees = new int[input[0].Length, input.Length];
            char[,] visibility = new char[trees.GetLength(0), trees.GetLength(1)];
            int[,] lowestOuterTree = new int[trees.GetLength(0), trees.GetLength(1)];
            int[,] treesSeen = new int[trees.GetLength(0), trees.GetLength(1)];

            for (int y = 0; y < input.Length; y++)
            {
                string row = input[y];
                for (int x = 0; x < row.Length; x++)
                {
                    trees[x, y] = int.Parse(row[x].ToString());
                    if (x == 0 || y == 0 || x == row.Length - 1 || y == input.Length - 1)
                    {
                        visibility[x, y] = 'T';
                        lowestOuterTree[x, y] = trees[x, y];
                    }
                    else visibility[x, y] = 'F';
                    Console.Write(trees[x, y]);
                }
                Console.WriteLine();
            }

            bool changed = false;
            int highestScenicScore = 0;
            do
            {
                changed = false;
                for (int y = 1; y < trees.GetLength(1) - 1; y++)
                {
                    for (int x = 1; x < trees.GetLength(0) - 1; x++)
                    {
                        // Search up
                        bool vis = true;
                        for (int i = y - 1; i >= 0; i--)
                        {
                            if (trees[x, y] <= trees[x, i]) vis = false;
                        }
                        if (vis) visibility[x, y] = 'T';
                        int upCount = 0;
                        for (int i = y - 1; i >= 0; i--)
                        {
                            if (trees[x, y] > trees[x, i]) upCount++;
                            else
                            {
                                upCount++;
                                break;
                            }
                        }

                        // Search down
                        vis = true;
                        for (int i = y + 1; i < trees.GetLength(1); i++)
                        {
                            if (trees[x, y] <= trees[x, i]) vis = false;
                        }
                        if (vis) visibility[x, y] = 'T';
                        int downCount = 0;
                        for (int i = y + 1; i < trees.GetLength(1); i++)
                        {
                            if (trees[x, y] > trees[x, i]) downCount++;
                            else
                            {
                                downCount++;
                                break;
                            }
                        }

                        // Search left
                        vis = true;
                        for (int i = x - 1; i >= 0; i--)
                        {
                            if (trees[x, y] <= trees[i, y]) vis = false;
                        }
                        if (vis) visibility[x, y] = 'T';
                        int leftCount = 0;
                        for (int i = x - 1; i >= 0; i--)
                        {
                            if (trees[x, y] > trees[i, y]) leftCount++;
                            else
                            {
                                leftCount++;
                                break;
                            }
                        }

                        // Search right
                        vis = true;
                        for (int i = x + 1; i < trees.GetLength(0); i++)
                        {
                            if (trees[x, y] <= trees[i, y]) vis = false;
                        }
                        if (vis) visibility[x, y] = 'T';
                        int rightCount = 0;
                        for (int i = x + 1; i < trees.GetLength(0); i++)
                        {
                            if (trees[x, y] > trees[i, y]) rightCount++;
                            else
                            {
                                rightCount++;
                                break;
                            }
                        }

                        int scenicScore = upCount * downCount * leftCount * rightCount;
                        Console.WriteLine($"At [{x}, {y}]: up: {upCount}, down: {downCount}, left: {leftCount}, right: {rightCount}, scenicScore: {scenicScore}");
                        if (scenicScore > highestScenicScore) highestScenicScore = scenicScore;

                        //int min = Math.Min(lowestOuterTree[x - 1, y],
                        //        Math.Min(lowestOuterTree[x + 1, y],
                        //        Math.Min(lowestOuterTree[x, y - 1], lowestOuterTree[x, y + 1])));

                        //if (lowestOuterTree[x, y] < trees[x, y])
                        //{
                        //    lowestOuterTree[x, y] = trees[x, y];
                        //    changed = true;
                        //} else if (lowestOuterTree[x, y] <  min)
                        //{
                        //    lowestOuterTree[x, y] = min;
                        //    changed = true;
                        //}

                        //Console.WriteLine();
                        //PrintGrid(lowestOuterTree);
                        //Console.ReadKey();

                        //if (visibility[x, y] != 'T')
                        //{
                        //    if (visibility[x - 1, y] != 'F' && trees[x, y] >= trees[x - 1, y])
                        //    {
                        //        if (trees[x, y] == trees[x - 1, y])
                        //        {
                        //            if (visibility[x, y] != 'S')
                        //            {
                        //                visibility[x, y] = 'S';
                        //                changed = true;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            visibility[x, y] = 'T';
                        //            changed = true;
                        //        }
                        //    }
                        //    else if (visibility[x + 1, y] != 'F' && trees[x, y] > trees[x + 1, y])
                        //    {
                        //        if (trees[x, y] == trees[x + 1, y])
                        //        {
                        //            if (visibility[x, y] != 'S')
                        //            {
                        //                visibility[x, y] = 'S';
                        //                changed = true;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            visibility[x, y] = 'T';
                        //            changed = true;
                        //        }
                        //    }
                        //    else if (visibility[x, y - 1] != 'F' && trees[x, y] > trees[x, y - 1])
                        //    {
                        //        if (trees[x, y] == trees[x, y - 1])
                        //        {
                        //            if (visibility[x, y] != 'S')
                        //            {
                        //                visibility[x, y] = 'S';
                        //                changed = true;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            visibility[x, y] = 'T';
                        //            changed = true;
                        //        }
                        //    }
                        //    else if (visibility[x, y + 1] != 'F' && trees[x, y] > trees[x, y + 1])
                        //    {
                        //        if (trees[x, y] == trees[x, y + 1])
                        //        {
                        //            if (visibility[x, y] != 'S')
                        //            {
                        //                visibility[x, y] = 'S';
                        //                changed = true;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            visibility[x, y] = 'T';
                        //            changed = true;
                        //        }
                        //    }
                        //}
                    }
                }
            } while (changed);


            int count = 0;
            Console.WriteLine();
            for (int y = 0; y < trees.GetLength(1); y++)
            {
                for (int x = 0; x < trees.GetLength(0); x++)
                {
                    if (visibility[x, y] == 'T') count++;
                    Console.Write(visibility[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Part 1: {count}");

            Console.WriteLine($"Part 2: {highestScenicScore}");

            Console.ReadKey();
        }
    }
}
