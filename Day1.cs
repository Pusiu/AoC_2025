namespace AoC_2025
{
    public class Day1 : Day
    {
        public override string Part1()
        {
            int dialPointer = 50;
            int zeroCount = 0;

            input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(r => r.ReplaceLineEndings()).ToList().ForEach(line =>
            {
                var direction = line.Substring(0, 1);
                var steps = int.Parse(line.Substring(1));
                var org = dialPointer;
                dialPointer = (dialPointer + (direction == "L" ? 100 - steps : steps)) % 100;
                if (dialPointer == 0) zeroCount++;
            });

            return zeroCount.ToString();
        }

        public override string Part2()
        {
            int dialPointer = 50;
            int zeroCount = 0;

            input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(r => r.ReplaceLineEndings()).ToList().ForEach(line =>
            {
                var direction = line.Substring(0, 1);
                var steps = int.Parse(line.Substring(1));
                for (int i = 0; i < steps; i++)
                {
                    dialPointer = (dialPointer + (direction == "L" ? 99 : 1)) % 100;
                    if (dialPointer == 0) zeroCount++;
                }
            });
            return zeroCount.ToString();
        }
    }
}