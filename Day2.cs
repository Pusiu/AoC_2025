using System.Linq;

namespace AoC_2025
{
    public class Day2 : Day
    {
        public override string Part1(string input) => Solve(input, false).ToString();

        public static long Solve(string input, bool part2) => 
        input.Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split('-'))
            .Select(x => (long.Parse(x[0]), long.Parse(x[1])))
            .Sum(r =>
            {
                long sumForRange = 0;
                var sb = new System.Text.StringBuilder(32);
                for (long currentId = r.Item1; currentId <= r.Item2; currentId++)
                {
                    sb.Clear();
                    sb.Append(currentId);
                    int len = sb.Length;
                    int maxWindow = len / 2;
                    bool invalidFound = false;

                    for (int slidingWindow = 1; slidingWindow <= maxWindow; slidingWindow++)
                    {
                        if (len % slidingWindow != 0) continue; // must divide evenly to fully cover the string
                        int repeatCount = len / slidingWindow;
                        if (repeatCount < 2) continue;

                        bool allMatch = true;
                        for (int pos = 0; pos < slidingWindow; pos++)
                        {
                            char c = sb[pos];
                            for (int rep = 1; rep < repeatCount; rep++)
                            {
                                if (sb[rep * slidingWindow + pos] != c)
                                {
                                    allMatch = false;
                                    break;
                                }
                            }
                        }
                        if (allMatch && (part2 ? repeatCount >= 2 : repeatCount == 2))
                        {
                            sumForRange += currentId;
                            invalidFound = true;
                            break;
                        }
                    }
                    if (invalidFound) continue;
                }
                return sumForRange;
            });

        override public string Part2(string input) => Solve(input, true).ToString();
    }
}