using LibChip8;

namespace LibChip32.Instructions;

public class LD : IInstruction
{
    public byte InstructionClassByte { get; } = 0x02;
    public byte InstructionIdentByte { get; } = 0x00;

    public unsafe void Execute(CPU cpu, uint instr)
    {
        var x = instr & Helpers.NibbleMask;
        var val = *(uint*)&cpu.Memory.MemPtr[cpu.Regs.PC + Helpers.DefaultInstructionSize];

        cpu.Regs.V[x] = val;

        cpu.Regs.PC += 4;
    }
}
