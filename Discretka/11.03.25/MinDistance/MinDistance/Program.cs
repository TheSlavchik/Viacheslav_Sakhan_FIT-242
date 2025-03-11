namespace MinDistance
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

            List<int> test = FordBellman(graph, vertexes);

            for (int i = 0;i < test.Count; i++)
            {
                Console.WriteLine(test[i]);
            }
        }

        private static List<int[]> ReadGraph()
        {
            List<int[]> graph = new();

            StreamReader reader = new StreamReader("C:\\My\\Discretka\\Graph5.txt");
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

                graph.Add(intLine);
                line = reader.ReadLine();
            }

            return graph;
        }

        private static List<int> FordBellman(List<int[]> graph, List<int> vertexes)
        {
            int startPoint = int.Parse(Console.ReadLine());
            List<List<int>> lambdas = [new(new int[vertexes.Count])];
            int n = 1;
            List<int> currentLambda;
            List<int> prevLambda;

            for (int i = 0; i < vertexes.Count; i++)
            {
                if (i != startPoint)
                {
                    lambdas[0][i] = int.MaxValue;
                }
            }

            do
            {
                lambdas.Add(new(new int[vertexes.Count]));
                currentLambda = lambdas[n];
                prevLambda = lambdas[n - 1];

                for (int j = 0; j < vertexes.Count; j++)
                {
                    if (j == startPoint)
                    {
                        currentLambda[j] = 0;
                        continue;
                    }

                    int min = int.MaxValue;

                    for (int k = 0; k < vertexes.Count; k++)
                    {
                        if (prevLambda[k] != int.MaxValue && graph[k][j] != int.MaxValue)
                        {
                            min = Math.Min(prevLambda[k] + graph[k][j], min);
                        }
                    }

                    currentLambda[j] = min;
                }

                n++;
            } while (n < vertexes.Count && prevLambda != currentLambda);

            return lambdas[^1];
        }
    }
}