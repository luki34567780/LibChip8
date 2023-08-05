using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct JMP : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x1, -1, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var targetAddress = (ushort)(instr & 0x0FFF);

            cpu.Regs.PC = targetAddress;

            // restart test rom
            if (cpu.Regs.PC == 988)
                cpu.Regs.PC = 0x200;

        }
    }
}
