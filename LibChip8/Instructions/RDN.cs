using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct RDN : IInstruction
    {
        public ushort Mask => 0xC000;

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (instr & 0x0F00) >> 8;
            var kk = (byte)(instr & 0x00FF);
            cpu.Regs.V[x] = (byte)(Random.Shared.Next(0, 256) & kk);
        }
    }
}
