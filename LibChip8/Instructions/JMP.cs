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
        public short Mask => 0x1000;

        public void Execute(CPU cpu, short instr)
        {
            var targetAddress = (short)(instr & 0x0FFF);
            cpu.Regs.PC = targetAddress;
        }
    }
}
