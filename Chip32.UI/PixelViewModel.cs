using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Chip8.UI
{
    public class PixelViewModel : ObservableObject
    {
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
        private Color _color;
    }
}
