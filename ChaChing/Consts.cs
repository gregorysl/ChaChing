using System.Globalization;

namespace ChaChing
{
    public static class Consts
    {        
        public static readonly CultureInfo CultureInfo = new CultureInfo("pl-PL");
        public const string ErrorMessage = "Error! Input is not valid or outside of suported range (0 - 999 999 999,99)";
        public const decimal MinValue = 0;
        public const decimal MaxValue = 999999999.99M;

        public const string Cent = "cent";
        public const string Dollar = "dollar";
        public const string Hundred = "hundred";
    }
}
