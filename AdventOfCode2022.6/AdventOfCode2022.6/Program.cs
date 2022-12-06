var input = File.ReadAllText("./input.txt");

var accumulator = "";

for (int i = 0; i < input.Length; i++)
{
    accumulator = accumulator.Contains(input[i]) ? accumulator.Substring(accumulator.ToList().FindIndex(x => x == input[i]) + 1) + input[i] : accumulator + input[i];

    if (accumulator.Length == 4)
    {
        Console.Write($"Part one: {accumulator} {i + 1}");
        break;
    }
}

accumulator = "";

for (int i = 0; i < input.Length; i++)
{
    accumulator = accumulator.Contains(input[i]) ? accumulator.Substring(accumulator.ToList().FindIndex(x => x == input[i]) + 1) + input[i] : accumulator + input[i];

    if (accumulator.Length == 14)
    {
        Console.Write($"Part two: {i + 1}");
        break;
    }
}