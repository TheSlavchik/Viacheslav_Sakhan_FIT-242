namespace Floyd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int[]> graph = ReadGraph();

            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph.Count; j++)
                {
                    Console.Write($"{graph[i][j]} ");
                }

                Console.WriteLine();
            }

            Floyd(graph, graph.Count);
            Console.WriteLine();

            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph.Count; j++)
                {
                    Console.Write($"{graph[i][j]} ");
                }

                Console.WriteLine();
            }
        }

        private static List<int[]> ReadGraph()
        {
            List<int[]> graph = [];

            StreamReader reader = new("C:\\My\\Discretka\\Graph6.txt");
            string line = reader.ReadLine();

            while (line != null)
            {
                string[] stringLine = line.Split();
                int[] intLine = new int[stringLine.Length];

                for (int i = 0; i < stringLine.Length; i++)
                {
                    int value = int.Parse(stringLine[i]);
                    if (value == 0)
                        value = int.MaxValue;
                    intLine[i] = value;
                }

                if (intLine[graph.Count] > 0)
                {
                    intLine[graph.Count] = 0;
                }

                graph.Add(intLine);
                line = reader.ReadLine();
            }

            Console.WriteLine(graph.Count);
            return graph;
        }

        private static List<int[]> Floyd(List<int[]> graph, int vertexesCount) 
        {
            for (int k = 0; k < vertexesCount; k++)
            {
                for (int j = 0; j < vertexesCount; j++)
                {
                    for (int i = 0; i < vertexesCount; i++)
                    {
                        if (graph[i][k] != int.MaxValue && graph[k][j] != int.MaxValue)
                        {
                            graph[i][j] = Math.Min(graph[i][j], graph[i][k] + graph[k][j]);
                        }
                    }
                }
            } 

            return graph;
        }
    }
}
