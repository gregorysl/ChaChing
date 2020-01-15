using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChaChing
{
    public class Converter
    {
        private const string ErrorMessage = "Error! Input number should be between 0 and 999 999 999,99";
        private const decimal MinValue = 0;
        private const decimal MaxValue = 999999999.99M;

        private const string Cent = "cent";
        private const string Dollar = "dollar";
        private const string Hundred = "hundred";
        private readonly string[] _separators = {"", "thousand", "million"};
        public Dictionary<int, string> Cardinal = new Dictionary<int, string>
        {
            {0, "zero"},
            {1, "one"},
            {2, "two"},
            {3, "three"},
            {4, "four"},
            {5, "five"},
            {6, "six"},
            {7, "seven"},
            {8, "eight"},
            {9, "nine"},
            {10, "ten"},
            {11, "eleven"},
            {12, "twelve"},
            {13, "thirteen"},
            {14, "fourteen"},
            {15, "fifteen"},
            {16, "sixteen"},
            {17, "seventeen"},
            {18, "eighteen"},
            {19, "nineteen"}
        };

        public Dictionary<int, string> Tens = new Dictionary<int, string>
        {
            {20, "twenty"},
            {30, "thirty"},
            {40, "forty"},
            {50, "fifty"},
            {60, "sixty"},
            {70, "seventy"},
            {80, "eighty"},
            {90, "ninety"}
        };

        public Converter()
        {
            foreach (var ten in Tens)
            {
                Cardinal.Add(ten.Key, ten.Value);
            }
        }

        private string ConvertCents(decimal number)
        {
            var tensCent = (int) ((number - Math.Truncate(number)) * 100);
            var wordNumber = ConvertHundreds(tensCent);
            var centsEnding = tensCent == 1 ? "" : "s";
            var centValue = string.Concat(" and ", wordNumber, " ", Cent, centsEnding);
            return centValue;
        }

        public string ConvertTens(decimal number)
        {
            var restFrom100 = (int) (number % 100);
            if (Cardinal.ContainsKey(restFrom100))
            {
                var item = Cardinal[restFrom100];
                return !restFrom100.Equals(0) || restFrom100.Equals(0) && number.Equals(0) ? item : "";
            }

            var cardinalNumber = FirstCardinalNumberSmallerThan(restFrom100);
            var remainingDigit = restFrom100 - cardinalNumber.Key;
            var remainingValue = Cardinal[remainingDigit];
            var resultNumber = string.Concat(cardinalNumber.Value, "-", remainingValue);

            return resultNumber;

        }

        public string ConvertHundreds(decimal number)
        {
            var tens = ConvertTens(number);

            number /= 100;
            var hundredsDigit = (int)(number % 10);
            if (hundredsDigit == 0) return tens;
            var tensSeparator = tens != "" ? " " : "";
            return string.Concat(Cardinal[hundredsDigit], " ", Hundred, tensSeparator, tens);

        }
        private string ConvertNumber(decimal number)
        {
            var sb = new StringBuilder();
            var dict = new List<string>();
            var integer = (int) number;

            foreach (var separator in _separators)
            {
                var hundreds = ConvertHundreds(integer);
                if (!string.IsNullOrWhiteSpace(hundreds))
                {
                    var separatorSpace = string.IsNullOrEmpty(separator) ? "" : " ";
                    var separatorWord = string.Concat(hundreds, separatorSpace, separator);
                    dict.Add(separatorWord);
                }

                integer /= 1000;

                if (integer <= 0) break;
            }
            
            return ReverseListAppender(dict);
        }

        public string ToWords(decimal number)
        {
            if(MinValue > number ^ number > MaxValue)
            {
                return ErrorMessage;
            }
            var wordedNumber = ConvertNumber(number);
            var dollarsEnding = ((int)number).Equals(1) ? "" : "s";
            var centValue = number != Math.Truncate(number) ? ConvertCents(number) : "";

            return string.Concat(wordedNumber, " ", Dollar, dollarsEnding, centValue);
        }

        private string ReverseListAppender(List<string> dict)
        {
            var sb = new StringBuilder();
            for (var i = dict.Count - 1; i >= 0; i--)
            {
                sb.Append(dict[i]).Append(" ");
            }

            return sb.ToString().Trim();
        }

        private KeyValuePair<int, string> FirstCardinalNumberSmallerThan(int number)
        {
            return Cardinal.Last(x => x.Key < number);
        }
    }
}