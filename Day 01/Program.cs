using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            List<int> elfCals = new List<int>();

            int cals = 0;
            foreach (string line in input)
            {
                if (line == "")
                {
                    elfCals.Add(cals);
                    cals = 0;
                }
                else
                {
                    cals += int.Parse(line);
                }
            }
            elfCals.Add(cals);

            elfCals.Sort();

            Console.WriteLine(elfCals[elfCals.Count - 1] + elfCals[elfCals.Count - 2] + elfCals[elfCals.Count - 3]);

            Console.ReadKey();
        }
    }
}
