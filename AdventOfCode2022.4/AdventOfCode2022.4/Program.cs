var input = File.ReadAllLines("./input.txt");
var partOneCount = 0;
var partTwoCount = 0;

foreach (var line in input)
{
    var e1 = line.Split(",")[0];
    var e2 = line.Split(",")[1];

    var e1sections = e1.Split("-").Select(x => int.Parse(x)).ToList();
    var e2sections = e2.Split("-").Select(x => int.Parse(x)).ToList();

    if ((e1sections[0] >= e2sections[0] && e1sections[1] <= e2sections[1]) || (e2sections[0] >= e1sections[0] && e2sections[1] <= e1sections[1]))
    {
        partOneCount++;
    }

    if ((e1sections[0] >= e2sections[0] && e1sections[0] <= e2sections[1])
        || (e1sections[1] <= e2sections[1] && e1sections[1] >= e2sections[0])
        || (e2sections[0] >= e1sections[0] && e2sections[0] <= e1sections[1])
        || (e2sections[1] <= e1sections[1] && e2sections[1] >= e1sections[1]))
    {
        partTwoCount++;
    }
}

var output = input
    .Select(x => x.Split(",").Select(x => x.Split("-").Select(x => new { start = x[0], end = x[1]})
    .Select(x 
    .Select(x => new { e1 = x[0], e2 = x[1] })
    ).Select(x => ((x[0].section1 >= x[2].section1 && x[0].section2 <= x[1].section2) || (x[1].section1 >= x[0].section1 && x[1].section2 <= x[0].section2)) ? 1 : 0);

Console.WriteLine($"Part one: {partOneCount}");
Console.WriteLine($"Part two: {partTwoCount}");