namespace AoC_2025
{
    public class Day3 : Day
    {
        public override string Part1(string input)
        {
            return input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(r => r.ReplaceLineEndings()).Sum(line =>
            {
                return Solve(line,0, 2, []);
            }).ToString();
        }

        static List<(int digit, int index)> GetDigitsOrdered(string line)
        {
            return [.. line.Select((c, index) => (digit: int.Parse(c.ToString()), index))
                .OrderByDescending(x => x.digit)
                .ThenBy(x => x.index)];
        }

        static long Solve(string input, int startIndex, int count, Dictionary<(int, int), long> memo)
        {
            if (memo.ContainsKey((startIndex, count)))
                return memo[(startIndex, count)];

            var orderedDigits = GetDigitsOrdered(input[startIndex..]).Select(x => (x.digit, index: x.index + startIndex)).ToList();
            
            if (count == 1)
            {
                long result = orderedDigits.FirstOrDefault().digit;
                return memo[(startIndex, count)] = result;
            }

            long biggest = 0;

            foreach (var (digit, index) in orderedDigits)
            {
                if (index + count > input.Length) continue;
                long subNumber = Solve(input, index + 1, count - 1, memo);
                long candidateNumber = digit * (long)Math.Pow(10, count - 1) + subNumber;

                if (candidateNumber > biggest)
                {
                    biggest = candidateNumber;
                }
            }

            memo[(startIndex, count)] = biggest;
            return biggest;
        }

        public override string Part2(string input)
        {
            return input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(r => r.ReplaceLineEndings()).Sum(line =>
            {
                 return Solve(line, 0, 12, []);
            }).ToString();
        }
    }
}