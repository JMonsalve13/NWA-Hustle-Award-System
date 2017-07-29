using System;
using System.Collections.Immutable;

namespace NWA.HustleCards.Misc
{
    public static class RandomAssistant
    {
        private static Random rand = new Random();
        private static readonly ImmutableArray<char> base64Digits = new ImmutableArray<char>()
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'a',
            'b',
            'c',
            'd',
            'e',
            'f',
            'g',
            'h',
            'i',
            'j',
            'k',
            'l',
            'm',
            'n',
            'o',
            'p',
            'q',
            'r',
            's',
            't',
            'u',
            'v',
            'w',
            'x',
            'y',
            'z',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'I',
            'J',
            'K',
            'L',
            'M',
            'N',
            'O',
            'P',
            'Q',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z',
            '_',
            '-',
        };

        public static string RandBase64(int digits = 6)
        {
            string ret = "";
            for(int i = 0; i < digits; i++)
            {
                ret += base64Digits[rand.Next(base64Digits.Length)];
            }
            return ret;
        }
    }
}
