var input = File.ReadAllLines("./input.txt");

Console.WriteLine(String.Join("\n", Enumerable
    .Range(1, 2)
    .Select(x => new
    {
        stacks = input
            .Take(input.ToList().FindIndex(x => x == string.Empty))
            .Reverse()
            .Where(l => l.Contains("["))
            .Select(l => l
                .Replace("[", "")
                .Replace("]", "")
                .Replace("    ", " .")
                .Replace(" ", "")
                .Trim()
                .Select((x, i) => new { Value = x, Index = i }))
            .SelectMany(x => x)
            .GroupBy(x => x.Index)
            .Select(x => x.Where(x => x.Value != '.').ToList())
            .ToDictionary(x => x.First().Index, x => new Stack<char>(x.Select(x => x.Value))),
        commands = input
            .Skip(input.ToList().FindIndex(x => x == string.Empty) + 1)
            .Select(l => l.Trim())
            .Select(l => new
            {
                n = int.Parse(l.Split("move ")[1].Split(" from")[0]),
                from = int.Parse(l.Split("from ")[1].Split(" to")[0]) - 1,
                to = int.Parse(l.Split(" to ")[1]) - 1,
            }),
    })
    .Select((p, i) =>
    {
        if (i == 0)
        {
            foreach (var c in p.commands)
            {
                for (int j = 0; j < c.n; j++)
                {
                    p.stacks[c.to].Push(p.stacks[c.from].Pop());
                }
            }
        }
        else
        {
            foreach (var c in p.commands)
            {
                var crates = new Stack<char>();

                for (int j = 0; j < c.n; j++)
                {
                    crates.Push(p.stacks[c.from].Pop());
                }

                for (int j = 0; j < c.n; j++)
                {
                    p.stacks[c.to].Push(crates.Pop());
                }
            }
        }

        return String.Join("", p.stacks.Select(s => s.Value.Peek()));
    })));