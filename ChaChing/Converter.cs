using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChaChing
{
    public class Converter
    {

        public const string Dollar = "dollar";
        public const string Hundred = "hundred";
        public string[] Separators = {"", "thousand", "million"};
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

        public string ConvertTens(int number)
        {
            var lessThan100 = number - number / 100 * 100;
            if (Cardinal.ContainsKey(lessThan100))
            {
                var item = Cardinal[lessThan100];
                if (lessThan100 != 0 || lessThan100 == 0 && number == 0)
                {
                    return item;
                }

                return "";
            }

            var cardinalNumber = FirstCardinalNumberSmallerThan(lessThan100);
            var remainingDigit = lessThan100 - cardinalNumber.Key;
            var remainingValue = Cardinal[remainingDigit];
            var resultNumber = string.Concat(cardinalNumber.Value, "-", remainingValue);

            return resultNumber;

        }

        public string ConvertHundreds(int number)
        {
            var tens = ConvertTens(number);

            number /= 100;
            var hundredsDigit = number % 10;
            if (hundredsDigit != 0)
            {
                var tensSeparator = tens != "" ? " " : "";
                return string.Concat(Cardinal[hundredsDigit], " ", Hundred, tensSeparator, tens);
            }

            return tens;
        }

        public string ToWords(decimal number)
        {
            var dict = new List<string>();
            
            var dollars = (int) number;
            var isSingle = dollars == 1;

            foreach (var separator in Separators)
            {
                var hundreds = ConvertHundreds(dollars);
                if (!string.IsNullOrWhiteSpace(hundreds))
                {
                    var separatorSpace = string.IsNullOrEmpty(separator) ? "" : " ";
                    dict.Add(string.Concat(hundreds, separatorSpace, separator));
                }
                dollars /= 1000;

                if (dollars <= 0) break;
            }

            dict.Insert(0, isSingle ? Dollar : $"{Dollar}s");

            return ReverseListAppender(dict);
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