using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 8xy0 - LD Vx, Vy
    internal struct LDReg : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x8, -1, -1, 0x0);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            byte x = (byte)((instr & 0x0F00) >> 8);
            byte y = (byte)((instr & 0x00F0) >> 4);

            cpu.Regs.V[y] = cpu.Regs.V[x];
        }
    }
}
