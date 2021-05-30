using System.Collections.Generic;

namespace DiacriticHolder.Types
{
    public class Diacritics
    {
        public static readonly Dictionary<Key, string[]> List = new()
        {
            { Key.A, new[] { "à", "á", "â", "ã", "ä", "å", "æ", "ā", "ă", "ą" } },
            { Key.C, new[] { "ç", "ć", "ĉ", "ċ", "č" } },
            { Key.D, new[] { "ď", "đ" } },
            { Key.E, new[] { "è", "é", "ê", "ë", "ð", "ē", "ĕ", "ė", "ę", "ě" } },
            { Key.G, new[] { "ĝ", "ğ", "ġ", "ģ" } },
            { Key.H, new[] { "ĥ", "ħ" } },
            { Key.I, new[] { "ì", "í", "î", "ï", "ĩ", "ī", "ĭ", "į" } },
            { Key.J, new[] { "ĵ" } },
            { Key.K, new[] { "ķ", "ĸ" } },
            { Key.L, new[] { "ĺ", "ļ", "ľ", "ŀ", "ł" } },
            { Key.N, new[] { "ñ", "ń", "ņ", "ň", "ŉ", "ŋ" } },
            { Key.O, new[] { "ò", "ó", "ô", "õ", "ö", "ø", "ō", "ŏ", "ő", "œ" } },
            { Key.P, new[] { "þ" } },
            { Key.R, new[] { "ŕ", "ŗ", "ř" } },
            { Key.S, new[] { "ß", "ś", "ŝ", "ş", "š", "ſ" } },
            { Key.T, new[] { "ţ", "ť", "ŧ" } },
            { Key.U, new[] { "ù", "ú", "û", "ü", "ũ", "ū", "ŭ", "ů", "ű", "ų" } },
            { Key.W, new[] { "ŵ" } },
            { Key.Y, new[] { "ý", "ÿ", "ŷ" } },
            { Key.Z, new[] { "ź", "ż", "ž" } },
        };
    }
}
