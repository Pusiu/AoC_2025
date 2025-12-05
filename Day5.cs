namespace AoC_2025
{
    public partial class Day5 : Day
    {
        public override string Part1(string input)
        {
            var sections = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var ranges = sections[0]
                .Split("\n")
                .Select(line =>
                {
                    var s = line.Split('-');
                    return (Low: long.Parse(s[0]), High: long.Parse(s[1]));
                })
                .ToList();
            
            var ingredients = sections[1]
                .Split("\n")
                .Select(long.Parse)
                .ToList();

            return ingredients.Count(i => ranges.Any(r => i >= r.Low && i <= r.High)).ToString();
        }

        public override string Part2(string input)
        {
            List<(long low, long high)> ranges = [];
            foreach (var r in 
            input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)[0]
            .Split("\n")
            .Select(line =>
                {
                    var s = line.Split('-');
                    return (Low: long.Parse(s[0]), High: long.Parse(s[1]));
                })
            .OrderBy(r => r.Low))
            {
                var insideRange = ranges.FirstOrDefault(existing => r.Low <= existing.high && r.High >= existing.low);
                if (insideRange == default)
                {
                    ranges.Add(r);
                }
                else
                {
                    ranges.Remove(insideRange);
                    ranges.Add((Math.Min(insideRange.low, r.Low), Math.Max(insideRange.high, r.High)));
                }
            }
            return ranges.Sum(r => r.high - r.low + 1).ToString();
        }
    }
}