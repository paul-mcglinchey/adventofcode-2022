var input = File.ReadAllLines("./input.txt");

Console.WriteLine(input
    .Select(x => x
        .Split(",")
        .Select(x => x.Split("-"))
        .Select(x => new { start = int.Parse(x[0]), end = int.Parse(x[1]) })
        .ToList())
    .Select(x => new { e1 = x[0], e2 = x[1] })
    .Select(x => new
    {
        partOne = ((x.e1.start >= x.e2.start && x.e1.end <= x.e2.end) || (x.e2.start >= x.e1.start && x.e2.end <= x.e1.end)) ? 1 : 0,
        partTwo = (x.e1.start >= x.e2.start && x.e1.start <= x.e2.end) || (x.e1.end <= x.e2.end && x.e1.end >= x.e2.start) || (x.e2.start >= x.e1.start && x.e2.start <= x.e1.end || (x.e2.end <= x.e1.end && x.e2.end >= x.e1.end)) ? 1 : 0,
    })
    .GroupBy(r => 1)
    .Select(g => new
    {
        partOneSum = g.Sum(x => x.partOne),
        partTwoSum = g.Sum(x => x.partTwo),
    })
    .FirstOrDefault());