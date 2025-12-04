namespace AoC_2025
{
    public class Day4 : Day
    {
        public override string Part1(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                             .Select(r => r.ReplaceLineEndings())
                             .Select(x => x.ToCharArray())
                             .ToArray();                    
            return lines.SelectMany((row, y) => row.Select((column, x) => lines[y][x] == '.' ? 0 : (GetNeighbourCount(lines, x, y) < 4 ? 1 : 0)))
                        .Where(c => c < 4)
                        .Sum()
                        .ToString();
        }

        static int GetNeighbourCount(char[][] grid, int x, int y)
        {
            int count = 0;
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0) continue;
                    int nx = x + dx;
                    int ny = y + dy;
                    if (nx >= 0 && nx < grid[0].Length && ny >= 0 && ny < grid.Length)
                    {
                        if (grid[ny][nx] == '@') count++;
                    }
                }
            }
            return count;
        }

        public override string Part2(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                             .Select(r => r.ReplaceLineEndings())
                             .Select(x => x.ToCharArray())
                             .ToArray();
            var totalRemoved = 0;
            while (true)
            {
                var rollsToBeRemoved = lines.SelectMany((row, y) => row.Select((column, x) => (x, y)))
                                            .Where(pos => lines[pos.y][pos.x] != '.' && GetNeighbourCount(lines, pos.x, pos.y) < 4);
                if (!rollsToBeRemoved.Any()) break;
                foreach (var (x, y) in rollsToBeRemoved)
                {
                    lines[y][x] = '.';
                    totalRemoved++;
                }
            }
            return totalRemoved.ToString();
        }
    }
}