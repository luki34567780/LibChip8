using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 8xy6 - SHR Vx {, Vy}
    internal struct SHR : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x8, -1, -1, 0x6);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);

            byte val;
            if (cpu.QuirkConfig.ShiftBehaviour == ShiftBehaviour.VxVy)
            {
                var y = (byte)((instr & 0x00F0) >> 4);
                val = cpu.Regs.V[y];
            }
            else if (cpu.QuirkConfig.ShiftBehaviour == ShiftBehaviour.Vx)
            {
                val = cpu.Regs.V[x];
            }
            else
            {
                throw new InvalidOperationException("Invalid shift behaviour");
            }

            cpu.Regs.V[x] = (byte)(val >> 1);
            cpu.Regs.VF = (byte)(val & 0x01);
        }
    }
}
