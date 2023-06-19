using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct SNEReg : IInstruction
    {
        public ushort Mask => 0x9000;

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (instr & 0x0F00) >> 8;
            var y = (instr & 0x00F0) >> 4;
            if (cpu.Regs.V[x] != cpu.Regs.V[y])
                cpu.Regs.PC += 2;
        }
    }
}
