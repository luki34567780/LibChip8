using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

using LibChip8;

namespace LibChip32.Instructions;
internal class NOP : IInstruction
{
    public byte InstructionClassByte { get; } = 0x00;
    public byte InstructionIdentByte { get; } = 0x01;

    public void Execute(CPU cpu, uint instr) { }
}
