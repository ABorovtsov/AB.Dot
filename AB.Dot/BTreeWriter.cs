using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace AB.Dot
{
    public class BTreeWriter
    {
        private readonly string _outputFolderPath;

        public BTreeWriter(string outputFolderPath)
        {
            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                throw new ArgumentException($"{nameof(outputFolderPath)} must be set");
            }

            _outputFolderPath = outputFolderPath;
        }

        public async Task WriteDotAsync<TNode>(IEnumerable<TNode> nodes, string fileName="graph.dot")
        {
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException($"{nameof(fileName)} cannot be null or white space");

            ValidateTNode(typeof(TNode));

            await File.WriteAllLinesAsync(Path.Combine(_outputFolderPath, fileName), GetDotLines(nodes));
        }

        private void ValidateTNode(Type type)
        {
            if (type.GetProperty("Key") == null)
            {
                throw new ArgumentException("\"TNode\" mast have the \"Key\" property");
            }

            if (type.GetProperty("Left") == null)
            {
                throw new ArgumentException("\"TNode\" mast have the \"Left\" property");
            }

            if (type.GetProperty("Right") == null)
            {
                throw new ArgumentException("\"TNode\" mast have the \"Right\" property");
            }
        }

        private IEnumerable<string> GetDotLines<TNode>(IEnumerable<TNode> nodes)
        {
            yield return "digraph G {";
            
            foreach (dynamic node in nodes)
            {
                var hasRelations = node.Right != null || node.Left != null;

                if (node.Left != null)
                {
                    yield return $"    {node.Key} -> {node.Left.Key};";
                }
                else if (hasRelations)
                {
                    yield return $"    \"{node.Key}LeftNull\" [label=\"NULL\"];";
                    yield return $"    {node.Key} -> \"{node.Key}LeftNull\";";
                }

                if (node.Right != null)
                {
                    yield return $"    {node.Key} -> {node.Right.Key};";
                }
                else if (hasRelations)
                {
                    yield return $"    \"{node.Key}RightNull\" [label=\"NULL\"];";
                    yield return $"    {node.Key} -> \"{node.Key}RightNull\";";
                }

                if (!hasRelations)
                {
                    yield return $"    {node.Key};";
                }
            }

            yield return "}";
        }
    }
}
