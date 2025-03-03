namespace Bridges
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int[]> graph = ReadGraph();
            List<int> vertexes = [];

            for (int i = 0; i < graph.Count; i++)
            {
                vertexes.Add(i);
            }

            List<(int, int)> edges = GetTreeVertexes(graph, vertexes);
            List<(int, int)> bridges = TestBridge(edges, graph, vertexes);

            foreach (var edge in bridges)
            {
                Console.WriteLine(edge);
            }
        }

        private static List<int[]> ReadGraph()
        {
            List<int[]> graph = new();

            StreamReader reader = new StreamReader("C:\\My\\Discretka\\Graph3.txt");
            string line = reader.ReadLine();

            while (line != null)
            {
                string[] stringLine = line.Split();
                int[] intLine = new int[stringLine.Length];

                for (int i = 0; i < stringLine.Length; i++)
                {
                    intLine[i] = int.Parse(stringLine[i]);
                }

                graph.Add(intLine);
                line = reader.ReadLine();
            }

            return graph;
        }

        private static List<(int, int)> GetTreeVertexes(List<int[]> graph, List<int> vertexes)
        {
            List<int> vertexesCopy = new(vertexes);
            List<int> tree = [];
            List<(int,int)> edges = [];

            tree.Add(0);
            vertexesCopy.RemoveAt(0);

            int i = 0;
            int firstPoint = 0;
            int treePathLength = 0;
            int minPathLength = int.MaxValue;
            int secondPoint = 0;

            do
            {
                for (int j = 0; j < vertexes.Count; j++)
                {
                    int pathLength = graph[tree[i]][j];

                    if (pathLength != 0 && pathLength < minPathLength && !tree.Contains(j))
                    {
                        minPathLength = pathLength;
                        secondPoint = j;
                        firstPoint = tree[i];
                    }
                }

                i++;

                if (i == tree.Count)
                {
                    tree.Add(secondPoint);
                    vertexesCopy.Remove(secondPoint);
                    treePathLength += minPathLength;
                    edges.Add((firstPoint, secondPoint));

                    i = 0;
                    minPathLength = int.MaxValue;
                    secondPoint = 0;
                    firstPoint = tree[0];
                }

            } while (vertexesCopy.Count > 0);

            return edges;
        }

        private static int GetGroupCount(List<int[]> graph, List<int> vertexes)
        {
            int groupCount = 1;
            List<int> vertexesCopy = new(vertexes);
            List<int> group = [];

            group.Add(vertexesCopy[0]);
            vertexesCopy.RemoveAt(0);
            int i = 0;

            do
            {
                for (int j = 0; j < vertexes.Count; j++)
                {
                    if (graph[group[i]][j] > 0 && !group.Contains(j))
                    {
                        vertexesCopy.Remove(j);
                        group.Add(j);
                    }
                }

                i++;

                if (i == group.Count)
                {
                    group.Clear();
                    group.Add(vertexesCopy[0]);
                    vertexesCopy.RemoveAt(0);
                    groupCount++;
                    i = 0;
                }

            } while (vertexesCopy.Count > 0);

            return groupCount;
        }

        private static List<(int, int)> TestBridge(List<(int, int)> edges, List<int[]> graph, List<int> vertexes)
        {
            List<(int, int)> bridges = [];

            for (int i = 0; i < edges.Count; i++)
            {
                (int, int) edge = edges[i];
                int n = edge.Item1;
                int m = edge.Item2;

                int x = graph[n][m];
                int y = graph[m][n];

                graph[n][m] = 0;
                graph[m][n] = 0;

                if (GetGroupCount(graph, vertexes) > 1)
                {
                    bridges.Add(edge);
                }

                graph[n][m] = x;
                graph[m][n] = y;
            }

            return bridges;
        }
    }
}
