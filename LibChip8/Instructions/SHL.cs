using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 8xyE - SHL Vx {, Vy}
    internal struct SHL : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x8, -1, -1, 0xE);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (instr & 0x0F00) >> 8;

            byte val;

            if (cpu.QuirkConfig.ShiftBehaviour == ShiftBehaviour.VxVy)
            {
                var y = (instr & 0x00F0) >> 4;
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

            var vf = (byte)((val & 0x80) >> 7);

            cpu.Regs.V[x] = (byte)(val << 1);

            cpu.Regs.VF = vf;


        }
    }
}
