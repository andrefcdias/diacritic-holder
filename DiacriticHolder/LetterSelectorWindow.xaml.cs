using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DiacriticHolder
{
    public sealed partial class LetterSelectorWindow : Window
    {
        public LetterSelectorWindow(string key)
        {
            InitializeComponent();

            TextBlock letter = new()
            {
                Text = key
            };

            LetterPanel.Children.Add(letter);
        }
    }
}
