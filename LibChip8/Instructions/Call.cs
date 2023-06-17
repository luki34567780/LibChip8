using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct Call : IInstruction
    {
        public short Mask => 0x2000;

        public void Execute(CPU cpu, short instr)
        {
            if (cpu.Regs.SP == 15)
                throw new Exception("stack overflow!");

            var addr = (short)(instr & 0x0FFF);
            cpu.Regs.SP++;
            cpu.Stack[cpu.Regs.SP].ReturnAddress = cpu.Regs.PC;
            cpu.Regs.PC = addr;
        }
    }
}
