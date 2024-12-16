using Utilities;

namespace Day1;

class Program
{
    private const string Sample = "sample.txt";
    private const string Input = "input.txt";

    static void Main(string[] args)
    {
        RunPart1(Sample);
        RunPart1(Input);
        RunPart2(Sample);
        RunPart2(Input);
    }

    struct Problem
    {
        public Vector A;
        public Vector B;
        public Vector Result;
    }

    static void RunPart1(string inputFile)
    {
        var start = DateTime.Now;
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        List<Problem> problems = new List<Problem>();
        for (int i = 0; i + 3 < lines.Length; i += 4)
        {
            Problem problem = new Problem();
            {
                var part = lines[i].Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var part2 = part[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                problem.A = new Vector(int.Parse(part2[0][2..]), int.Parse(part2[1][2..]));
            }
            {
                var part = lines[i + 1].Split(":",
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var part2 = part[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                problem.B = new Vector(int.Parse(part2[0][2..]), int.Parse(part2[1][2..]));
            }
            {
                var part = lines[i + 2].Split(":",
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var part2 = part[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                problem.Result = new Vector(int.Parse(part2[0][2..]), int.Parse(part2[1][2..]));
            }

            problems.Add(problem);
        }

        foreach (var problem in problems)
        {
            long aMultiple = 0;
            long bMultiple = 0;
            // get a start
            var aMax = Math.Max(problem.Result.X / problem.A.X, problem.Result.Y / problem.A.Y);
            long bestResult = int.MaxValue;
            for (int aM = 0; aM < aMax; aM++)
            {
                var x = aM * problem.A.X;
                var y = aM * problem.A.Y;

                var bM1 = (problem.Result.X - x) / problem.B.X;
                var bM2 = (problem.Result.Y - y) / problem.B.Y;
                if ((bM1 == bM2) && (problem.Result.X == aM * problem.A.X + bM1 * problem.B.X) &&
                    (problem.Result.Y == aM * problem.A.Y + bM1 * problem.B.Y))
                {
                    bestResult = Math.Min(bestResult, aM * 3 + bM1);
                }
            }
            if (bestResult != int.MaxValue)
            {
                Console.WriteLine($"Answer is {bestResult}");
                result += bestResult;
            }
            
           
        }


        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    public static List<int> GetPrimes(long number)
    {
        var primes = new List<int>();

        for (int div = 2; div <= number; div++)
            while (number % div == 0)
            {
                primes.Add(div);
                number = number / div;
            }

        return primes;
    }
    
  

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);
        List<Problem> problems = new List<Problem>();
        long offset = 10000000000000;
        for (int i = 0; i + 2 < lines.Length; i += 4)
        {
            Problem problem = new Problem();
            {
                var part = lines[i].Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var part2 = part[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                problem.A = new Vector(int.Parse(part2[0][2..]), int.Parse(part2[1][2..]));
            }
            {
                var part = lines[i + 1].Split(":",
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var part2 = part[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                problem.B = new Vector(int.Parse(part2[0][2..]), int.Parse(part2[1][2..]));
            }
            {
                var part = lines[i + 2].Split(":",
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var part2 = part[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                problem.Result = new Vector(int.Parse(part2[0][2..]) + offset, int.Parse(part2[1][2..]) + offset);
            }

            problems.Add(problem);
        }

        var obj = new object();
        foreach (var problem in problems)
        { 
            
            long b = (problem.Result.X * problem.A.Y - problem.Result.Y * problem.A.X);
            long bdiv = (problem.B.X * problem.A.Y - problem.A.X * problem.B.Y);
            if (bdiv == 0)
            {
                continue;
            }

            b = b / bdiv;

            long a = (problem.Result.Y * problem.B.X - problem.Result.X * problem.B.Y);
            long adiv = (problem.B.X * problem.A.Y - problem.A.X * problem.B.Y);
            if (adiv == 0)
            {
                continue;
            }

            a = a / bdiv;
            if (a > 0 && b > 0 
                      && (problem.Result.X ==  problem.A.X * a + problem.B.X * b) 
                      && (problem.Result.Y == problem.A.Y * a + problem.B.Y * b))
            {
                long thisResult = a * 3 + b;
                Console.WriteLine($"Answer is {thisResult}");
                lock (obj)
                {
                    result +=thisResult;
                }
            }
            else
            {
                Console.WriteLine($"No result");
            }
            var solverResult = Day13Solver.FindWinnersPart2(problem.A.X, problem.A.Y, problem.B.X, problem.B.Y, problem.Result.X, problem.Result.Y);
            if (solverResult.HasValue)
            {
                Console.WriteLine($"Solver Answer is {solverResult.Value.X * 3 + solverResult.Value.Y}");
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
}