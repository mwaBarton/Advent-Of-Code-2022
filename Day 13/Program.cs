using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_13
{
    public class ListItem
    {
        public bool isList = false;
        public List<ListItem> opList;
        public int opInt;

        public ListItem(bool isList, int item = 0, List<ListItem> list = null)
        {
            this.isList = isList;
            if (isList)
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

            if (!line.Contains(","))
            {
                // just a num
                return new ListItem(false, int.Parse(line));
            }

            // It's a list
            ListItem result = new ListItem(true, 0, new List<ListItem>());
            int i = 0;
            ListItem currentItem = null;
            while (i < line.Length)
            {
                char c = line[i];
                switch (c)
                {
                    case '[':
                        // Start of new list
                        // Find the next ]
                        int nextCloseBracket = i + 1;
                        while (line[nextCloseBracket] != ']') nextCloseBracket++;

                        string thisListSubstring = line.Substring(i + 1, nextCloseBracket - i - 1);

                        currentItem = ParseLine(thisListSubstring);
                        i = nextCloseBracket + 1;

                        break;
                    case ',':
                        if (currentItem != null)
                        result.AddItem(currentItem);
                        break;
                    default:
                        // Must be part of a number
                        // Find next comma or end
                        int nextCommaOrBracket = i + 1;
                        while (nextCommaOrBracket < line.Length && line[nextCommaOrBracket] != ',') nextCommaOrBracket++;

                        string numberSubstring = line.Substring(i + 1, nextCommaOrBracket - i - 1);

                        currentItem = ParseLine(numberSubstring);
                        i = nextCommaOrBracket + 1;
                        break;
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            string line1 = "", line2 = "";
            foreach (string line in lines)
            {
                if (line == "")
                {
                    // we've read 2
                    Console.Write(ParseLine(line1));
                    Console.Write(ParseLine(line2));
                } else
                {
                    if (line1 == "") line1 = line;
                    else line2 = line;
                }
            }
        }
    }
}
