using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions;

// Fx07 - LD Vx, DT
internal class LDVXDT : IInstruction
{
    public bool IsInstruction(Instruction instr)
    {
        return instr.CompareValues(0xF, -1, 0x0, 0x7);
    }

    public void Execute(CPU cpu, ushort instr)
    {
        var x = (byte)((instr & 0x0F00) >> 8);
        cpu.Regs.V[x] = cpu.Timers.DT;
    }
}
