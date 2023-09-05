using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibChip8.Instructions
{
    public class DRW : IInstruction
    {
        private const byte OnValue = 255;
        private const byte OffValue = 0;
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0xD, -1, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            Span<byte> sprite = stackalloc byte[15];

            var startX = cpu.Regs.V[(instr & 0x0F00) >> 8];
            var startY = cpu.Regs.V[(instr & 0x00F0) >> 4];
            var height = instr & 0x000F;

            for (var i = 0; i < height; i++)
            {
                var spriteLine = cpu.Memory[cpu.Regs.I + i]; // A line of the sprite to render

                for (var bit = 0; bit < 8; bit++)
                {
                    var x = (startX + bit) % Screen.Width;
                    var y = (startY + i) % Screen.Height;

                    var spriteBit = ((spriteLine >> (7 - bit)) & 1);
                    var oldBit = cpu.Screen[x, y];
                    

                    // New bit is XOR of existing and new.
                    var newBit = oldBit ^ spriteBit;

                    if (newBit != 0)
                        cpu.Screen[x, y] = OnValue;
                    else // Otherwise write a pending clear
                        cpu.Screen[x, y] = OffValue;

                    // If we wiped out a pixel, set flag for collission.
                    if (oldBit != 0 && newBit == 0)
                        cpu.Regs.V[0xF] = 1;
                }
            }
        }
    }
}
