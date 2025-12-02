using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;

namespace AoC_2025
{
    public abstract class Day
    {
        protected string input="";

        public virtual void Run()
        {
            Console.WriteLine($"Running {this}");
            var stopwatch = Stopwatch.StartNew();
            var input = GetInput(MethodHasTest(nameof(Part1))).GetAwaiter().GetResult();
            Console.WriteLine($"Part 1: {Part1(input)}\nin {stopwatch.Elapsed}");
            stopwatch.Restart();
            input = GetInput(MethodHasTest(nameof(Part2))).GetAwaiter().GetResult();
            Console.WriteLine($"Part 2: {Part2(input)}\nin {stopwatch.Elapsed}");
        }

         bool MethodHasTest(string methodName)
        {
            var mi = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (mi == null) return false;
            return mi.GetCustomAttributes(true)
                     .Any(a => string.Equals(a.GetType().Name, "TestAttribute", System.StringComparison.OrdinalIgnoreCase)
                               || string.Equals(a.GetType().Name, "Test", System.StringComparison.OrdinalIgnoreCase));
        }

        public async Task<string> GetInput(bool isTest = false)
        {
            var baseDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var inputDirectory = baseDirectory + @"\Inputs\";
            if (!Directory.Exists(inputDirectory))
                Directory.CreateDirectory(inputDirectory);

            var match = Regex.Match(GetType().ToString(), @"\d+$");
            int dayNumber = int.Parse(match.Value);


            var filename = $"day{dayNumber}{(isTest ? "_test" : "")}.txt";
            if (isTest) Console.WriteLine("Reading test file");
            if (File.Exists(inputDirectory + filename))
            {
                input = File.ReadAllText(inputDirectory + filename);
            }
            else
            {
                var cookiePath = baseDirectory + @"\cookie.txt";
                var cookie = "";
                if (File.Exists(cookiePath))
                {
                    cookie = File.ReadAllText(cookiePath);
                }
                else
                {
                    Console.WriteLine("Session cookie does not exist, please provie a session cookie: ");
                    cookie = Console.ReadLine();
                    File.WriteAllText(cookiePath, cookie);
                }
                var url = $"https://adventofcode.com/2025/day/{dayNumber}/input";
                var client = new HttpClient();
                try
                {
                    Console.WriteLine("Fetching input from AoC website");
                    var resp = client.Send(new HttpRequestMessage(HttpMethod.Get, url)
                    {
                        Headers = { { "Cookie", cookie } }
                    });
                    input = await resp.Content.ReadAsStringAsync();
                    File.WriteAllText(inputDirectory + filename, input);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error getting input: {e.Message}");
                }
            }

            return input.Replace("\r", "").Trim();
        }

        public virtual string Part1(string input)
        {
            return "";
        }

        public virtual string Part2(string input)
        {
            return "";
        }
    }
}