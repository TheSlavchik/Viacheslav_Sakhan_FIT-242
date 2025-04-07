namespace WaveAlgorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (int, int) startPoint = (int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
            (int, int) endPoint = (int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
            List<List<string>> map = ReadMap();

            Console.WriteLine(WaveAlgorithm(map, startPoint, endPoint));
        }

        private static List<List<string>> ReadMap()
        {
            List<List<string>> map = [];

            StreamReader reader = new("C:\\My\\Discretka\\Map.txt");
            string line = reader.ReadLine();
            List<string> lineList;
            List<string> xStr = new();

            for (int i = 0; i < line.Length + 2; i++)
            {
                xStr.Add("x");
            }

            map.Add(xStr);

            while (line != null)
            {
                lineList = new();
                lineList.Add("x");

                for(int i = 0; i < line.Length; i++)
                {
                    lineList.Add(line[i].ToString());
                }

                lineList.Add("x");

                map.Add(lineList);
                line = reader.ReadLine();
            }

            map.Add(xStr);
            return map;
        }

        private static int WaveAlgorithm(List<List<string>> map, (int, int) start, (int, int) end)
        {
            int n = 0;
            map[start.Item1][start.Item2] = "0";

            while (map[end.Item1][end.Item2] == " ")
            {
                bool isReplaced = false;

                for (int i = 0; i < map.Count; i++)
                {
                    for (int j = 0; j < map.Count; j++)
                    {
                        if (map[i][j] == n.ToString())
                        {
                            if (map[i - 1][j] == " ")
                            {
                                map[i - 1][j] = (n+1).ToString();
                                isReplaced = true;
                            }
                            if (map[i][j+1] == " ")
                            {
                                map[i][j + 1] = (n + 1).ToString();
                                isReplaced = true;
                            }
                            if (map[i + 1][j] == " ")
                            {
                                map[i + 1][j] = (n + 1).ToString();
                                isReplaced = true;
                            }
                            if (map[i][j - 1] == " ")
                            {
                                map[i][j - 1] = (n + 1).ToString();
                                isReplaced = true;
                            }
                        }
                    }
                }

                if (!isReplaced)
                {
                    return -1;
                }

                n++;
            }

            return int.Parse(map[end.Item1][end.Item2]);
        }
    }
}
