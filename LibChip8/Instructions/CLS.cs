using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 00E0
    internal class CLS : IInstruction
    {
        public bool IsInstruction(Instruction instr) => instr.CompareValues(0x0, 0x0, 0xE, 0x0);

        public void Execute(CPU cpu, ushort instr)
        {
            for (int i = 0; i < 64; i++)
            {
                for (int  j = 0; j < 32; j++)
                {
                    cpu.Screen.Pixels[i, j] = 0;
                }
            }
        }
    }
}
