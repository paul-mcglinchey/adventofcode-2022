var input = File.ReadAllLines("./input.txt");
var count = 0;
var scenicScore = 0;

count += ((input.Length + input[0].Length) * 2) - 4;

for (int i = 1; i < input.Length - 1; i++)
{
    for (int j = 1; j < input[i].Length - 1; j++)
    {
        var current = int.Parse(input[i][j].ToString());
        var currentScenicScore = 0;

        var top = input.Select(x => int.Parse(x[j].ToString())).Take(i);
        var left = input[i].Select(x => int.Parse(x.ToString())).Take(j);
        var right = input[i].Select(x => int.Parse(x.ToString())).Skip(j + 1);
        var bottom = input.Select(x => int.Parse(x[j].ToString())).Skip(i + 1);

        if (top.All(x => x < current) || left.All(x => x < current) || right.All(x => x < current) || bottom.All(x => x < current))
        {
            count++;
        }

        var topRange = 0;
        var leftRange = 0;
        var rightRange = 0;
        var bottomRange = 0;

        foreach (var treeHeight in top.Reverse())
        {
            topRange++;

            if (treeHeight >= current)
            {
                break;
            }
        }

        foreach (var treeHeight in left.Reverse())
        {
            leftRange++;

            if (treeHeight >= current)
            {
                break;
            }
        }

        foreach (var treeHeight in right)
        {
            rightRange++;

            if (treeHeight >= current)
            {
                break;
            }
        }

        foreach (var treeHeight in bottom)
        {
            bottomRange++;

            if (treeHeight >= current)
            {
                break;
            }
        }

        currentScenicScore = topRange * leftRange * rightRange * bottomRange;

        if (currentScenicScore > scenicScore)
        {
            scenicScore = currentScenicScore;
        }
    }
}

Console.WriteLine($"Part one: {count}");
Console.WriteLine($"Part two: {scenicScore}");
