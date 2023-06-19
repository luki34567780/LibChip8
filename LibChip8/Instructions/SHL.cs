using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct SHL : IInstruction
    {
        public ushort Mask => 0x800E;

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (instr & 0x0F00) >> 8;
            cpu.Regs.V[0xF] = (byte)(cpu.Regs.V[x] >> 7);
            cpu.Regs.V[x] <<= 1;
        }
    }
}
