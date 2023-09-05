using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 8xy3 - XOR Vx, Vy
    internal struct XORReg : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x8, -1, -1, 0x3);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);
            var y = (byte)((instr & 0x00F0) >> 4);

            cpu.Regs.V[x] ^= cpu.Regs.V[y];
            cpu.Regs.VF = 0;
        }
    }
}
