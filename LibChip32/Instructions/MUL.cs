using LibChip8;

namespace LibChip32.Instructions;

public class MUL : IInstruction
{
    // todo: manage overflow
    public byte InstructionClassByte { get; } = 0x03;
    public byte InstructionIdentByte { get; } = 0x02;

    public void Execute(CPU cpu, uint instr)
    {
        var y = instr & Helpers.NibbleMask;
        var x = (instr & (Helpers.NibbleMask << 4)) >> 4;

        var v = cpu.Regs.V;

        v[x] = v[x] * v[y];
    }
}
