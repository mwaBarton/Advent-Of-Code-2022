using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_12
{
    public class Graph
    {
        private Node[,] nodes;
        private bool[,] M;
        public List<Node> startNodes;
        public Node targetNode;

        public Graph(Node[,] grid, List<Node> starts, Node end)
        {
            nodes = grid;
            startNodes = starts;
            targetNode = end;

            // Compute M
            M = new bool[nodes.GetLength(0) * nodes.GetLength(1), nodes.GetLength(0) * nodes.GetLength(1)];

            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                for (int x = 0; x < nodes.GetLength(0); x++)
                {
                    // For each node

                    // Check up
                    if (y > 0)
                    {
                        if (nodes[x, y - 1].elevation <= nodes[x, y].elevation + 1)
                        {
                            AddEdge(nodes[x, y], nodes[x, y - 1]);
                        }
                    }

                    // Check down
                    if (y < nodes.GetLength(1) - 1)
                    {
                        if (nodes[x, y + 1].elevation <= nodes[x, y].elevation + 1)
                        {
                            AddEdge(nodes[x, y], nodes[x, y + 1]);
                        }
                    }

                    // Check left
                    if (x > 0)
                    {
                        if (nodes[x - 1, y].elevation <= nodes[x, y].elevation + 1)
                        {
                            AddEdge(nodes[x, y], nodes[x - 1, y]);
                        }
                    }

                    // Check right
                    if (x < nodes.GetLength(0) - 1)
                    {
                        if (nodes[x + 1, y].elevation <= nodes[x, y].elevation + 1)
                        {
                            AddEdge(nodes[x, y], nodes[x + 1, y]);
                        }
                    }
                }
            }
        }

        public Node GetNodeById(int id)
        {
            var y = id / nodes.GetLength(0);
            var x = id % nodes.GetLength(0);
            return nodes[x, y];
        }

        public void AddEdge(Node a, Node b)
        {
            M[a.id, b.id] = true;
        }

        public Node GetNodeAtPosition(int x, int y)
        {
            return nodes[x, y];
        }

        public List<Node> GetNeighbours(Node n)
        {
            List<Node> result = new List<Node>();

            for (int i = 0; i < M.GetLength(0); i++)
            {
                // for each node id
                if (M[n.id, i] == true)
                {
                    result.Add(GetNodeById(i));
                }
            }

            return result;
        }

        public void ResetGraph()
        {
            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                for (int x = 0; x < nodes.GetLength(0); x++)
                {
                    // For each node
                    nodes[x, y].parent = null;
                    nodes[x, y].explored = false;
                }
            }
        }
    }

    public class Node
    {
        public char elevation;
        public int id;
        public int x;
        public int y;
        public bool explored = false;
        public Node parent = null;

        public Node(char c, int i, int X, int Y)
        {
            elevation = c;
            id = i;
            x = X;
            y = Y;
        }
    }

    internal class Program
    {
        static Graph GetGraph(string[] inputLines)
        {
            List<Node> starts = new List<Node>();
            Node end = null;

            Node[,] grid = new Node[inputLines[0].Length, inputLines.Length];

            for (int y = 0; y < inputLines.Length; y++)
            {
                for (int x = 0; x < inputLines[0].Length; x++)
                {
                    switch (inputLines[y][x])
                    {
                        case 'a':
                        case 'S':
                            grid[x, y] = new Node('a', x + y * inputLines[0].Length, x, y);
                            starts.Add(grid[x, y]);
                            break;
                        case 'E':
                            grid[x, y] = new Node('z', x + y * inputLines[0].Length, x, y);
                            end = grid[x, y];
                            break;
                        default:
                            grid[x, y] = new Node(inputLines[y][x], x + y * inputLines[0].Length, x, y);
                            break;
                    }
                    
                }
            }

            return new Graph(grid, starts, end);
        }

        static int BFS(Graph G, Node start)
        {
            G.ResetGraph();
            Queue<Node> Q = new Queue<Node>();
            start.explored = true;
            Q.Enqueue(start);

            while (Q.Count > 0)
            {
                Node node = Q.Dequeue();
                if (node == G.targetNode)
                {
                    break;
                }
                foreach (Node neighbour in G.GetNeighbours(node))
                {
                    if (!neighbour.explored)
                    {
                        neighbour.explored = true;
                        neighbour.parent = node;
                        Q.Enqueue(neighbour);
                    }
                }
            }

            int numSteps = -1;

            Node n = G.targetNode;
            while (n != null)
            {
                n = n.parent;
                numSteps++;
            }

            return numSteps;
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            Graph G = GetGraph(lines);

            int shortest = int.MaxValue;
            foreach(Node s in G.startNodes)
            {
                int steps = BFS(G, s);

                if (steps > 0)
                {
                    Console.WriteLine($"({s.x}, {s.y}): {steps}");
                    if (steps < shortest)
                    {
                        shortest = steps;
                    }
                }
                
            }

            Console.WriteLine(shortest);

            Console.ReadKey();
        }
    }
}
