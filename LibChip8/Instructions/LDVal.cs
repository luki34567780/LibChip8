using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct LDVal : IInstruction
    {
        public ushort Mask => 0x6000;

        public void Execute(CPU cpu, ushort instr)
        {
            byte registerIndex = (byte)((instr & 0x0F00) >> 8);
            byte value = (byte)(instr & 0x00FF);

            cpu.Regs.V[registerIndex] = value;
        }
    }
}
