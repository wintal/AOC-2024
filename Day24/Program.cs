using Utilities;

namespace Day24;

class Program
{
    private const string Sample = "sample.txt";
    private const string Input = "input.txt";

    static void Main(string[] args)
    {
        RunPart1(Sample);
        RunPart1(Input);
        //     RunPart2(Sample);
        RunPart2(Input);
    }

    static void RunPart1(string inputFile)
    {
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        int lineNum = 0;
        Dictionary<string, int> state = new();
        while (lines[lineNum] != string.Empty)
        {
            var parts = lines[lineNum]
                .Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            state[parts[0]] = int.Parse(parts[1]);
            lineNum++;
        }

        lineNum++;
        List<Instruction> instructions = new();
        while (lineNum < lines.Length)
        {
            var parts = lines[lineNum]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            instructions.Add(new Instruction()
            {
                Dest = parts[4],
                Left = parts[0],
                Right = parts[2],
                Op = parts[1],
            });
            lineNum++;
        }


        HashSet<string> setState = new HashSet<string>(state.Keys);

        HashSet<string> availableState = new HashSet<string>(state.Keys);
        var runnableInstructions = instructions.Where(s =>
            setState.Contains(s.Left) && setState.Contains(s.Right) && !setState.Contains(s.Dest)).ToList();
        do
        {
            foreach (var instruction in runnableInstructions)
            {
                var left = state[instruction.Left];
                var right = state[instruction.Right];
                int thisResult = 0;
                switch (instruction.Op)
                {
                    case "XOR":
                        thisResult = (left == right) ? 0 : 1;
                        break;
                    case "AND":
                        thisResult = ((left & right) == 1) ? 1 : 0;
                        break;
                    case "OR":
                        thisResult = ((left | right) == 1) ? 1 : 0;
                        break;
                }

                state[instruction.Dest] = thisResult;
                setState.Add(instruction.Dest);
            }


            runnableInstructions = instructions.Where(s =>
                setState.Contains(s.Left) && setState.Contains(s.Right) && !setState.Contains(s.Dest)).ToList();
        } while (runnableInstructions.Any());

        var outputStateKeys = setState.Where(s => s.StartsWith("z")).ToList();
        outputStateKeys.Sort((a, b) => b.CompareTo(a));
        foreach (var key in outputStateKeys)
        {
            result = result * 2 + state[key];
        }

        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        int lineNum = 0;
        Dictionary<string, int> state = new();
        while (lines[lineNum] != string.Empty)
        {
            var parts = lines[lineNum]
                .Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            state[parts[0]] = int.Parse(parts[1]);
            lineNum++;
        }

        lineNum++;
        List<Instruction> instructions = new();
        while (lineNum < lines.Length)
        {
            var parts = lines[lineNum]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            instructions.Add(new Instruction()
            {
                Dest = parts[4],
                Left = parts[0],
                Right = parts[2],
                Op = parts[1],
            });
            lineNum++;
        }

        string lastCarry = string.Empty;
        
        // NOTE: this doesn't solve it, but it does find places where the instructions are incorrect. 
        // The error is then fixed by inspection (by comparing the full-adder against a correct added implementation
        //  fixed in the source, then run again.
        for (int i = 0; i < 44; i++)
        {
            string fmt = "00.##";
            string left = "x" + i.ToString(fmt);
            string right = "y" + i.ToString(fmt);
            string output = "z" + i.ToString(fmt);

            // correct adder is 
            // left xor right => ab
            // ab xor carry => output
            // a and b => cin1
            // ab and carry => cin2
            // cin1 or cin2 => carry

            var inst = FindInstruction(instructions, left, right, "XOR");
            string ab = inst.Dest;

            inst = FindInstruction(instructions, left, right, "AND");
            string cin1 = inst.Dest;


            if (i == 0)
            {
                lastCarry = cin1;
            }
            else
            {
                inst = FindInstruction(instructions, ab, lastCarry, "XOR");
                if (inst == null)
                {
                    // ab or lastCarry is wrong
                }

                string outputReg = inst.Dest;


                inst = FindInstruction(instructions, ab, lastCarry, "AND");
                if (inst == null)
                {
                }

                string cin2 = inst.Dest;


                inst = FindInstruction(instructions, cin1, cin2, "OR");
                if (inst == null)
                {
                }

                lastCarry = inst.Dest;
            }
        }


        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static Instruction FindInstruction(List<Instruction> instructions, string left, string right, string op)
    {
        return instructions.Where(inst =>
        {
            return inst.Op == op &&
                   ((inst.Left == left && inst.Right == right) || (inst.Left == right && inst.Right == left));
        }).FirstOrDefault();
    }
}

internal class Instruction
{
    public string Left;
    public string Right;
    public string Op;
    public string Dest;
}