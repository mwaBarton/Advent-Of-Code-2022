using System;
using System.Collections.Generic;

namespace Day_11
{
    public class Number
    {
        public List<int> primeFact = new List<int>();

        public Number(int input)
        {
            for (int i = 2; i <= input; i++)
            {
                if (isPrime(i) && input % i == 0) primeFact.Add(i);
            }
        }

        public bool isPrime(int n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        public bool isDivisibleBy(int n)
        {
            return primeFact.Contains(n);
        }

        public int ToInt()
        {
            int result = 1;
            foreach (var item in primeFact)
            {
                result *= item;
            }
            return result;
        }
    }

    public class Monkey
    {
        public List<long> items = new List<long>();
        public Func<long, long> operation;
        public long divisibleBy;
        public int ifTrue;
        public int ifFalse;
        public long itemsInspected = 0;
        public Monkey(List<long> items, Func<long, long> operation, long divisibleBy, int ifTrue, int ifFalse)
        {

            //foreach (int item in items)
            //{
            //    this.items.Add(new Number(item));
            //}

            this.items = items;

            this.operation = operation;
            this.divisibleBy = divisibleBy;
            this.ifTrue = ifTrue;
            this.ifFalse = ifFalse;
        }
    }

    internal class Program
    {
        static string ListString(List<long> l)
        {
            string result = "";
            for (int i = 0; i < l.Count; i++)
            {
                result += l[i];
                if (i < l.Count - 1)
                {
                    result += ", ";
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            List<Monkey> monkeys = new List<Monkey>();
            //monkeys.Add(new Monkey(new List<long> { 79, 98 }, x => x * 19, 23, 2, 3));
            //monkeys.Add(new Monkey(new List<long> { 54, 65, 75, 74 }, x => x + 6, 19, 2, 0));
            //monkeys.Add(new Monkey(new List<long> { 79, 60, 97 }, x => x * x, 13, 1, 3));
            //monkeys.Add(new Monkey(new List<long> { 74 }, x => x + 3, 17, 0, 1));
            monkeys.Add(new Monkey(new List<long> { 98, 97, 98, 55, 56, 72 }, x => x * 13, 11, 4, 7));
            monkeys.Add(new Monkey(new List<long> { 73, 99, 55, 54, 88, 50, 55 }, x => x + 4, 17, 2, 6));
            monkeys.Add(new Monkey(new List<long> { 67, 98 }, x => x * 11, 5, 6, 5));
            monkeys.Add(new Monkey(new List<long> { 82, 91, 92, 53, 99 }, x => x + 8, 13, 1, 2));
            monkeys.Add(new Monkey(new List<long> { 52, 62, 94, 96, 52, 87, 53, 60 }, x => x * x, 19, 3, 1));
            monkeys.Add(new Monkey(new List<long> { 94, 80, 84, 79 }, x => x + 5, 2, 7, 0));
            monkeys.Add(new Monkey(new List<long> { 89 }, x => x + 1, 3, 0, 5));
            monkeys.Add(new Monkey(new List<long> { 70, 59, 63 }, x => x + 3, 7, 4, 3));

            for (int round = 0; round < 10000; round++)
            {
                for (int i = 0; i < monkeys.Count; i++)
                {
                    //Console.WriteLine($"Monkey {i}: ");
                    for (int j = 0; j < monkeys[i].items.Count; j++)
                    {
                        //Console.WriteLine($" Monkey inspects an item with a worry level of {monkeys[i].items[j]}.");
                        monkeys[i].items[j] = monkeys[i].operation(monkeys[i].items[j]) % 9699690;
                        monkeys[i].itemsInspected++;
                        //Console.WriteLine($"  Item becomes {monkeys[i].items[j]}");
                        //monkeys[i].items[j] = monkeys[i].items[j] / 3;
                        //Console.WriteLine($"  Item divides by 3 to become {monkeys[i].items[j]}");


                        if (monkeys[i].items[j] % monkeys[i].divisibleBy == 0)
                        {
                            //Console.WriteLine($"  Divisible by {monkeys[i].divisibleBy}");
                            //Console.WriteLine($"  Item thrown to {monkeys[i].ifTrue}");
                            monkeys[monkeys[i].ifTrue].items.Add(monkeys[i].items[j]);
                        }
                        else
                        {
                            //Console.WriteLine($"  Not divisible by {monkeys[i].divisibleBy}");
                            //Console.WriteLine($"  Item thrown to {monkeys[i].ifFalse}");
                            monkeys[monkeys[i].ifFalse].items.Add(monkeys[i].items[j]);
                        }
                    }
                    monkeys[i].items.Clear();
                }

                if ((round + 1) % 1000 == 0)
                {
                    Console.WriteLine($"After round {round + 1}, the monkeys are holding items with these worry levels:");

                    for (int i = 0; i < monkeys.Count; i++)
                    {
                        //Console.WriteLine($"Monkey {i}: {ListString(monkeys[i].items)}");
                    }
                    Console.WriteLine();

                    
                }

                //Console.WriteLine($"After round {round + 1}, the monkeys are holding items with these worry levels:");
                //for (int i = 0; i < monkeys.Count; i++)
                //{
                //    Console.WriteLine($"Monkey {i} inspected: {monkeys[i].itemsInspected}");
                //}
                //Console.WriteLine();

                //Console.ReadKey();

            }

            for (int i = 0; i < monkeys.Count; i++)
            {
                Console.WriteLine($"Monkey {i}: {monkeys[i].itemsInspected}");
            }

            monkeys.Sort((a, b) => (int)(b.itemsInspected - a.itemsInspected));

            Console.WriteLine(monkeys[0].itemsInspected * monkeys[1].itemsInspected);

            Console.ReadKey();
        }
    }
}
