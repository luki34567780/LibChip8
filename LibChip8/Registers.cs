namespace LibChip8
{
    public class Registers
    {
        public ushort[] V = new ushort[16];
        public ushort V0 { get => V[0]; set => V[0] = value; }
        public ushort V1 { get => V[1]; set => V[1] = value; }
        public ushort V2 { get => V[2]; set => V[2] = value; }
        public ushort V3 { get => V[3]; set => V[3] = value; }
        public ushort V4 { get => V[4]; set => V[4] = value; }
        public ushort V5 { get => V[5]; set => V[5] = value; }
        public ushort V6 { get => V[6]; set => V[6] = value; }
        public ushort V7 { get => V[7]; set => V[7] = value; }
        public ushort V8 { get => V[8]; set => V[8] = value; }
        public ushort V9 { get => V[9]; set => V[9] = value; }
        public ushort VA { get => V[10]; set => V[10] = value; }
        public ushort VB { get => V[11]; set => V[11] = value; }
        public ushort VC { get => V[12]; set => V[12] = value; }
        public ushort VD { get => V[13]; set => V[13] = value; }
        public ushort VE { get => V[14]; set => V[14] = value; }
        public ushort VF { get => V[15]; set => V[15] = value; }
        public ushort I { get; set; }
        public ushort PC { get; set; } = 0x200;
        public byte SP { get; set; }
    }
}