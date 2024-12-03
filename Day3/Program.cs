using System.Text.RegularExpressions;

namespace Day3;

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


    static void RunPart1(string inputFile)
    {
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        int result = 0;

        bool doIt = true;
        foreach (var line in lines)
        {
            for (int i = 0; i < line.Length - 8; i++)
            {
                if (line[i..(i + 4)] == "mul(")
                {
                    int offset = 4;
                    int firstNumber = 0;
                    for (int num = 0; num < 3; num++)
                    {
                        if (char.IsNumber(line[i + offset]))
                        {
                            firstNumber = firstNumber * 10 + (line[i + offset]) - '0';
                            offset++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (line[i + offset] != ',' || firstNumber == 0)
                    {
                        continue;
                    }

                    offset++;
                    int secondNumber = 0;
                    for (int num = 0; num < 3; num++)
                    {
                        if (char.IsNumber(line[i + offset]))
                        {
                            secondNumber = secondNumber * 10 + (line[i + offset]) - '0';

                            offset++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (line[i + offset] != ')' || secondNumber == 0)
                    {
                        continue;
                    }

                    result += firstNumber * secondNumber;
                    i += offset;
                }
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }

    static void RunPart2(string inputFile)
    {
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        int result = 0;

        bool doIt = true;
        foreach (var line in lines)
        {
            var matchDO = line[1..3] == "do()";


            for (int i = 0; i < line.Length - 7; i++)
            {
                if (line[i..].StartsWith("do()"))
                {
                    doIt = true;
                }
                else if (line[i..].StartsWith("don't()"))
                {
                    doIt = false;
                }
                else if (line[i..].StartsWith("mul("))
                {
                    int offset = 4;
                    int firstNumber = 0;
                    for (int num = 0; num < 3; num++)
                    {
                        if (char.IsNumber(line[i + offset]))
                        {
                            firstNumber = firstNumber * 10 + (line[i + offset]) - '0';
                            offset++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (line[i + offset] != ',' || firstNumber == 0)
                    {
                        continue;
                    }

                    offset++;
                    int secondNumber = 0;
                    for (int num = 0; num < 3; num++)
                    {
                        if (char.IsNumber(line[i + offset]))
                        {
                            secondNumber = secondNumber * 10 + (line[i + offset]) - '0';

                            offset++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (line[i + offset] != ')' || secondNumber == 0)
                    {
                        continue;
                    }

                    if (doIt)
                    {
                        result += firstNumber * secondNumber;
                    }

                    i += offset;
                }
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }
}