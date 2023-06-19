using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct XORReg : IInstruction
    {
        public ushort Mask => 0x8003;

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);
            var y = (byte)((instr & 0x00F0) >> 4);

            cpu.Regs.V[x] ^= cpu.Regs.V[y];
        }
    }
}
