using (var reader = new StreamReader("./input.txt"))
{
    var line = reader.ReadLine();
    var totalCals = new List<int>();
    var current = 0;

    while (line != null)
    {
        if (line.Trim() == string.Empty)
        {
            totalCals.Add(current);
            current = 0;
        } 
        else
        {
            current += int.Parse(line.Trim());
        }

        line = reader.ReadLine();
    }

    Console.WriteLine($"Total cals of heftiest boi: {totalCals.Max()}");
    Console.WriteLine($"Total cals of top 3 heftiest bois: {totalCals.OrderByDescending(c => c).Take(3).Sum()}");

}