using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_06
{
    internal class Program
    {
        static bool AreAllCharactersDifferent(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    if (i != j && s[i] == s[j]) return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");


            int startOfPacket = 0;
            for (int i = 13; i < input.Length; i++)
            {
                string sub = input.Substring(i - 13, 14);
                //Console.WriteLine(sub);
                if (AreAllCharactersDifferent(sub))
                {
                    startOfPacket = i + 1;
                    Console.WriteLine(startOfPacket);
                }
            }

            Console.ReadKey();
        }
    }
}
