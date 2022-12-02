var input = File.ReadAllText("./input.txt");
var lines = input.Split("\n");

Console.WriteLine("Part one: " +
        input.Split("\n")
        .Select(x => x.Trim())
        .Select(x => new { theirs = x[0].ToString(), yours = x[2].ToString() })
        .Select(x => (x.yours == "X" 
            ? 1 
            : x.yours == "Y" 
                ? 2 
                : 3) +
            (x.theirs == "A"
                ? x.yours == "X"
                    ? 3
                    : x.yours == "Y"
                        ? 6
                        : 0
            : x.theirs == "B"
                ? x.yours == "X"
                    ? 0
                    : x.yours == "Y"
                        ? 3
                        : 6
                : x.yours == "X"
                    ? 6
                    : x.yours == "Y"
                        ? 0
                        : 3))
        .Sum());

Console.WriteLine("Part two: " +
    input.Split("\n")
    .Select(x => x.Trim())
    .Select(x => new { theirs = x[0].ToString(), outcome = x[2].ToString() })
    .Select(x => (
        x.outcome == "X" 
            ? x.theirs == "A"
                ? 3
                : x.theirs == "B"
                    ? 1
                    : 2
            : x.outcome == "Y"
                ? x.theirs == "A"
                    ? 4
                    : x.theirs == "B"
                        ? 5
                        : 6
                : x.theirs == "A"
                    ? 8
                    : x.theirs == "B"
                        ? 9
                        : 7))
    .Sum());