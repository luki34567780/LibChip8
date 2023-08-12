using LibChip8;

namespace LibChip32.Instructions;

internal class JP_ADDRESS : IInstruction
{
    public byte InstructionClassByte { get; } = 0x01;
    public byte InstructionIdentByte { get; } = 0x01;

    public void Execute(CPU cpu, uint instr)
    {
        var x = instr & Helpers.NibbleMask;

        cpu.Regs.PC = cpu.Regs.V[x];
    }
}
