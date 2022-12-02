using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_02
{
    internal class Program
    {
        static void Part1()
        {
            var input = File.ReadAllLines("Input.txt");

            string[] moves = { "R", "P", "S" };

            int totalScore = 0;
            foreach (string lines in input)
            {
                int roundScore = 0;
                var parts = lines.Split(' ');
                string oppMove = moves[parts[0][0] - 65];
                string myMove = moves[parts[1][0] - 88];

                switch (myMove)
                {
                    case "R":
                        roundScore += 1;

                        switch (oppMove)
                        {
                            case "R":
                                roundScore += 3;
                                break;
                            case "P":
                                roundScore += 0;
                                break;
                            case "S":
                                roundScore += 6;
                                break;
                        }
                        break;
                    case "P":
                        roundScore += 2;

                        switch (oppMove)
                        {
                            case "R":
                                roundScore += 6;
                                break;
                            case "P":
                                roundScore += 3;
                                break;
                            case "S":
                                roundScore += 0;
                                break;
                        }
                        break;
                    case "S":
                        roundScore += 3;

                        switch (oppMove)
                        {
                            case "R":
                                roundScore += 0;
                                break;
                            case "P":
                                roundScore += 6;
                                break;
                            case "S":
                                roundScore += 3;
                                break;
                        }
                        break;
                }

                totalScore += roundScore;
            }

            Console.WriteLine(totalScore);
        }

        static void Part2()
        {
            var input = File.ReadAllLines("Input.txt");

            string[] moves = { "R", "P", "S" };

            int totalScore = 0;
            foreach (string lines in input)
            {
                int roundScore = 0;
                var parts = lines.Split(' ');
                string oppMove = moves[parts[0][0] - 65];
                string myMove = "";

                switch (parts[1])
                {
                    case "X":
                        switch (oppMove)
                        {
                            case "R":
                                myMove = "S";
                                break;
                            case "P":
                                myMove = "R";
                                break;
                            case "S":
                                myMove = "P";
                                break;
                        }
                        break;
                    case "Y":
                        myMove = oppMove;
                        break;
                    case "Z":
                        switch (oppMove)
                        {
                            case "R":
                                myMove = "P";
                                break;
                            case "P":
                                myMove = "S";
                                break;
                            case "S":
                                myMove = "R";
                                break;
                        }
                        break;
                }

                switch (myMove)
                {
                    case "R":
                        roundScore += 1;

                        switch (oppMove)
                        {
                            case "R":
                                roundScore += 3;
                                break;
                            case "P":
                                roundScore += 0;
                                break;
                            case "S":
                                roundScore += 6;
                                break;
                        }
                        break;
                    case "P":
                        roundScore += 2;

                        switch (oppMove)
                        {
                            case "R":
                                roundScore += 6;
                                break;
                            case "P":
                                roundScore += 3;
                                break;
                            case "S":
                                roundScore += 0;
                                break;
                        }
                        break;
                    case "S":
                        roundScore += 3;

                        switch (oppMove)
                        {
                            case "R":
                                roundScore += 0;
                                break;
                            case "P":
                                roundScore += 6;
                                break;
                            case "S":
                                roundScore += 3;
                                break;
                        }
                        break;
                }

                totalScore += roundScore;
            }

            Console.WriteLine(totalScore);
        }

        static void Main(string[] args)
        {
            Part1();

            Part2();

            Console.ReadKey();
        }
    }
}
