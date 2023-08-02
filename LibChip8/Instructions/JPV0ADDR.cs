using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // Bnnn - JP V0, addr
    internal struct JPV0ADDR : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0xB, -1, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var targetAddress = (ushort)((instr & 0x0FFF) + cpu.Regs.V0);
            cpu.Regs.PC = targetAddress;
        }
    }
}
