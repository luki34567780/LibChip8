using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions;
internal class LDFVX : IInstruction
{
    public bool IsInstruction(Instruction instr)
    {
        return instr.CompareValues(0xF, -1, 0x2, 0x9);
    }

    public void Execute(CPU cpu, ushort instr)
    {
        var reg = (byte)((instr & 0x0F00) >> 8);
        var val = cpu.Regs.V[reg];
        cpu.Regs.I = (ushort)(CPU.FontSetStartAddress + (val * 5));
    }
}
