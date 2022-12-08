using System.Text.RegularExpressions;

var input = File.ReadAllLines("./input.txt");
var files = new Dictionary<string, int>();
var currentPosition = "/";

files.Add("/", 0);

foreach (var line in input)
{
    if (line.StartsWith("$"))
    {
        // this is a command
        var match = Regex.Match(line, @"([\S]) ([a-zA-Z]*) ([\S]*)");

        switch (match.Groups[2].Value)
        {
            case "cd":
                // move
                switch (match.Groups[3].Value)
                {
                    case "/":
                        // reset
                        currentPosition = "/";
                        break;
                    case "..":
                        // move up
                        var dirs = GetDirs(currentPosition);
                        currentPosition = "/" + String.Join("/", dirs.Take(dirs.Length - 1));
                        break;
                    default:
                        // dir
                        currentPosition += (currentPosition == "/" ? "" : "/") + match.Groups[3].Value;
                        files.Add(currentPosition, 0);
                        break;
                }

                break;
            default:
                break;
        }
    }
    else
    {
        if (!line.StartsWith("dir"))
        {
            var fileSize = int.Parse(line.Split(" ")[0]);

            var fileDirs = files.Keys;

            files[currentPosition] += fileSize;

            var dirs = GetDirs(currentPosition);

            for (int i = dirs.Length - 1; i >= 0; i--)
            {
                //Console.WriteLine($"Current Position: {currentPosition}, Parents: /{String.Join("/", dirs.Take(i))}, Filesize: {fileSize}");
                files["/" + String.Join("/", dirs.Take(i))] += fileSize;
            }
        }

    }
}

var target = 100000;
var values = files.Select(f => f.Value).Where(v => v <= 100000).OrderByDescending(v => v).ToArray();
var current = values.Where(v => v <= target).Sum();

Console.WriteLine($"Part one: {current}");

var totalSpace = 70000000;
var requiredSpace = 30000000;
var rootTotalSize = files["/"];
var spaceToClear = requiredSpace - (totalSpace - rootTotalSize);
var closest = files.Select(f => f.Value).Where(v => v >= spaceToClear).OrderBy(v => v).FirstOrDefault();

Console.WriteLine($"Part two: {closest}");

static string[] GetDirs(string position)
{
    return position.Split("/", StringSplitOptions.RemoveEmptyEntries);
}

static int Converge(int initial, int target, int[] values)
{
    var current = initial;

    if (current < target && values.Length > 0)
    {
        current = values[0];
        current = Converge(current, target, values.Where(v => v <= target - current).OrderByDescending(v => (target - current - v)).ToArray());
    }

    return current;
}
