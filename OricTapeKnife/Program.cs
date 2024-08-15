using System;

namespace OricTapePenKnife
{
    class Program
    {

        private static string outputFile = "";

        static int Main(string[] args)
        {

            Console.Write("OricTapePenKnife - ");

            if (args.Length < 2)
            {
                Console.WriteLine("Set basic type needs at least 2 arguments: <operation> <inputfile> [outpufFile]");

                Environment.Exit(-1);
            }

            var operation = args[0];
            var inputFile = args[1];
            byte[] buffer = { };
            try
            {
                buffer = System.IO.File.ReadAllBytes(inputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file. '{ex.Message}'");
                return -1;
            }
            switch (operation.ToUpper())
            {
                case "SETBASIC":
                    Warn3Args(args);
                    Console.WriteLine($"Setting type to BASIC on file {outputFile}");
                    buffer[6] = 0;
                    System.IO.File.WriteAllBytes(outputFile, buffer);
                    break;

                case "SETASM":
                    Warn3Args(args);
                    Console.WriteLine($"Setting type to asssembly on file {outputFile}");
                    buffer[6] = 0x80;
                    System.IO.File.WriteAllBytes(outputFile, buffer);
                    break;

                case "NOAUTO":
                    Warn3Args(args);
                    Console.WriteLine($"Setting no auto-run on file {outputFile}");
                    buffer[7] = 0;
                    System.IO.File.WriteAllBytes(outputFile, buffer);
                    break;

                case "BASICAUTO":
                    Warn3Args(args);
                    Console.WriteLine($"Setting BASIC auto-run on file {outputFile}");
                    buffer[7] = 0x80;
                    System.IO.File.WriteAllBytes(outputFile, buffer);
                    break;

                case "ASMAUTO":
                    Warn3Args(args);
                    Console.WriteLine($"Setting assembly auto-run on file {outputFile}");
                    buffer[7] = 0xC7;
                    System.IO.File.WriteAllBytes(outputFile, buffer);
                    break;

                case "DETAILS":
                    Console.WriteLine($"");
                    Console.WriteLine($"Examine details to file {inputFile}");
                    if (buffer[6]==0)
                    {
                        Console.WriteLine($"Type: Basic");
                    } 
                    else if (buffer[6] == 0)
                    {
                        Console.WriteLine($"Type: Assembler");
                    } 
                    else
                    {
                        Console.WriteLine($"Type: Unknown [{buffer[6]}]");
                    }

                    if (buffer[7] == 0)
                    {
                        Console.WriteLine($"Auto-Start: No");
                    }
                    else if (buffer[7] == 0x80)
                    {
                        Console.WriteLine($"Auto-Start: Basic");
                    }
                    else if (buffer[7] == 0xC7)
                    {
                        Console.WriteLine($"Auto-Start: Assembler");
                    }
                    else
                    {
                        Console.WriteLine($"Auto-Start: Unknown");
                    }
                    var fileName = "";
                    var index = 13;
                    while (fileName.Length<17)
                    {
                        fileName += (char)buffer[index];
                        index++;
                        if (buffer[index]==0)
                        {
                            break;
                        }
                    }
                    Console.WriteLine($"Filename: {fileName}");
                    break;

                default:
                    Console.WriteLine($"Operation {operation} unknown");
                    break;
            }
            return 0;
        }

        private static void Warn3Args(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Set basic type needs 3 arguments: <operation> <inputfile> <outpufFile>");
                Environment.Exit(-1);
            }
            outputFile = args[2];
        }
    }
}
