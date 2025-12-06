using System.ComponentModel;
using System.Text;

namespace AoC_2025
{
    public class Day6 : Day
    {
        // [Test]
        public override string Part1(string input)
        {
            var inp = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
            var grandTotal = 0L;
            for (int problemIndex=0; problemIndex<inp[0].Length; problemIndex++)
            {
                var multiply = inp[^1][problemIndex] == "*";
                var sum=0L;
                for (int problemInput=0; problemInput < inp.Length - 1; problemInput++)
                {
                    var val = long.Parse(inp[problemInput][problemIndex]);
                    if (sum == 0) sum = val;
                    else
                    {
                        sum = multiply ? sum * val : sum + val;
                    }
                }
                grandTotal += sum;
            }
            return grandTotal.ToString();
        }

        public override string Part2(string input)
        {
            var inp = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToArray()).ToArray();
            var grandTotal = 0L;
            var currentOperand = ' ';
            StringBuilder sb = new();
            var currentProblemValue=0L;
            for (int i=0; i < inp[0].Length; i++)
            {
                sb.Clear();
                bool hasEncounteredCharacter=false;
                for (int j=0; j < inp.Length; j++)
                {
                    char c = inp[j][i];
                    if (c != ' ')
                    {
                        hasEncounteredCharacter=true;
                        if (c == '*' || c == '+')
                            currentOperand = c;
                        else
                            sb.Append(c);
                    }
                }
                if (hasEncounteredCharacter)
                {
                    long num = long.Parse(sb.ToString());
                    var mul = currentOperand == '*';
                    if (currentProblemValue == 0) currentProblemValue = num;
                    else currentProblemValue = mul ? currentProblemValue*num : currentProblemValue + num;
                }
                if (!hasEncounteredCharacter || i==inp[0].Length-1)
                {
                    currentOperand = ' ';
                    grandTotal += currentProblemValue;
                    currentProblemValue=0L;
                }
            }
            return grandTotal.ToString();
        }
    }
}