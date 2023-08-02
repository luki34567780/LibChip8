using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal class DRW : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0xD, -1, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            Span<byte> sprite = stackalloc byte[15];

            var x = cpu.Regs.V[(instr & 0x0F00) >> 8];
            var y = cpu.Regs.V[(instr & 0x00F0) >> 4];
            var height = instr & 0x000F;

            cpu.Memory.AsSpan(cpu.Regs.I, height).CopyTo(sprite);

            bool changed = false;

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    byte pixelColor = (byte)(sprite[row] & (1 << (7 - col))); // Fix the bit shift here
                    byte screenColor = cpu.Screen.Pixels[(x + col) % Screen.Width, (y + row) % Screen.Height]; // Apply wrapping
                    byte newColor = (byte)(pixelColor != 0 ? 1 : 0);

                    changed = changed || (screenColor == 1 && newColor == 0);

                    cpu.Screen.Pixels[(x + col) % Screen.Width, (y + row) % Screen.Height] = newColor; // Apply wrapping
                }
            }

            cpu.Regs.VF = (ushort)(changed ? 1 : 0);
        }
    }
}
