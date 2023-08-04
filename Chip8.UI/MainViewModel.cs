using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibChip8;
using Color = System.Windows.Media.Color;

namespace Chip8.UI
{
    public partial class MainViewModel : ObservableObject
    {
        public const int Height = 32;
        public const int Width = 64;
        public CPU CPU { get; private set; }

        private WriteableBitmap _bitmap;

        public WriteableBitmap Bitmap
        {
            get => _bitmap;
            set
            {
                SetProperty(ref _bitmap, value);
            }

        }

        public MainViewModel()
        {
            CPU = new CPU();
            CPU.LoadImage(File.ReadAllBytes("logo.ch8"));
            Bitmap = new WriteableBitmap(Height, Width, 96, 96, PixelFormats.Gray8, BitmapPalettes.Gray256);

        }

        [RelayCommand]
        public void Tick()
        {
            CPU.RunTick();

            Bitmap.Lock();
            Bitmap.WritePixels(new Int32Rect(0, 0, Height, Width), CPU.Screen.Pixels, Height, 0);
            Bitmap.Unlock();

            OnPropertyChanged(nameof(Bitmap));

            var bm = new Bitmap(64, 32);

            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    bm.SetPixel(i, j, CPU.Screen[i, j] == 0 ? System.Drawing.Color.Black : System.Drawing.Color.Wheat);
                }
            }

            bm.Save("output.png");
        }
    }
}
