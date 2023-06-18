using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct SEReg : IInstruction
    {
        public ushort Mask => 0x5000;

        public void Execute(CPU cpu, ushort instr)
        {
            byte register1Index = (byte)((instr & 0x0F00) >> 8);
            byte register2Index = (byte)((instr & 0x00F0) >> 8);

            if (cpu.Regs.V[register1Index] == cpu.Regs.V[register2Index])
            {
                // Skip the next instruction
                cpu.Regs.PC += 2;
            }
        }
    }
}
