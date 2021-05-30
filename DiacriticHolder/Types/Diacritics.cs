using System.Collections.Generic;

namespace DiacriticHolder.Types
{
    public class Diacritics
    {
        public static readonly Dictionary<Key, string[]> List = new()
        {
            { Key.A, new[] { "a", "à", "á", "â", "ã", "ä", "å", "æ", "ā", "ă", "ą" } },
            { Key.C, new[] { "c", "ç", "ć", "ĉ", "ċ", "č" } },
            { Key.D, new[] { "d", "ď", "đ" } },
            { Key.E, new[] { "e", "è", "é", "ê", "ë", "ð", "ē", "ĕ", "ė", "ę", "ě" } },
            { Key.G, new[] { "g", "ĝ", "ğ", "ġ", "ģ" } },
            { Key.H, new[] { "h", "ĥ", "ħ" } },
            { Key.I, new[] { "i", "ì", "í", "î", "ï", "ĩ", "ī", "ĭ", "į" } },
            { Key.J, new[] { "j", "ĵ" } },
            { Key.K, new[] { "k", "ķ", "ĸ" } },
            { Key.L, new[] { "i", "ĺ", "ļ", "ľ", "ŀ", "ł" } },
            { Key.N, new[] { "n", "ñ", "ń", "ņ", "ň", "ŉ", "ŋ" } },
            { Key.O, new[] { "o", "ò", "ó", "ô", "õ", "ö", "ø", "ō", "ŏ", "ő", "œ" } },
            { Key.P, new[] { "p", "þ" } },
            { Key.R, new[] { "r", "ŕ", "ŗ", "ř" } },
            { Key.S, new[] { "s", "ß", "ś", "ŝ", "ş", "š", "ſ" } },
            { Key.T, new[] { "t", "ţ", "ť", "ŧ" } },
            { Key.U, new[] { "u", "ù", "ú", "û", "ü", "ũ", "ū", "ŭ", "ů", "ű", "ų" } },
            { Key.W, new[] { "w", "ŵ" } },
            { Key.Y, new[] { "y", "ý", "ÿ", "ŷ" } },
            { Key.Z, new[] { "z", "ź", "ż", "ž" } },
        };
    }
}
