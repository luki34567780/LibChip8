using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct SHR : IInstruction
    {
        public ushort Mask => 0x8006;

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);

            cpu.Regs.VF = (byte)(cpu.Regs.V[x] & 0x01);
            cpu.Regs.V[x] >>= 1;
        }
    }
}
