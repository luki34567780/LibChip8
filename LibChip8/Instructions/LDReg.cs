using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct LDReg : IInstruction
    {
        public ushort Mask => 0x8000;

        public void Execute(CPU cpu, ushort instr)
        {
            byte x = (byte)((instr & 0x0F00) >> 8);
            byte y = (byte)((instr & 0x00F0) >> 4);

            cpu.Regs.V[x] = cpu.Regs.V[y];
        }
    }
}
