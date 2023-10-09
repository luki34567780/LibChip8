using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using LibChip8;

using Color = System.Windows.Media.Color;

namespace Chip8.UI
{
    public partial class MainViewModel : ObservableObject
    {
        public const double HZ = 500;
        public const double TIMER_EVENTS_PER_SECOND = 100;
        public const double TIME_BETWEEN_TICKS = 1000 / TIMER_EVENTS_PER_SECOND;
        public static int ITERATIONS_PER_TICK = (int)Math.Ceiling(HZ / TIMER_EVENTS_PER_SECOND);

        public readonly object CpuLock = new();

        public const int Height = 32;
        public const int Width = 64;
        public Dispatcher UIDispatcher { get; set; }
        public CPU CPU { get; private set; }

        private readonly WriteableBitmap _bitmap;
        private readonly System.Timers.Timer _timer;
        private readonly Queue<byte> _pendingKeyClearQueue = new();

        public WriteableBitmap Bitmap
        {
            get => _bitmap;
            init => SetProperty(ref _bitmap, value);
        }

        public void KeyChanged(object sender, KeyEventArgs e)
        {
            Debugger.Log(0, "", $"Processing Key. Key: {e.Key}, IsDown: {e.IsDown}");
            var val = KeyToByte(e.Key);

            if (e.IsDown)
                CPU.Keyboard.SetKey(val, true);
            else
            {
                lock (CpuLock)
                {
                    _pendingKeyClearQueue.Enqueue(val);
                }
            }
        }

        public byte KeyToByte(Key key)
        {
            switch (key)
            {
                case Key.D1:
                    return 0x1;
                case Key.D2:
                    return 0x2;
                case Key.D3:
                    return 0x3;
                case Key.D4:
                    return 0xC;
                case Key.Q:
                    return 0x4;
                case Key.W:
                    return 0x5;
                case Key.E:
                    return 0x6;
                case Key.R:
                    return 0xD;
                case Key.A:
                    return 0x7;
                case Key.S:
                    return 0x8;
                case Key.D:
                    return 0x9;
                case Key.F:
                    return 0xE;
                case Key.Z:
                    return 0xA;
                case Key.X:
                    return 0x0;
                case Key.C:
                    return 0xB;
                case Key.V:
                    return 0xF;
                default:
                    return 0xFF;
            }
        }

        public MainViewModel()
        {
            UIDispatcher = Dispatcher.CurrentDispatcher;
            CPU = new CPU();
            CPU.LoadImage(File.ReadAllBytes("images/8ce 8ttorney Disk1 (by SysL)(2016).ch8"));
            Bitmap = new WriteableBitmap(Height, Width, 96, 96, PixelFormats.Gray8, null);

            _timer = new System.Timers.Timer(TIME_BETWEEN_TICKS);
            _timer.Elapsed += (_, _2) => TimerTick();
            _timer.Start();
        }

        private void TimerTick()
        {
            if (Monitor.TryEnter(CpuLock, 1))
            {
                try
                {
                    for (int i = 0; i < ITERATIONS_PER_TICK; i++)
                    {
                        Tick();
                    }

                    while (_pendingKeyClearQueue.TryDequeue(out var key))
                    {
                        CPU.Keyboard.SetKey(key, false);
                    }

                    foreach (var key in _pendingKeyClearQueue)
                    {
                        CPU.Keyboard.SetKey(key, false);
                    }
                }
                finally
                {
                    Monitor.Exit(CpuLock);
                }
            }

        }

        [RelayCommand]
        public async Task MultiTick()
        {
            int c = 0;
            for (int i = 0; i < 100000; i++)
            {
                CPU.RunTick();

                if (CPU.LastInstruction.GetType() == typeof(LibChip8.Instructions.DRW))
                    UpdateBitmap();

                c++;
            }
        }

        private void UpdateBitmap()
        {
            UIDispatcher.Invoke(() =>
            {
                Bitmap.Lock();
                Bitmap.WritePixels(new Int32Rect(0, 0, Height, Width), CPU.Screen.Pixels, Height, 0);
                Bitmap.Unlock();
                OnPropertyChanged(nameof(Bitmap));

            }, DispatcherPriority.Render);

        }

        [RelayCommand]
        public void Tick()
        {
            CPU.RunTick();

            if (CPU.LastInstruction.GetType() == typeof(LibChip8.Instructions.DRW))
            {
                UpdateBitmap();
            }
        }
    }
}
