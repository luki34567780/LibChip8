using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 3xkk - SE Vx, byte
    internal struct SEVal : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x3, -1, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            byte registerIndex = (byte)((instr & 0x0F00) >> 8);
            byte value = (byte)(instr & 0x00FF);

            if (cpu.Regs.V[registerIndex] == value)
            {
                // Skip the next instruction
                cpu.Regs.PC += 2;
            }
        }
    }
}
