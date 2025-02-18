namespace Graph_2
{
    internal class Program
    {
        //алгоритм прима, ближайшего соседа
        //алгоритм крускала\краскала

        static void Main(string[] args)
        {
            List<int[]> graph = ReadGraph();
            List<int> vertexes = [];

            for (int i = 0; i < graph.Count; i++)
            {
                vertexes.Add(i);
            }

            Console.WriteLine(Algorithm1(graph, vertexes));
            Console.WriteLine(Algorithm2(graph, vertexes));
        }

        private static List<int[]> ReadGraph()
        {
            List<int[]> graph = new();

            StreamReader reader = new StreamReader("C:\\My\\Discretka\\Graph2.txt");
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

        private static int Algorithm1(List<int[]> graph, List<int> vertexes)
        {
            List<int> vertexesCopy = new(vertexes);
            List<int> tree = [];

            tree.Add(0);
            vertexesCopy.RemoveAt(0);

            int i = 0;
            int treePathLength = 0;
            int minPathLength = int.MaxValue;
            int minIndex = 0;

            do
            {
                for (int j = 0; j < vertexes.Count; j++)
                {
                    int pathLength = graph[tree[i]][j];

                    if (pathLength != 0 && pathLength < minPathLength && !tree.Contains(j))
                    {
                        minPathLength = pathLength;
                        minIndex = j;
                    }
                }

                i++;

                if (i == tree.Count)
                {
                    tree.Add(minIndex);
                    vertexesCopy.Remove(minIndex);
                    treePathLength += minPathLength;

                    i = 0;
                    minPathLength = int.MaxValue;
                    minIndex = 0;
                }

            } while (vertexesCopy.Count > 0);

            return treePathLength;
        }

        private static int Algorithm2(List<int[]> graph, List<int> vertexes)
        {
            List<(int, int, int)> paths = [];
            List<List<int>> groups = [[]];
            List<int> addedGroups = [];
            int pathLength = 0;

            for (int i = 0; i < vertexes.Count; i++)
            {
                for (int j = i+1; j < vertexes.Count; j++)
                {
                    int length = graph[i][j];

                    if (length != 0)
                    {
                        paths.Add((length, i, j));
                    }
                }
            }

            paths.Sort();

            groups[0].Add(paths[0].Item2);
            groups[0].Add(paths[0].Item3);
            pathLength += paths[0].Item1;
            paths.RemoveAt(0);

            void Concat()
            {
                for (int i = 1; i < addedGroups.Count; i++)
                {
                    int n = addedGroups[i];

                    groups[n].Remove(paths[0].Item2);
                    groups[n].Remove(paths[0].Item3);
                    groups[0] = groups[0].Concat(groups[n]).ToList();
                }

                for (int i = 1; i < addedGroups.Count; i++)
                {
                    int n = addedGroups[i];
                    groups.RemoveAt(n);
                    pathLength -= paths[0].Item1;
                }
            }

            while (groups[0].Count != vertexes.Count)
            {
                for(int i = 0; i < groups.Count; i++)
                {
                    bool firstContains = groups[i].Contains(paths[0].Item2);
                    bool secondContains = groups[i].Contains(paths[0].Item3);

                    if (firstContains != secondContains)
                    {
                        addedGroups.Add(i);
                        pathLength += paths[0].Item1;
                        if (firstContains)
                            groups[i].Add(paths[0].Item3);
                        else
                            groups[i].Add(paths[0].Item2);
                    }
                }

                if (addedGroups.Count == 0)
                {
                    groups.Add([]);

                    int count = groups.Count - 1;

                    groups[count].Add(paths[0].Item2);
                    groups[count].Add(paths[0].Item3);

                    pathLength += paths[0].Item1;
                }
                else if (addedGroups.Count > 1)
                {
                    Concat();
                }

                paths.RemoveAt(0);
                addedGroups.Clear();
            }

            return pathLength;
        }
    }
}