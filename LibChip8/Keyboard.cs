using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8;
public class Keyboard
{
    public bool[] Keys { get; } = new bool[16];

    public delegate void KeyPressedEventHandler(byte key);
    public event KeyPressedEventHandler? KeyPressed;

    public void Clear()
    {
        for (int i = 0; i < Keys.Length; i++)
        {
            Keys[i] = false;
        }
    }

    public void SetKey(byte key, bool pressed)
    {
        if (key == byte.MaxValue)
            return;

        Keys[key] = pressed;

        if (pressed)
        {
            KeyPressed?.Invoke(key);
        }

    }
}
