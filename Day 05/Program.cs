using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_05
{
    internal class Program
    {
        static string prettyCrateLine(string[] crateLine)
        {
            string result = "";

            for(int i = 0; i < crateLine.Length; i++)
            {
                result += crateLine[i];
                if (i < crateLine.Length - 1)
                {
                    result += ", ";
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            const int numStacks = 9;

            // Get two lists of the two parts of the input
            List<string[]> inputCrates = new List<string[]>();
            List<string> inputCommands = new List<string>();

            using(StreamReader sr = new StreamReader("input.txt"))
            {
                string line = sr.ReadLine();
                do
                {
                    string[] temp = new string[numStacks];
                    for (int i = 0; i < temp.Length; i++)
                    {
                        int index = i * 4 + 1;
                        if (line.Length >= index + 1)
                        {
                            if (line[index] == ' ')
                            {
                                temp[i] = "";
                            } else
                            {
                                temp[i] = line[index].ToString();
                            }
                        } else
                        {
                            temp[i] = "";
                        }
                    }
                    inputCrates.Add(temp);

                    line = sr.ReadLine();

                } while (line[1] != '1');

                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Replace("move ", "").Replace("from ", "").Replace("to ", "");
                    inputCommands.Add(line);
                }
            }

            // Load the stacks
            List<Stack<string>> stacks = new List<Stack<string>>();
            for (int i = 0; i < numStacks; i++)
            {
                stacks.Add(new Stack<string>());

                for (int j = inputCrates.Count - 1; j >= 0; j--)
                {
                    if (inputCrates[j][i] != "") stacks[i].Push(inputCrates[j][i]);
                }
            }

            // Print the inputs
            Console.WriteLine(inputCrates.Select((a) => { return prettyCrateLine(a); }).Aggregate((a, b) => { return a + "\n" + b; }));
            Console.WriteLine(inputCommands.Aggregate((a, b) => { return a + "\n" + b; }));

            // Execute the commands (part 1)
            //foreach (string c in inputCommands)
            //{
            //    var parts = c.Split(' ');

            //    for (int i = 0; i < int.Parse(parts[0]); i++)
            //    {
            //        stacks[int.Parse(parts[2]) - 1].Push(stacks[int.Parse(parts[1]) - 1].Pop());
            //    }
            //}

            // Execute the commands (part 2)
            Stack<string> tempStack = new Stack<string>();
            foreach (string c in inputCommands)
            {
                var parts = c.Split(' ');

                for (int i = 0; i < int.Parse(parts[0]); i++)
                {
                    tempStack.Push(stacks[int.Parse(parts[1]) - 1].Pop());
                }
                for (int i = 0; i < int.Parse(parts[0]); i++)
                {
                    stacks[int.Parse(parts[2]) - 1].Push(tempStack.Pop());
                }
            }

            Console.WriteLine();
            foreach (var stack in stacks)
            {
                Console.WriteLine(prettyCrateLine(stack.ToArray()));
            }
            
            Console.ReadKey();
        }
    }
}
