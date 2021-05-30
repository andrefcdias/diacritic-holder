using Microsoft.UI.Xaml;
using PInvoke;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using static PInvoke.User32;

namespace DiacriticHolder
{
    public partial class App : Application
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCaretPos(out POINT lpPoint);

        public struct GUITHREADINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public Rectangle rcCaret;
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetGUIThreadInfo(uint idThread, ref GUITHREADINFO lpgui);

        private Dictionary<Key, int> keyCounter = new();
        private Window popup;

        private bool GetCaretPosition(ref POINT point)
        {
            GUITHREADINFO guiInfo = new();
            guiInfo.cbSize = Marshal.SizeOf(guiInfo);

            if (!GetGUIThreadInfo(0, ref guiInfo))
                throw new Win32Exception();

            point.x = guiInfo.rcCaret.X;
            point.y = guiInfo.rcCaret.Y;

            ClientToScreen(guiInfo.hwndCaret, ref point);

            return point.x != 0 && point.y != 0;
        }

        private void OpenPopup(Key key)
        {
            POINT popupPosition = new();
            if (!GetCaretPosition(ref popupPosition))
            {
                GetCursorPos(out popupPosition);
            }

            popup = new LetterSelectorWindow(key.ToString());
            popup.Activate();
            IntPtr popupHandler = GetActiveWindow();

            SetWindowPos(popupHandler, SpecialWindowHandles.HWND_TOPMOST, popupPosition.x, popupPosition.y, 300, 200, 0);
        }

        private void ClosePopup()
        {
            popup.Close();
            popup = null;
        }

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var kr = new KeyListener();
            //using (var kr = new KeyListener())
            //{
            kr.RegisterHook((key) =>
            {
                bool hasKey = keyCounter.TryGetValue(key, out int occurrences);
                keyCounter[key] = hasKey ? ++occurrences : 1;

                //if(occurrences > 3 && popup == null)
                if (popup == null)
                {
                    OpenPopup(key);
                }
            }, (key) =>
            {
                keyCounter[key] = 0;
                
                if(popup != null)
                {
                    ClosePopup();
                }
            });
            //}
        }
    }
}
