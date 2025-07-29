using System;
using System.Collections.Generic;

public class GraphMap
{
    private Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();

    // Khởi tạo đồ thị từ danh sách các cạnh
    public GraphMap()
    {
        string[] edges = new string[]
        {
            "42-0", "0-1", "1-47", "1-2", "2-24", "2-3", "3-27", "3-4",
            "27-28", "28-29", "4-5", "29-5", "29-30", "5-6",
            "44-14", "14-15", "15-16", "16-26", "16-17", "17-35", "17-18",
            "35-36", "36-37", "37-38", "18-20", "20-19", "37-20",
            "43-7", "7-8", "8-9", "9-25", "9-11", "11-31", "11-12",
            "31-32", "32-33", "33-34", "12-13", "33-13", "13-10", "24-26", "26-25"
        };

        foreach (var edge in edges)
        {
            var parts = edge.Split('-');
            int u = int.Parse(parts[0]);
            int v = int.Parse(parts[1]);
            AddEdge(u, v);
        }
    }

    // Thêm cạnh vào danh sách kề (vì là vô hướng, nên phải thêm 2 chiều)
    private void AddEdge(int u, int v)
    {
        if (!adjList.ContainsKey(u)) adjList[u] = new List<int>();
        if (!adjList.ContainsKey(v)) adjList[v] = new List<int>();

        adjList[u].Add(v);
        adjList[v].Add(u);
    }

    // Tìm đường đi ngắn nhất từ 'from' đến 'to' bằng BFS
    public int[] FindBestWay(int from, int to)
    {
        var queue = new Queue<int>();
        var visited = new HashSet<int>();
        var parent = new Dictionary<int, int>();

        queue.Enqueue(from);
        visited.Add(from);
        parent[from] = -1;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (current == to)
                break;

            foreach (int neighbor in adjList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    parent[neighbor] = current;
                }
            }
        }

        // Không tìm thấy đường đi
        if (!parent.ContainsKey(to))
            return new int[0];

        // Truy vết ngược để dựng đường đi
        List<int> path = new List<int>();
        int crawl = to;
        while (crawl != -1)
        {
            path.Add(crawl);
            crawl = parent[crawl];
        }

        path.Reverse();
        return path.ToArray();
    }
}
