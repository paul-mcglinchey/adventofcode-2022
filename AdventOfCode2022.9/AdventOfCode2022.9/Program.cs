var input = File.ReadAllLines("./input.txt");

var directions = new Dictionary<char, Action<Position>>()
{
    { 'U', (p) => p.Y++ }, { 'L', (p) => p.X-- }, { 'R', (p) => p.X++ }, { 'D', (p) => p.Y-- },
};

PartOne();
PartTwo();

void PartOne()
{
    var head = new Position(0, 0);
    var tail = new Position(0, 0);
    var history = new HashSet<(int, int)> { (0, 0) };

    foreach (var line in input)
    {
        var dir = line[0];

        for (int mag = 0; mag < int.Parse(line[2..]); mag++)
        {
            directions[dir].Invoke(head);

            tail = Chase(tail, head);

            history.Add((tail.X, tail.Y));
        }
    }

    Console.WriteLine($"Part one: {tail.History.Distinct().Count()}");
}

void PartTwo()
{
    var head = new Position(0, 0);
    var tail = new List<Position>()
    {
        new Position(0, 0),
        new Position(0, 0),
        new Position(0, 0),
        new Position(0, 0),
        new Position(0, 0),
        new Position(0, 0),
        new Position(0, 0),
        new Position(0, 0),
        new Position(0, 0),
    };

    foreach (var line in input)
    {
        var dir = line[0];

        for (int mag = 0; mag < int.Parse(line[2..]); mag++)
        {
            directions[dir].Invoke(head);

            tail[0] = Chase(tail[0], head);

            for (int i = 1; i < tail.Count; i++)
            {
                tail[i] = Chase(tail[i], tail[i - 1]);
            }
        }
    }

    Console.WriteLine($"Part two: {tail.Last().History.Distinct().Count()}");
}

Position Chase(Position tail, Position head)
{
    var dx = head.X - tail.X;
    var dy = head.Y - tail.Y;

    // Distance in both directions <= 1 then no move required
    if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
    {
        return tail;
    }

    tail.X += Math.Sign(dx);
    tail.Y += Math.Sign(dy);

    tail.History.Add((tail.X, tail.Y));

    return tail;
}

class Position
{
    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public HashSet<(int, int)> History = new HashSet<(int, int)> { (0, 0) };
}