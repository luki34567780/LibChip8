using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal class LDIADDR : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0xA, -1, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            cpu.Regs.I = (ushort)(instr & 0x0FFF);
        }
    }
}
