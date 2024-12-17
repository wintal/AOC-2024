using Utilities;

namespace Day17;

class Program
{
    private const string Sample = "sample.txt";
    private const string Input = "input.txt";

    static void Main(string[] args)
    {
        RunPart1(Sample);
        RunPart1(Input);
        /*RunPart2(Sample);*/
        RunPart2(Input);
    }

    struct VmState
    {
        public long A;
        public long B;
        public long C;
    }

    static void RunPart1(string inputFile)
    {
        var start = DateTime.Now;

        var lines = System.IO.File.ReadAllLines(inputFile);


        VmState state = new VmState();
        state.A = long.Parse(lines[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1]);
        state.B = long.Parse(lines[1].Split(":", StringSplitOptions.RemoveEmptyEntries)[1]);
        state.C = long.Parse(lines[2].Split(":", StringSplitOptions.RemoveEmptyEntries)[1]);

        var operations = lines[4].Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
        var opCodes = operations.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        var results = RunOperations(state, opCodes, 0);
        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', results)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static List<long> RunOperations(VmState state, int[] opCodes, int i)
    {
        long ConvertLiteral(int value)
        {
            switch (value)
            {
                case 0: return 0;
                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                case 4:
                    return state.A;
                case 5:
                    return state.B;
                case 6: return state.C;
                default:
                    throw new Exception();
            }
        }

        List<long> output = new();
        var ptr = i;
        while (ptr < opCodes.Length)
        {
            var opcode = opCodes[ptr];
            var literal = opCodes[ptr + 1];
            switch (opcode)
            {
                case 0:
                    // adv
                    state.A = state.A / (1L << (int)ConvertLiteral(literal));
                    ptr += 2;
                    break;
                case 1:
                    state.B = state.B ^ literal;
                    ptr += 2;
                    break;
                case 2:
                    state.B = (ConvertLiteral(literal) & 7);
                    ptr += 2;
                    break;
                case 3:
                    if (state.A != 0)
                    {
                        ptr = literal;
                    }
                    else
                    {
                        ptr += 2;
                    }
                    break;
                case 4:
                    state.B = state.B ^ state.C;
                    ptr += 2;
                    break;
                case 5:
                    output.Add(ConvertLiteral(literal) & 7);
                    ptr += 2;
                    break;
                case 6:
                    state.B = state.A / (1 << (int)ConvertLiteral(literal));
                    ptr += 2;
                    break;
                case 7:
                    state.C = state.A / (1 << (int)ConvertLiteral(literal));
                    ptr += 2;
                    break;
            }
        }

        return output;
    }

    static void RunPart2(string inputFile)
    { 
        var start = DateTime.Now;

        var lines = System.IO.File.ReadAllLines(inputFile);

        VmState state = new VmState();
        state.A = long.Parse(lines[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1]);
        state.B = long.Parse(lines[1].Split(":", StringSplitOptions.RemoveEmptyEntries)[1]);
        state.C = long.Parse(lines[2].Split(":", StringSplitOptions.RemoveEmptyEntries)[1]);

        var operations = lines[4].Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
        var opCodes = operations.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
 
       long FindSubset(int[] theseOpcodes, long baseNumber)
       {
           int index = theseOpcodes.Length - 1;
           for (int i = 0; i <= 7; i++)
           {
               var newState = state;
               newState.A = (baseNumber << 3) + i;
               var thisResult = RunOperations(newState, opCodes, 0);
               if (thisResult.First() == theseOpcodes[index])
               {
                   // try to find solutions from here
                   if (theseOpcodes.Length > 1)
                   {
                       var subsetResult = FindSubset(theseOpcodes[..index], (baseNumber << 3) + i);
                       if (subsetResult > 0)
                       {
                           return subsetResult;
                       }
                   }
                   else
                   {
                       return (baseNumber << 3) + i;
                   }
               }
           }
           return -1;
       }

       long number = 0;
       number = FindSubset(opCodes, 0);

        System.Console.WriteLine(
            $"Result {inputFile} is {number} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
}