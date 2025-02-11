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
            int groupCount = 0;
            List<int> vertexesCopy = new(vertexes);
            List<int> group = [];

            do
            {
                group.Add(vertexesCopy[0]);
                vertexesCopy.RemoveAt(0);

                for (int i = 0; i < group.Count; i++)
                {
                    for (int j = 0; j < vertexes.Count; j++)
                    {
                        if (graph[group[i]][j] == '1' && !group.Contains(j))
                        {
                            vertexesCopy.Remove(j);
                            group.Add(j);
                        }
                    }
                }

                groupCount++;

            } while (vertexesCopy.Count > 0);

            return groupCount;
        }

        private static int Algorithm2(List<string> graph, List<int> vertexes)
        {
            int componentsCount = 0;
            List<int> groups = [];
            int maxGroup = 1;

            for (int i = 0; i < vertexes.Count; i++)
            {
                for(int j = 0; j < i+1; j++)
                {
                }
            }

            return componentsCount;
        }
    }
}