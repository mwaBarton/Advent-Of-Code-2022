using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_09
{
    internal class Program
    {
        struct Point
        {
            public int x, y;

            public Point(int X, int Y)
            {
                x = X;
                y = Y;
            }
        }

        static bool UpdateTailPos(Point head, ref Point tail)
        {
            if (head.x > tail.x)
            {
                // head is right of tail
                if (head.y == tail.y && head.x == tail.x + 2)
                {
                    // just right
                    tail.x += 1;
                    return true;
                } else if (head.y == tail.y - 2)
                {
                    // right and above
                    tail.x += 1;
                    tail.y -= 1;
                    return true;
                } else if (head.y == tail.y + 2)
                {
                    // right and below
                    tail.x += 1;
                    tail.y += 1;
                    return true;
                }
            }

            if (head.x < tail.x)
            {
                // head is left of tail
                if (head.y == tail.y && head.x == tail.x - 2)
                {
                    // just left
                    tail.x -= 1;
                    return true;
                }
                else if (head.y == tail.y - 2)
                {
                    // left and above
                    tail.x -= 1;
                    tail.y -= 1;
                    return true;
                }
                else if (head.y == tail.y + 2)
                {
                    // left and below
                    tail.x -= 1;
                    tail.y += 1;
                    return true;
                }
            }

            if (head.y > tail.y)
            {
                // head is below tail
                if (head.x == tail.x && head.y == tail.y + 2)
                {
                    // just below
                    tail.y += 1;
                    return true;
                }
                else if (head.x == tail.x - 2)
                {
                    // below and left
                    tail.x -= 1;
                    tail.y += 1;
                    return true;
                }
                else if (head.x == tail.x + 2)
                {
                    // below and right
                    tail.x += 1;
                    tail.y += 1;
                    return true;
                }
            }

            if (head.y < tail.y)
            {
                // head is above tail
                if (head.x == tail.x && head.y == tail.y - 2)
                {
                    // just above
                    tail.y -= 1;
                    return true;
                }
                else if (head.x == tail.x - 2)
                {
                    // above and left
                    tail.x -= 1;
                    tail.y -= 1;
                    return true;
                }
                else if (head.x == tail.x + 2)
                {
                    // above and right
                    tail.x += 1;
                    tail.y -= 1;
                    return true;
                }
            }

            return false;
        }

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            Point head = new Point(0, 0);
            Point tail = new Point(0, 0);

            Dictionary<Point, int> visitedPoints = new Dictionary<Point, int>();

            foreach(string motion in input)
            {
                char dir = motion[0];
                int d = int.Parse(motion.Substring(2));

                Console.WriteLine($"Next move: {motion}");

                for (int i = 0; i < d; i++)
                {
                    switch (dir)
                    {
                        case 'L':
                            head.x -= 1;
                            break;
                        case 'R':
                            head.x += 1;
                            break;
                        case 'U':
                            head.y -= 1;
                            break;
                        case 'D':
                            head.y += 1;
                            break;
                    }
                    UpdateTailPos(head, ref tail);

                    bool found = false;
                    foreach (var point in visitedPoints.Keys)
                    {
                        if (point.x == tail.x && point.y == tail.y)
                        {
                            visitedPoints[point] += 1;
                            found = true;
                            break;
                        }
                    }
                    if (!found) visitedPoints.Add(new Point(tail.x, tail.y), 1);

                    Console.WriteLine($"Head is now at ({head.x}, {head.y}) - tail is now at ({tail.x}, {tail.y})");
                }
            }

            int total = 0;
            foreach (var point in visitedPoints.Keys)
            {
                total += 1;
            }

            Console.WriteLine(total);

            Console.ReadKey();
        }
    }
}
