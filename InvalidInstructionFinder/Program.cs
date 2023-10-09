using LibChip8;


namespace InvalidInstructionFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var decoder = new InstructionDecoder(null);

            int counter = 0;

            for (ushort i = 0; i < ushort.MaxValue; i++)
            {
                try
                {
                    decoder.DecodeInstruction(i);
                }
                catch
                {
                    counter++;
                }
            }

            Console.WriteLine(counter);
        }
    }
}