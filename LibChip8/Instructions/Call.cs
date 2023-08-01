using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct Call : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr[0] == 2;
        }

        public void Execute(CPU cpu, ushort instr)
        {
            if (cpu.Regs.SP == 15)
                throw new Exception("stack overflow!");

            var addr = (ushort)(instr & 0x0FFF);
            cpu.Regs.SP++;
            cpu.Stack[cpu.Regs.SP].ReturnAddress = cpu.Regs.PC;
            cpu.Regs.PC = addr;
        }
    }
}
