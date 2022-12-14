using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_13
{
    public class ListItem : IComparable<ListItem>
    {
        public bool isList = false;
        public List<ListItem> opList;
        public int opInt;

        public ListItem(bool isList, int item, List<ListItem> list)
        {
            this.isList = isList;
            if (!isList)
            {
                opInt = item;
            }
            else
            {
                opList = list;
            }
        }

        public void AddItem(ListItem i)
        {
            opList.Add(i);
        }

        public int CompareTo(ListItem other)
        {
            if (!isList && !other.isList)
            {
                // Both values are integers
                return opInt.CompareTo(other.opInt);
            } else
            {
                if (isList)
                {
                    if (other.isList)
                    {
                        // Both values are lists: compare the first value of each list,
                        // then the second value, and so on. If the left list runs out of items first,
                        // the inputs are in the right order. If the right list runs out of items first,
                        // the inputs are not in the right order. If the lists are the same length and no
                        // comparison makes a decision about the order, continue checking the next part of the input.

                        int i = 0;

                        while (true)
                        {
                            if (i == opList.Count && i < other.opList.Count)
                            {
                                // Left list runs out first
                                // In the right order
                                return -1;
                            } else if (i == other.opList.Count && i < opList.Count)
                            {
                                // Right list runs out first
                                // Not in the right order
                                return 1;
                            } else if (i == opList.Count && i == other.opList.Count)
                            {
                                // lists are equal
                                return 0;
                            } else
                            {
                                // Not got to the end of either list
                                // compare the next value of each list
                                int result = opList[i].CompareTo(other.opList[i]);
                                if (result != 0) return result;
                            }

                            i++;
                        }
                    }
                    else
                    {
                        // This is list but other is integer
                        // convert other to list and compare again
                        return this.CompareTo(new ListItem(true, 0, new List<ListItem>() { other }));
                    }
                } else if (other.isList)
                {
                    // This is integer but other is list
                    // Convert this to list and compare again
                    return (new ListItem(true, 0, new List<ListItem>() { this })).CompareTo(other);
                }
            }

            throw new Exception("This error should never happen");
        }

        public override string ToString()
        {
            string result = "";
            if (isList)
            {
                result += "[";
                for (int i = 0; i < opList.Count; i++)
                {
                    result += opList[i];
                    if (i < opList.Count - 1) result += ",";
                }
                result += "]";
            } else
            {
                result = opInt.ToString();
            }

            return result;
        }
    }

    internal class Program
    {
        static ListItem ParseLine(string line)
        {
            if (line == "") return null;

            if (line == "[]") return new ListItem(true, 0, new List<ListItem>());

            if (!line.Contains(",") && !line.Contains("["))
            {
                // just a num '77'
                return new ListItem(false, int.Parse(line), null);
            }

            // Something containing brackets and maybe commas
            if (line[0] == '[')
            {
                // It's a list containing at least one other list
                // '[1,2,[3,4],5]'
                ListItem result = new ListItem(true, 0, new List<ListItem>());

                int i = 1;
                string currentSubstring = "";
                bool consumingInt = false;
                ListItem temp = null;
                while (i < line.Length)
                {
                    switch (line[i])
                    {
                        case ']':
                        case ',':
                            // Need to add the most recent item to the list and reset for next item
                            if (consumingInt) result.AddItem(ParseLine(currentSubstring));
                            else result.AddItem(temp);

                            consumingInt = false;
                            currentSubstring = "";

                            break;
                        case '[':
                            // It's the start of another list, need to find the substring and recurse
                            int numOpenBrackets = 1;
                            int length = 1;
                            int iStart = i;
                            while (numOpenBrackets > 0)
                            {
                                i++;
                                length++;
                                if (line[i] == '[') numOpenBrackets++;
                                else if (line[i] == ']') numOpenBrackets--;
                            }
                            temp = ParseLine(line.Substring(iStart, length));

                            break;
                        default:
                            currentSubstring += line[i];
                            consumingInt = true;
                            break;
                    }
                    i++;
                }
                return result;
            }

            throw new Exception("Error");
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");


            List<Tuple<ListItem,ListItem>> pairs = new List<Tuple<ListItem,ListItem>>();

            ListItem div1 = ParseLine("[[2]]");
            ListItem div2 = ParseLine("[[6]]");
            List<ListItem> packets = new List<ListItem>() { div1, div2 };

            string line1 = "", line2 = "";

            foreach (string line in lines)
            {
                if (line == "")
                {
                    // we've read 2
                    pairs.Add(new Tuple<ListItem,ListItem>(ParseLine(line1), ParseLine(line2)));

                    line1 = "";
                    line2 = "";
                } else
                {
                    packets.Add(ParseLine(line));
                    if (line1 == "") line1 = line;
                    else line2 = line;
                }
            }
            pairs.Add(new Tuple<ListItem, ListItem>(ParseLine(line1), ParseLine(line2)));

            int i = 1;
            List<int> inOrderIndices = new List<int>();
            foreach(var pair in pairs)
            {
                Console.WriteLine($"Pair {i}");
                Console.WriteLine(pair.Item1);
                Console.WriteLine(pair.Item2);
                int comp = pair.Item1.CompareTo(pair.Item2);
                if (comp > 0) Console.WriteLine("Not in order");
                else if (comp < 0)
                {
                    Console.WriteLine("In order");
                    inOrderIndices.Add(i);
                }
                else Console.WriteLine("Equal");
                Console.WriteLine();
                i++;
            }

            Console.WriteLine($"In order indices: {inOrderIndices.Select((ind) => ind + " ").Aggregate((a,b) => a + b)}");
            Console.WriteLine($"Sum of these: {inOrderIndices.Sum()}");

            packets.Sort();
            int ind1 = 0, ind2 = 0;
            for (int j = 0; j < packets.Count; j++)
            {
                if (packets[j] == div1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    ind1 = j + 1;
                } else if (packets[j] == div2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    ind2 = j + 1;
                }
                Console.WriteLine($"{j + 1}: {packets[j]}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.WriteLine($"\nResult: {ind1 * ind2}");

            Console.ReadKey();
        }
    }
}
