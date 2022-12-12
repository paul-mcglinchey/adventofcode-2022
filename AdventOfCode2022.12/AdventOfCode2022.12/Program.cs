var input = File.ReadAllLines("./input.txt");

var visited = new HashSet<(int, int)>();
(int, int)? end = null;
(int, int)? start = null;

for (int i = 0; i < input.Length; i++)
{
    var startIndex = input[i].IndexOf('S');
    var endIndex = input[i].IndexOf('E');

    if (startIndex != -1)
    {
        start = (i, startIndex);
    }

    if (endIndex != -1)
    {
        visited.Add((i, endIndex));
        end = (i, endIndex);
    };
}

while (!visited.Contains(start.Value))
{
    var position = visited.Last();

    // capture all the surrounding moves and their respective heights

    // eliminate movements

    // weight the available moves if there's multiple

    // add to the visited set

    // loop will continue until we converge on the start position
}

