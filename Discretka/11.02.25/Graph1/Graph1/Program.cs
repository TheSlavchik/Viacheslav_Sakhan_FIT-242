namespace Graph1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> graph = ReadGraph();
            List<int> vertexes = new();

            for (int i = 0; i < graph.Count; i++)
            {
                vertexes.Add(i);
            }

            Console.WriteLine(Algorithm1(graph, vertexes));
            Console.WriteLine(Algorithm2(graph, vertexes));
        }

        private static List<string> ReadGraph()
        {
            List<string> graph = new();

            StreamReader reader = new StreamReader("C:\\My\\Discretka\\Graph.txt");
            string line = reader.ReadLine();

            while (line != null)
            {
                graph.Add(line);
                line = reader.ReadLine();
            }

            return graph;
        }

        private static int Algorithm1(List<string> graph, List<int> vertexes)
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
                    if (graph[group[i]][j] == '1' && !group.Contains(j))
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

        private static int Algorithm2(List<string> graph, List<int> vertexes)
        {
            List<int> groups = [];
            int maxGroup = 0;

            for (int i = 0; i < vertexes.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (groups.Count < i + 1)
                    {
                        maxGroup++;
                        groups.Add(maxGroup);
                    }

                    if (graph[i][j] == '1')
                    {
                        groups[i] = groups[j] = Math.Min(groups[i], groups[j]);
                    }
                }

                maxGroup = groups.Max();
            }

            return maxGroup;
        }
    }
}
