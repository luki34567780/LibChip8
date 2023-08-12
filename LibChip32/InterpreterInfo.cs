using System.Runtime.InteropServices;

namespace LibChip8
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct InterpreterInfo
    {
        public uint MaxStackSize { get; set; }
        public uint EmulatorSpeed { get; set; }
        public uint ScreenWidth { get; set; }
        public uint ScreenHeight { get; set; }
        public byte ScreenMode { get; set; }

        public fixed byte Reserved[495];

    }
}
