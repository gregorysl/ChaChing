using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChaChing
{
    public class Converter
    {

        public const string Dollar = " dollar";
        public const string Hundred = " hundred";
        public const string Thousand = " thousand";

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

        public string ConvertHundreds(int dollars)
        {
            var sb= new StringBuilder();
            var dict = new List<string>();
            var lessThan100 = dollars - dollars / 100 * 100;
            if (Cardinal.ContainsKey(lessThan100))
            {
                var item = Cardinal[lessThan100];
                if (lessThan100 != 0 || lessThan100 == 0 && dollars == 0)
                {
                    dict.Add(item);
                }
            }
            else
            {
                var smaller = FirstSmaller(lessThan100);
                sb.Append(smaller.Value);
                var remaining = lessThan100 - smaller.Key;
                if (remaining > 0)
                {
                    var remainingKey = Cardinal[remaining];
                    sb.Append("-").Append(remainingKey);
                }

                dict.Add(sb.ToString());
                sb.Clear();
            }

            dollars /= 100;
            var hundreds = dollars % 10;
            if (hundreds != 0)
            {
                var hundredToAdd = dict.Any() ? $"{Hundred} " : Hundred;
                dict.Add(hundredToAdd);
                dict.Add(Cardinal[hundreds]);
            }
            return ReverseListAppender(sb,dict);
        }

        public string ToWords(float number)
        {
            var dict = new List<string>();
            var sb = new StringBuilder();

            var dollars = (int) number;
            var isSingle = dollars == 1;
            
            var a = new[] {"", "thousand"};
            
            var hundreds = ConvertHundreds(dollars);
            dict.Add(hundreds);
            dollars /= 1000;
            if (dollars > 0)
            {
                var thousands = ConvertHundreds(dollars);
                var thousandToAdd = !string.IsNullOrEmpty(hundreds) ? $"{Thousand} " : Thousand;
                dict.Add(thousandToAdd);
                dict.Add(thousands);
                dollars /= 1000;
            }

            sb.Append(Dollar);
            if (!isSingle)
            {
                sb.Append("s");
            }

            dict.Insert(0, sb.ToString());
            sb.Clear();

            return ReverseListAppender(sb,dict);
        }

        private string ReverseListAppender(StringBuilder sb,List<string> dict)
        {
            for (var i = dict.Count - 1; i >= 0; i--)
            {
                sb.Append(dict[i]);
            }

            return sb.ToString();
        }

        private KeyValuePair<int, string> FirstSmaller(int number)
        {
            return Cardinal.Last(x => x.Key < number);
        }
    }
}