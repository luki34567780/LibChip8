using LibChip8;

namespace LibChip32.Instructions;

public class SNE : IInstruction
{
    public byte InstructionClassByte { get; } = 0x01;
    public byte InstructionIdentByte { get; } = 0x04;

    public unsafe void Execute(CPU cpu, uint instr)
    {
        var x = instr & Helpers.NibbleMask;
        var y = (instr & (Helpers.NibbleMask << 4)) >> 4;

        bool skipEight = false;

        if (cpu.Regs.V[x] != cpu.Regs.V[y])
        {
            uint nextInstructionStart = cpu.Regs.PC + Helpers.DefaultInstructionSize;
            var val1 = cpu.Memory[nextInstructionStart];
            var val2 = cpu.Memory[nextInstructionStart + 1];

            foreach (var (first, second) in Helpers.LongInstructions)
            {
                skipEight = val1 == first && val2 == second || skipEight;
            }

            // this code avoids a branch.
            // to do this it reinterprets the bool
            // as a int (0 = false, 1 = true)
            // multiplys the result with 4 and
            // adds that result to 4.
            cpu.Regs.PC = (uint)(4 + (4 * *(byte*)&skipEight));
        }
    }
}
