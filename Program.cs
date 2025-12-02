using System.Reflection;
using System.Text.RegularExpressions;
namespace AoC_2025
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(Day).IsAssignableFrom(x) && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as Day)
                .OrderBy(x => int.Parse(DayRegex().Match(x?.ToString() ?? "").Value))
                .ToList().LastOrDefault()?.Run();
        }

        [GeneratedRegex(@"\d+$")]
        private static partial Regex DayRegex();
    }
}