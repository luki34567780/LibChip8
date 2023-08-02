using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 00EE 
    internal struct RET : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x0, 0x0, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            if (cpu.Regs.SP == 0)
                throw new Exception("RET encountered while SP is already zero!");

            cpu.Regs.SP--;
            cpu.Regs.PC = cpu.Stack[cpu.Regs.SP].ReturnAddress;
        }
    }
}
