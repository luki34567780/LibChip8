using LibChip8;

namespace LibChip32.Instructions;

public class JMP_VX_VY : IInstruction
{
    public byte InstructionClassByte { get; } = 0x01;
    public byte InstructionIdentByte { get; } = 0x05;

    public void Execute(CPU cpu, uint instr)
    {
        var x = instr & Helpers.NibbleMask;
        var y = (instr & (Helpers.NibbleMask << 4)) >> 4;

        cpu.Regs.PC = cpu.Regs.V[x] + cpu.Regs.V[y];
    }
}
