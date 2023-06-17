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
        public short Mask => 0x00EE;

        public void Execute(CPU cpu, short instr)
        {
            if (cpu.Regs.SP == 0)
                throw new Exception("RET encountered while SP is already zero!");

            cpu.Regs.PC = cpu.Stack[cpu.Regs.SP].ReturnAddress;
            cpu.Regs.SP--;
        }
    }
}
