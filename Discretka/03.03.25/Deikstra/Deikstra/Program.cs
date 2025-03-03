namespace Dijkstra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<int[]> graph = ReadGraph();
            List<int> vertexes = [];

            for (int i = 0; i < graph.Count; i++)
            {
                vertexes.Add(i);
            }

            Console.WriteLine(DijkstraAlgorithm(graph, vertexes));
        }

        private static List<int[]> ReadGraph()
        {
            List<int[]> graph = new();

            StreamReader reader = new StreamReader("C:\\My\\Discretka\\Graph4.txt");
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

        private static int DijkstraAlgorithm(List<int[]> graph, List<int> vertexes)
        {
            int startID = int.Parse(Console.ReadLine());
            int endID = int.Parse(Console.ReadLine());
            int currentID = startID;
            int pathLength = 0;

            HashSet<int> visitedPoints = [];
            visitedPoints.Add(startID);

            List<int> collumn = [];

            for (int i = 0; i < vertexes.Count; i++)
            {
                collumn.Add(int.MaxValue);
            }

            List<int> paths = new(collumn);
            collumn[startID] = 0;

            for (int i = 0; i < vertexes.Count - 1; i++)
            {
                for (int j = 0; j < vertexes.Count; j++)
                {
                    if (!visitedPoints.Contains(j) && graph[currentID][j] != 0)
                    {
                        collumn[j] = Math.Min(graph[currentID][j] + pathLength, collumn[j]);
                    }
                }

                for (int j = 0; j < vertexes.Count; j++)
                {
                    if (!visitedPoints.Contains(j))
                    {
                        paths[j] = collumn[j];
                    }
                }

                pathLength = paths.Min();
                currentID = paths.IndexOf(pathLength);
                visitedPoints.Add(currentID);

                paths[currentID] = int.MaxValue;
            }

            return pathLength;
        }
    }
}
