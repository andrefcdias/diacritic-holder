using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PInvoke;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static DiacriticHolder.Types.Win32;
using static PInvoke.User32;

namespace DiacriticHolder
{
    public partial class App : Application
    {
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

            popup = new LetterSelectorWindow(key, () => ClosePopup());
            popup.ExtendsContentIntoTitleBar = false;
            popup.SetTitleBar(new Grid());
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
            new TestWindow().Activate();
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

                //if (occurrences > 3 && popup == null)
                if (popup == null)
                {
                    OpenPopup(key);
                }
            }, (key) =>
            {
                keyCounter[key] = 0;
                
                //if(popup != null)
                //{
                //    ClosePopup();
                //}
            });
            //}
        }
    }
}
