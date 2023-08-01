using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 5xy0 - SE Vx, Vy
    internal struct SEReg : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x5, -1, -1, 0x0);
        }

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
