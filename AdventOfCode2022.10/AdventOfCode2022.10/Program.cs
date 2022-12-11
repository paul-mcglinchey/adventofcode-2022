var input = File.ReadAllLines("./input.txt");

var register = new List<int>() { 1 };
var cycle = 0;
var total = 0;
var crtRow = "";
var row = 0;

foreach (var line in input)
{
    switch (line.Split(" ")[0])
    {
        case "addx":
            var value = int.Parse(line.Split(" ")[1]);
            UpdateTotal();
            ScanForward();
            UpdateTotal();
            ScanForward();
            register.Add(value);
            break;
        case "noop":
            ScanForward();
            UpdateTotal();
            break;
        default:
            throw new NotImplementedException();
    }
}

Console.WriteLine($"Part one: {total}");

void UpdateTotal()
{
    if (cycle == 20 || ((cycle - 20) % 40 == 0))
    {
        var currentSum = register.Sum();
        total += currentSum * cycle;
    }
}

void ScanForward()
{
    if (cycle != 0 && cycle % 40 == 0)
    {
        Console.WriteLine(crtRow);
        crtRow = "";

        if (row == 6)
        {
            row = 0;
            Console.WriteLine("\n");
        }
        else
        {
            row++;
        }
    }

    var spriteCenterPosition = register.Sum();
    var spriteRange = new HashSet<int> { spriteCenterPosition - 1, spriteCenterPosition, spriteCenterPosition + 1 };

    crtRow += spriteRange.Contains(cycle - (row * 40)) ? '#' : '.';

    cycle++;
}