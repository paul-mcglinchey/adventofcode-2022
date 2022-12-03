using System.Text.RegularExpressions;

var input = File.ReadAllLines("./input.txt");

Console.WriteLine("Part one: " + input
    .Select(x => new { comp1 = x.Substring(0, x.Length / 2), comp2 = x.Substring(x.Length / 2) })
    .Select(x => (byte)x.comp1
        .Where(c => x.comp2.Contains(c))
        .ToArray()[0])
    .Select(x => x < 97 ? x - 38 : x - 96)
    .Sum());

Console.WriteLine("Part two: " + input
    .Select((x, i) => new { Index = i, Value = x })
    .GroupBy(x => x.Index / 3)
    .Select(x => x
        .Select(v => v.Value)
        .ToList())
    .Select(group => group
        .Skip(1)
        .Aggregate(group[0].ToCharArray(), (s, u) => s.Intersect(u).ToArray())
        .FirstOrDefault())
    .Select(x => x < 97 ? x - 38 : x - 96)
    .Sum());