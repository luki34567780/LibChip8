namespace LibChip8
{
    public class Registers
    {
        public byte[] V = new byte[16];
        public byte V0 { get => V[0]; set => V[0] = value; }
        public byte V1 { get => V[1]; set => V[1] = value; }
        public byte V2 { get => V[2]; set => V[2] = value; }
        public byte V3 { get => V[3]; set => V[3] = value; }
        public byte V4 { get => V[4]; set => V[4] = value; }
        public byte V5 { get => V[5]; set => V[5] = value; }
        public byte V6 { get => V[6]; set => V[6] = value; }
        public byte V7 { get => V[7]; set => V[7] = value; }
        public byte V8 { get => V[8]; set => V[8] = value; }
        public byte V9 { get => V[9]; set => V[9] = value; }
        public byte VA { get => V[10]; set => V[10] = value; }
        public byte VB { get => V[11]; set => V[11] = value; }
        public byte VC { get => V[12]; set => V[12] = value; }
        public byte VD { get => V[13]; set => V[13] = value; }
        public byte VE { get => V[14]; set => V[14] = value; }
        public byte VF { get => V[15]; set => V[15] = value; }
        public ushort I { get; set; }
        public ushort PC { get; set; } = 0x200;
        public byte SP { get; set; }
    }
}