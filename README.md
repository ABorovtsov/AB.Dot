# About AB.Dot


<a href="https://www.nuget.org/packages/AB.Dot"><img src="https://img.shields.io/nuget/v/AB.Dot.svg?style=flat&logo=nuget"></a>


DOT (graph description) file generator for tree and graph.

![Example](./img/visualization.png)

There are many visualization tools like [Visual Studio Code extensions](https://marketplace.visualstudio.com/search?term=graphviz&target=VSCode&category=Programming%20Languages&sortBy=Relevance), online tools like [Graphviz Visual Editor](http://magjac.com/graphviz-visual-editor/) or [Viz.js](http://viz-js.com/).

# Example

The code below generates the dot-file for the graph adjacency list.

```c#
var graph = new List<LinkedList<int>>() // adjacency list
{
    new LinkedList<int>(new int[] {1,2}),
    new LinkedList<int>(new int[] {0,3}),
    new LinkedList<int>(new int[] {0}),
    new LinkedList<int>(new int[] {1}),
};

await new GraphWriter("./output").WriteDotAsync(graph, "graph.dot");
```