var input = File.ReadAllLines("./input.txt");
var width = input.First().Length;

var startIndex = string.Join("", input).IndexOf('S');
var endIndex = string.Join("", input).IndexOf('E');

Node start = new Node(null, (startIndex % width, startIndex / width));
start.G = start.H = 0;

Node end = new Node(null, (endIndex % width, endIndex / width));
end.G = end.H = 0;

var openList = new List<Node>();
var closedList = new List<Node>();

openList.Add(start);

while (openList.Count > 0)
{
    var currentNode = openList.First();
    var currentIndex = 0;

    for (int i = 0; i < openList.Count; i++)
    {
        if (openList.ElementAt(i).F < currentNode.F)
        {
            currentNode = openList.ElementAt(i);
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

        Console.WriteLine($"Part one: {path.Count - 1}");
        break;
    }

    var children = new List<Node>();
    var currentNodeCharacter = input[currentNode.Position.Item2][currentNode.Position.Item1];
    var currentNodeHeight = (byte)(currentNodeCharacter == 'S' ? 'a' : currentNodeCharacter == 'E' ? 'z' : currentNodeCharacter);

    foreach ((int, int) pos in new HashSet<(int, int)>() { (-1, 0), (0, 1), (1, 0), (0, -1) })
    {
        var nodePosition = (currentNode.Position.Item1 + pos.Item1, currentNode.Position.Item2 + pos.Item2);

        if (nodePosition.Item1 > (width - 1) || nodePosition.Item1 < 0 || nodePosition.Item2 > (input.Length - 1) || nodePosition.Item2 < 0)
        {
            continue;
        }

        var character = input[nodePosition.Item2][nodePosition.Item1];
        var height = (byte)(character == 'S' ? 'a' : character == 'E' ? 'z' : character);

        // make sure the node is reachable (not higher than 1 unit)
        if (height < currentNodeHeight || height > currentNodeHeight + 1)
        {
            continue;
        }

        children.Add(new Node(currentNode, nodePosition));
    };

    foreach (Node child in children)
    {
        if (closedList.Select(x => x.Position).Contains(child.Position))
        {
            continue;
        }

        child.G = currentNode.G + 1;
        child.H = (decimal)Math.Sqrt(Math.Pow(child.Position.Item1 - end.Position.Item1, 2) + Math.Pow(child.Position.Item2 - end.Position.Item2, 2));

        if (openList.Where(x => x.Position == child.Position && child.G > x.G).ToList().Count > 0)
        {
            continue;
        }

        openList.Add(child);
    }
}
public class Node
{
    public Node(Node? parent, (int, int) position)
    {
        Parent = parent;
        Position = position;
    }

    public Node? Parent { get; set; }

    public (int, int) Position { get; set; }

    public decimal G { get; set; }

    public decimal H { get; set; }

    public decimal F => G + H;

    public bool Equals(Node other) => this.Position == other.Position;
}