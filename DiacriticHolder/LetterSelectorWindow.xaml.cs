using DiacriticHolder.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Runtime.InteropServices;
using static PInvoke.User32;

namespace DiacriticHolder
{
    public sealed partial class LetterSelectorWindow : Window
    {
        private Action _closeWindow;

        void LetterButton_onClick(object sender, ItemClickEventArgs e)
        {
            _closeWindow.Invoke();

            char value = char.Parse((string)e.ClickedItem);

            INPUT[] pInputs = new[]
            {
                new INPUT()
                {
                    type = InputType.INPUT_KEYBOARD,
                    Inputs = new INPUT.InputUnion()
                    {
                        ki = new KEYBDINPUT()
                        {
                            wScan = ScanCode.BACK,
                            wVk = VirtualKey.VK_BACK,
                            time = 0
                        }
                    }
                },
                new INPUT()
                {
                    type = InputType.INPUT_KEYBOARD,
                    Inputs = new INPUT.InputUnion()
                    {
                        ki = new KEYBDINPUT()
                        {
                            wScan = (ScanCode)value,
                            wVk = 0,
                            time = 0,
                            dwFlags = KEYEVENTF.KEYEVENTF_UNICODE
                        }
                    }
                }
            };

            SendInput(pInputs.Length, pInputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public LetterSelectorWindow(Key key, Action closeWindowAction)
        {
            _closeWindow = closeWindowAction;

            InitializeComponent();

            LetterView.ItemsSource = Diacritics.List[key];
        }

    }
}
