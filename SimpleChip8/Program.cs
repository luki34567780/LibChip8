using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Common;
namespace SimpleChip8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var proc in Process.GetProcesses())
            {
                try
                {
                    proc.ProcessorAffinity = unchecked((nint)0xFFFFFFFFFFFFFFFFL);
                }
                catch {}
            }
            ;


            int counter = 0;
            var cpu = new LibChip8.CPU();
            cpu.LoadImage(File.ReadAllBytes("test_opcode.ch8"));

            var sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed.TotalSeconds < 20) ;
            Console.WriteLine("Finished Waiting");

            sw.Reset();
                

            try
            {
                while (true)
                {
                    cpu.RunTick();
                    if (counter++ > 1000000000)
                    {
                        Console.WriteLine("Breaking because of Iteration limit");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine($"Executed {counter} instructions");
            }

            sw.Stop();

            Console.WriteLine($"Ran {sw.Elapsed.TotalMilliseconds} ms total for {counter - 2} instructions, {sw.Elapsed.TotalNanoseconds / (double)counter}ns per instruction");

            var bm = new Bitmap(64, 32);

            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    bm.SetPixel(i, j, cpu.Screen.Pixels[i, j] == 0 ? Color.Black : Color.Wheat);
                }
            }

            bm.Save("output.png");
        }
    }
}