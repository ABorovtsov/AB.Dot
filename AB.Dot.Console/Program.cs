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


            System.Console.WriteLine("Done!");
            System.Console.ReadLine();
        }
    }
}
