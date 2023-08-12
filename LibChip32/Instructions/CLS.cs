using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibChip8;

namespace LibChip32.Instructions;
internal class CLS : IInstruction
{
    public byte InstructionClassByte { get; } = 0x00;
    public byte InstructionIdentByte { get; } = 0x02;

    public void Execute(CPU cpu, uint instr)
    {
        Helpers.UnimplementedInstruction();
    }
}
