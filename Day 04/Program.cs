using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_04
{
    public struct Elf
    {
        public int left, right;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("Input.txt");

            int count1 = 0, count2 = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var e1 = parts[0].Split('-');
                var e2 = parts[1].Split('-');
                Elf elf1 = new Elf() { left = int.Parse(e1[0]), right = int.Parse(e1[1]) };
                Elf elf2 = new Elf() { left = int.Parse(e2[0]), right = int.Parse(e2[1]) };

                // Part 1
                if (elf1.left >= elf2.left && elf1.right <= elf2.right)
                {
                    count1++;
                } else if (elf2.left >= elf1.left && elf2.right <= elf1.right)
                {
                    count1++;
                }

                // Part 2
                if (elf2.right >= elf1.left && elf2.right <= elf1.right)
                {
                    count2++;
                } else if (elf2.right >= elf1.right && elf2.left <= elf1.right)
                {
                    count2++;
                }
            }

            Console.WriteLine(count1);
            Console.WriteLine(count2);
            Console.ReadKey();
        }
    }
}
