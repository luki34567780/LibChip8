using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibChip8;

namespace Chip8.UI
{
    public partial class MainViewModel : ObservableObject
    {
        public const int Height = 32;
        public const int Width = 64;
        public CPU CPU { get; private set; }
        public PixelViewModel[] Data { get; private set; } = new PixelViewModel[Height * Width];

        public PixelViewModel GetPixelViewModel(int x, int y) => Data[x * Height + y];

        public MainViewModel()
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = new PixelViewModel();
            }
            CPU = new CPU();
            CPU.LoadImage(File.ReadAllBytes("logo.ch8"));
        }

        [RelayCommand]
        public void Tick()
        {
            CPU.RunTick();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    GetPixelViewModel(x, y).Color = CPU.Screen[x, y] == 0 ? System.Drawing.Color.Black: System.Drawing.Color.White;
                }
            }
        }
    }
}
