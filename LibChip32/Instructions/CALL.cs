using LibChip8;

namespace LibChip32.Instructions;

internal class CALL : IInstruction
{
    public byte InstructionClassByte { get; } = 0x01;
    public byte InstructionIdentByte { get; } = 0x02;

    public void Execute(CPU cpu, uint instr)
    {
        var x = instr & Helpers.NibbleMask;

        cpu.Memory.AddToStack(cpu.Regs.PC);
        cpu.Regs.PC = cpu.Regs.V[x];
    }
}
