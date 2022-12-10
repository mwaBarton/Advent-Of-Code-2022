using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_10
{
    internal class Program
    {
        class RunningCommand
        {
            public string command;
            public int cyclesLeftBeforeCompletion;

            public RunningCommand(string Command, int CyclesLeft)
            {
                command = Command;
                cyclesLeftBeforeCompletion = CyclesLeft;
            }
        }

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int cyclesComplete = 0;
            int x = 1;
            string display = "";

            List<RunningCommand> running = new List<RunningCommand>();

            int signalStrength = 0;

            foreach (string command in input)
            {
                string opcode = command.Substring(0, 4);
                
                if (opcode == "noop")
                {
                    running.Add(new RunningCommand(command, 1));
                } else if (opcode == "addx")
                {
                    running.Add(new RunningCommand(command, 2));
                }

                while (running.Count > 0)
                {
                    List<RunningCommand> toRemove = new List<RunningCommand>();
                    for (int i = 0; i < running.Count; i++)
                    {
                        if ((cyclesComplete + 1 - 20) % 40 == 0)
                        {
                            signalStrength += (cyclesComplete + 1) * x;
                            Console.WriteLine($"During cycle {cyclesComplete + 1}: x = {x}, sig = {(cyclesComplete + 1) * x}");
                        }

                        if ((cyclesComplete) % 40 == 0)
                        {
                            display += "\n";
                        }

                        if (Math.Abs(x - ((cyclesComplete) % 40)) <= 1)
                            display += "#";
                        else
                            display += ".";

                        running[i].cyclesLeftBeforeCompletion -= 1;
                        if (running[i].cyclesLeftBeforeCompletion == 0)
                        {
                            if (running[i].command.Substring(0, 4) == "addx")
                            {
                                x += int.Parse(running[i].command.Substring(5));
                            }
                            toRemove.Add(running[i]);
                        }
                        foreach (var item in toRemove)
                        {
                            running.Remove(item);
                        }

                        cyclesComplete++;
                    }
                }
            }

            Console.WriteLine(signalStrength);
            Console.WriteLine(display);

            Console.ReadKey();
        }
    }
}
