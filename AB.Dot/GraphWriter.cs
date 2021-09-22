using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AB.Dot
{
    public class GraphWriter
    {
        private readonly string _outputFolderPath;
        private readonly bool _directed;

        public GraphWriter(string outputFolderPath, bool directed=false)
        {
            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                throw new ArgumentException($"{nameof(outputFolderPath)} must be set");
            }

            _outputFolderPath = outputFolderPath;
            _directed = directed;
        }

        public async Task WriteDotAsync(IEnumerable<IEnumerable<int>> adjacencyList, string fileName="graph.dot")
        {
            if (adjacencyList == null) throw new ArgumentNullException(nameof(adjacencyList));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException($"{nameof(fileName)} cannot be null or white space");

            await File.WriteAllLinesAsync(Path.Combine(_outputFolderPath, fileName), GetDotLines(adjacencyList));
        }

        private IEnumerable<string> GetDotLines(IEnumerable<IEnumerable<int>> adjacencyList)
        {
            yield return _directed 
                ? "strict digraph G {"
                : "strict graph G {";
            
            var indexFrom = 0;
            foreach (IEnumerable<int> nodeEdges in adjacencyList)
            {
                var edgeCount = 0;

                foreach (int indexTo in nodeEdges)
                {
                    edgeCount++;

                    yield return _directed
                        ? $"    {indexFrom} -> {indexTo};"
                        : $"    {indexFrom} -- {indexTo};";
                }

                if (edgeCount == 0)
                {
                    yield return $"    {indexFrom};";
                    continue;
                }

                indexFrom++;
            }

            yield return "}";
        }
    }
}
