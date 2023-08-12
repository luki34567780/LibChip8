using LibChip8;

namespace LibChip32.Instructions;

internal class RET : IInstruction
{
    public byte InstructionClassByte { get; } = 0x01;
    public byte InstructionIdentByte { get; } = 0x00;

    public void Execute(CPU cpu, uint instr)
    {
        cpu.Regs.PC = cpu.Memory.RemoveFromStack().Value;
    }
}
