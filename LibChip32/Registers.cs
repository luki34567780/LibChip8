namespace LibChip8
{
    public class Registers
    {
        public uint[] V = new uint[16];
        public uint I { get; set; }
        public uint PC { get; set; } = 0x200;
        public byte SP { get; set; }
    }
}