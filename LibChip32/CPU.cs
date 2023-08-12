using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    public class CPU
    {
        public CPU()
        {
        }

        public InstructionDecoder Decoder { get; } = new();

        public Registers Regs { get; } = new();
        public Screen Screen { get; } = new();
        public Memory Memory { get; } = new();

        public void LoadImage(byte[] image)
        {
            image.CopyTo(Memory.AsSpan(0x200));
        }

        public void RunTick()
        {
            var instructionBinary = (ushort)(Memory[Regs.PC] << 8 | Memory[Regs.PC + 1]);
            var instructionImplementation = Decoder.DecodeInstruction(instructionBinary);
            //Console.WriteLine($"Executing instruction {instructionImplementation.ToString().Split(".").Last()} (Hex {instructionBinary:X}, PC: {Regs.PC})");

            Regs.PC += 2;

            instructionImplementation.Execute(this, instructionBinary);
        }
    }
}
