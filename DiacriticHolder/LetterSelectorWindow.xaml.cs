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

        void LetterButton_onClick(object sender, RoutedEventArgs e)
        {
            _closeWindow.Invoke();

            Button button = (Button)sender;
            char value = char.Parse((string)button.Content);

            INPUT[] pInputs = new[]
            {
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

            foreach(string letter in Diacritics.List[key])
            {
                Button letterButton = new()
                {
                    Content = letter
                };
                letterButton.Click += LetterButton_onClick;

                LetterPanel.Children.Add(letterButton);
            }
        }
    }
}
