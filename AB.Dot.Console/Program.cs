using System.Collections.Generic;
using System.Threading.Tasks;

namespace AB.Dot.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var graph = new List<LinkedList<int>>() // adjacency list
            {
                new LinkedList<int>(new int[] {1,2}),
                new LinkedList<int>(new int[] {0,3}),
                new LinkedList<int>(new int[] {0}),
                new LinkedList<int>(new int[] {1}),
            };

            await new GraphWriter("./output").WriteDotAsync(graph, "graph.dot");


            var node4 = new Node {Key = 4, Left = null, Right = null};
            var node3 = new Node {Key = 3, Left = null, Right = node4};
            var node_1 = new Node {Key = -1, Left = null, Right = null};
            var node0 = new Node {Key = 0, Left = null, Right = null};
            var node1 = new Node {Key = 1, Left = node_1, Right = node0};
            var node2 = new Node {Key = 2, Left = node1, Right = node3};
            
            var nodes = new List<Node> { node4, node3, node_1, node0, node1, node2 }; // the product of any traversal algorithm 
            await new BTreeWriter("./output").WriteDotAsync(nodes, "tree.dot");

            System.Console.WriteLine("Done!");
            System.Console.ReadLine();
        }
    }

    public class Node
    {
        public int Key { get; set; }
        
        public Node Left { get; set; }

        public Node Right { get; set; }
    }
}
