using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8;
public class Keyboard
{
    public bool[] Keys { get; } = new bool[16];
    public byte LastPressedKey { get; set; } = 0xFF;

    public delegate void KeyPressedEventHandler(byte key);
    public event KeyPressedEventHandler? KeyPressed;

    public void SetKey(byte key, bool pressed)
    {
        Keys[key] = pressed;
        if (pressed)
        {
            LastPressedKey = key;
            KeyPressed?.Invoke(key);
        }

        Keys[key] = pressed;
    }
}
