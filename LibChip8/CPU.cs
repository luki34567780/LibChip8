using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LibChip8.Instructions;

using static System.Net.Mime.MediaTypeNames;

namespace LibChip8
{
    public class CPU
    {
        public const int FontSetStartAddress = 0x0;
        public IInstruction LastInstruction { get; private set; }
        private byte[] _image;
        public CPU()
        {
            // initialize memory at 0x000 to 0x1FF with the fontset
            new byte[]
            {
                0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
                0x20, 0x60, 0x20, 0x20, 0x70, // 1
                0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
                0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
                0x90, 0x90, 0xF0, 0x10, 0x10, // 4
                0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
                0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
                0xF0, 0x10, 0x20, 0x40, 0x40, // 7
                0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
                0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
                0xF0, 0x90, 0xF0, 0x90, 0x90, // A
                0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
                0xF0, 0x80, 0x80, 0x80, 0xF0, // C
                0xE0, 0x90, 0x90, 0x90, 0xE0, // D
                0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
                0xF0, 0x80, 0xF0, 0x80, 0x80, // F
            }.CopyTo(Memory.AsSpan());

            Decoder = new InstructionDecoder(this);
        }

        public QuirkConfig QuirkConfig = new ();
        public InstructionDecoder Decoder { get; }
        public Timers Timers { get; } = new();
        public Registers Regs { get; } = new();
        public Stack[] Stack { get; } = new Stack[16];
        public Screen Screen { get; } = new();
        public Keyboard Keyboard { get; } = new();
        public byte[] Memory { get; } = new byte[4096];

        public void LoadImage(byte[] image)
        {
            _image = image.ToArray();
            image.CopyTo(Memory.AsSpan(0x200));
        }

        public void PushStackFrame(ushort address)
        {
            Stack[Regs.SP++].ReturnAddress = address;
        }

        public ushort PopStackFrame()
        {
            Regs.SP--;
            return Stack[Regs.SP].ReturnAddress;
        }

        public void RunTick()
        {
            var instructionBinary = (ushort)(Memory[Regs.PC] << 8 | Memory[Regs.PC + 1]);
            var instructionImplementation = Decoder.DecodeInstruction(instructionBinary);
            //Console.WriteLine($"Executing instruction {instructionImplementation.ToString().Split(".").Last()} (Hex {instructionBinary:X}, PC: {Regs.PC})");

            Regs.PC += 2;
            
            LastInstruction = instructionImplementation;

            instructionImplementation.Execute(this, instructionBinary);
        }
    }
}
