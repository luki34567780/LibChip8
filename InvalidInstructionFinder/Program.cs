using LibChip8;

namespace InvalidInstructionFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var decoder = new InstructionDecoder();

            for (ushort i = 0; i < ushort.MaxValue; i++)
            {
                try
                {
                    decoder.DecodeInstruction(i);
                }
                catch
                {
                    System.Console.WriteLine($"Invalid Instruction: {i}");
                    return;
                }
            }
        }
    }
}