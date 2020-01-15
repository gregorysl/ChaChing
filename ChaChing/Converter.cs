using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ChaChing
{
    public class Converter
    {
        private readonly string[] _separators = {"", "thousand", "million"};
        public Dictionary<int, string> Cardinal = new Dictionary<int, string>
        {
            {0, ""},
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
        
        private string ConvertCents(string centsString)
        {
            var centsPart = decimal.Parse(centsString);
            var centsWord = ConvertTens(centsPart);
            var centsEnding = centsString == "01" ? "" : "s";
            var centValue = string.Concat(" and ", centsWord, " ", Consts.Cent, centsEnding);
            return centValue;
        }

        public string ConvertTens(decimal number)
        {
            var tens = (int) (number % 100);
            if (Cardinal.ContainsKey(tens))
            {
                return Cardinal[tens];
            }

            var cardinalNumber = FirstCardinalNumberSmallerThan(tens);
            var remainingDigit = tens - cardinalNumber.Key;
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
            return string.Concat(Cardinal[hundredsDigit], " ", Consts.Hundred, tensSeparator, tens);

        }
        private string ConvertNumber(decimal number)
        {
            var integer = (int) number;
            var result = "";
            foreach (var separator in _separators)
            {
                var hundreds = ConvertHundreds(integer);
                if (!string.IsNullOrWhiteSpace(hundreds))
                {
                    var separatorSpace = string.IsNullOrEmpty(separator) ? "" : " ";
                    var secondSeparator = string.IsNullOrEmpty(result) ? "" : " ";
                    result = string.Concat(hundreds, separatorSpace, separator, secondSeparator, result);
                }

                integer /= 1000;

                if (integer <= 0) break;
            }
            return result;
        }

        public string ToWords(string input)
        {
            if (!IsValid(input))
            {
                return Consts.ErrorMessage;
            }
            var number = Convert.ToDecimal(input, Consts.CultureInfo);
            var numberParts = input.Split(',');
            var wordedNumber = numberParts[0] == "0" ? "zero" : ConvertNumber(number);
            var dollarsEnding = ((int)number).Equals(1) ? "" : "s";

            var centValue = numberParts.Length == 2 ? ConvertCents(numberParts[1]) : "";

            return string.Concat(wordedNumber, " ", Consts.Dollar, dollarsEnding, centValue);
        }

        private bool IsValid(string input)
        {
            var regex = new Regex(@"^(?:\d\s*){1,9}(?:\,\d{0,2})?$");
            return input != null && regex.Match(input).Success;
        }

        private KeyValuePair<int, string> FirstCardinalNumberSmallerThan(int number)
        {
            return Cardinal.Last(x => x.Key < number);
        }
    }
}