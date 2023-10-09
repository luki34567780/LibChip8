using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // Bnnn - JP V0, addr
    internal struct JPV0ADDR : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0xB, -1, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            cpu.Stack[cpu.Regs.SP++].ReturnAddress = cpu.Regs.PC;

            ushort targetAddress;
            if (cpu.QuirkConfig.JumpOffsetBehaviour == JumpOffsetBehaviour.NNNPlusV0)
            {
                targetAddress = (ushort)((instr & 0x0FFF) + cpu.Regs.V0);
            }
            else if (cpu.QuirkConfig.JumpOffsetBehaviour == JumpOffsetBehaviour.XNNPlusVx)
            {
                var x = (instr & 0x0F00) >> 8;
                targetAddress = (ushort)((instr & 0x00FF) + cpu.Regs.V[x]);
            }
            else
            {
                throw new InvalidOperationException("Invalid jump offset behaviour");
            }
            cpu.Regs.PC = targetAddress;
        }
    }
}
