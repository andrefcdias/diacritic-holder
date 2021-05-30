using DiacriticHolder.Types;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DiacriticHolder
{
    public sealed partial class LetterSelectorWindow : Window
    {
        public LetterSelectorWindow(Key key)
        {
            InitializeComponent();

            foreach(string letter in Diacritics.List[key])
            {
                LetterPanel.Children.Add(new Button()
                {
                    Content = letter
                });
            }
        }
    }
}
