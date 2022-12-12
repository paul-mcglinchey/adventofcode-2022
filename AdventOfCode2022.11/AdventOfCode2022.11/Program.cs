var monkeys = new List<Monkey>();

Setup();
Play(null);
Console.WriteLine($"Monkey business level (Part one): {monkeys.OrderByDescending(x => x.InspectionCount).Take(2).Select(x => x.InspectionCount).Aggregate((current, final) => current * final)}");

Setup();
var moduloFactor = monkeys.Aggregate(1, (current, monkey) => current * monkey.Test);
Play(moduloFactor);
Console.WriteLine($"Monkey business level (Part two): {monkeys.OrderByDescending(x => x.InspectionCount).Take(2).Select(x => x.InspectionCount).Aggregate((current, final) => current * final)}");

void Setup()
{
    var input = File.ReadAllLines("./input.txt");
    Monkey monkey = new Monkey();

    foreach (var line in input)
    {
        if (line.StartsWith("Monkey"))
        {
            monkey.Id = int.Parse(line.Split(" ")[1].Split(":")[0]);
        }

        if (line.Trim().StartsWith("Starting items:"))
        {
            monkey.Items = new Queue<long>(line.Split(":")[1].Split(",").Select(x => long.Parse(x.Trim())));
        }

        if (line.Trim().StartsWith("Operation:"))
        {
            var sign = line.Split("=")[1].Trim().Split(" ")[1];
            var operand = line.Split(" ").Last();

            if (int.TryParse(operand, out var op))
            {
                monkey.Operation = (long old) => sign == "*" ? old * op : old + op;
            }
            else
            {
                monkey.Operation = (long old) => sign == "*" ? old * old : old + old;
            }
        }

        if (line.Trim().StartsWith("Test:"))
        {
            monkey.Test = int.Parse(line.Split(" ").Last());
        }

        if (line.Trim().StartsWith("If"))
        {
            var dest = int.Parse(line.Split(" ").Last());

            if (line.Contains("true"))
            {
                monkey.TrueDest = dest;
            }
            else
            {
                monkey.FalseDest = dest;

                monkeys.Add(monkey);
                monkey = new Monkey();
            }
        }

        if (line == string.Empty)
        {
            continue;
        }
    }
}

void Play(int? moduloFactor)
{
    for (int i = 0; i < (moduloFactor == null ? 20 : 10000); i++)
    {
        // new round
        foreach (var m in monkeys)
        {
            // new turn
            while (m.Items.Count > 0)
            {
                var item = m.Items.Dequeue();
                m.InspectionCount++;

                item = m.Operation(item);

                if (moduloFactor == null)
                {
                    item /= 3;
                }
                else
                {
                    item %= moduloFactor.Value;
                }

                if (item % m.Test == 0)
                {
                    monkeys[m.TrueDest].Items.Enqueue(item);
                }
                else
                {
                    monkeys[m.FalseDest].Items.Enqueue(item);
                }
            }
        }
    }
}

class Monkey
{
    public int Id { get; set; }

    public Queue<long> Items { get; set; } = new Queue<long>();

    public Func<long, long> Operation { get; set; } = i => i;

    public int Test { get; set; }

    public int TrueDest { get; set; }

    public int FalseDest { get; set; }

    public long InspectionCount { get; set; } = 0;

    public string ToString()
    {
        return $"Monkey {this.Id} has items {String.Join(", ", this.Items)} and an inspection count of {this.InspectionCount}";
    }
}
