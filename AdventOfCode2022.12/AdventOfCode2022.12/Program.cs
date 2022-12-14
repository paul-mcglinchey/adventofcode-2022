var input = File.ReadAllLines("./input.txt");
var width = input.First().Length;

var startIndex = string.Join("", input).IndexOf('S');
var endIndex = string.Join("", input).IndexOf('E');

Node start = new Node(null, (startIndex % width, startIndex / width), (byte)'a');
start.G = start.H = 0;
Node end = new Node(null, (endIndex % width, endIndex / width), (byte)'z');
end.G = end.H = 0;

// Part one
Console.WriteLine($"Part one: {Astar(start, end)}");

// Part two
var lowestAstar = string.Join("", input)
    .Select((x, i) => new { index = i, height = x })
    .Where(x => x.height == 'a')
    .Select(x => new Node(null, (x.index % width, x.index / width), (byte)'a'))
    .Where(x => x.GetChildren(input, width - 1, input.Length - 1).Any(c => c.Height == (byte)'b'))
    .Select(x => Astar(x, end))
    .Min();

Console.WriteLine($"Part two: {lowestAstar}");

int? Astar(Node start, Node end)
{
    var openList = new List<Node>();
    var closedList = new List<Node>();

    openList.Add(start);

    while (openList.Count > 0)
    {
        var currentNode = openList.First();
        var currentIndex = 0;

        for (int i = 1; i < openList.Count; i++)
        {
            if (openList[i].F < currentNode.F)
            {
                currentNode = openList[i];
                currentIndex = i;
            }
        }

        openList.RemoveAt(currentIndex);
        closedList.Add(currentNode);

        if (currentNode.Equals(end))
        {
            var path = new HashSet<(int, int)>();
            var current = currentNode;

            while (current != null)
            {
                path.Add(current.Position);
                current = current.Parent;
            }

            return path.Count() - 1;
        }

        var currentNodeCharacter = input[currentNode.Position.Item2][currentNode.Position.Item1];
        var currentNodeHeight = (byte)(currentNodeCharacter == 'S' ? 'a' : currentNodeCharacter == 'E' ? 'z' : currentNodeCharacter);

        foreach (Node child in currentNode.GetChildren(input, width - 1, input.Length - 1))
        {
            if (closedList.Select(x => x.Position).Contains(child.Position))
            {
                continue;
            }

            child.G = currentNode.G + 1;
            child.H = (int)Math.Sqrt(Math.Pow(child.Position.Item1 - end.Position.Item1, 2) + Math.Pow(child.Position.Item2 - end.Position.Item2, 2));

            if (openList.Select(x => x.Position).Contains(child.Position))
            {
                continue;
            }

            if (!openList.Select(x => x.Position).Contains(child.Position))
            {
                openList.Add(child);
            }
        }
    }

    return null;
}

public class Node
{
    public Node(Node? parent, (int, int) position, byte height)
    {
        Parent = parent;
        Position = position;
        Height = height;
    }

    public Node? Parent { get; set; }

    public (int, int) Position { get; set; }

    public byte Height { get; set; }

    public int G { get; set; }

    public int H { get; set; }

    public int F => G + H;

    public bool Equals(Node other) => this.Position == other.Position;

    public List<Node> GetChildren(string[] grid, int widthOfGrid, int heightOfGrid)
    {
        var children = new List<Node>();

        foreach ((int, int) pos in new HashSet<(int, int)>() { (-1, 0), (0, 1), (1, 0), (0, -1) })
        {
            var nodePosition = (this.Position.Item1 + pos.Item1, this.Position.Item2 + pos.Item2);

            if (nodePosition.Item1 > widthOfGrid || nodePosition.Item1 < 0 || nodePosition.Item2 > heightOfGrid || nodePosition.Item2 < 0)
            {
                continue;
            }

            var character = grid[nodePosition.Item2][nodePosition.Item1];
            var height = (byte)(character == 'S' ? 'a' : character == 'E' ? 'z' : character);

            // make sure the node is reachable (not higher than 1 unit)
            if (height > this.Height + 1)
            {
                continue;
            }

            children.Add(new Node(this, nodePosition, height));
        };

        return children;
    }
}