using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChaChing.Test
{
    [TestClass]
    public class ConverterTests
    {
       [TestMethod]
        public void ZeroToHundred()
        {
            var tests = new Dictionary<int, string>
            {
                {0, "zero dollars"},
                {1, "one dollar"},
                {2, "two dollars"},
                {3, "three dollars"},
                {4, "four dollars"},
                {5, "five dollars"},
                {6, "six dollars"},
                {7, "seven dollars"},
                {8, "eight dollars"},
                {9, "nine dollars"},
                {10, "ten dollars"},
                {11, "eleven dollars"},
                {12, "twelve dollars"},
                {13, "thirteen dollars"},
                {14, "fourteen dollars"},
                {15, "fifteen dollars"},
                {16, "sixteen dollars"},
                {17, "seventeen dollars"},
                {18, "eighteen dollars"},
                {19, "nineteen dollars"},
                {20, "twenty dollars"},
                {21, "twenty-one dollars"},
                {22, "twenty-two dollars"},
                {23, "twenty-three dollars"},
                {24, "twenty-four dollars"},
                {25, "twenty-five dollars"},
                {26, "twenty-six dollars"},
                {27, "twenty-seven dollars"},
                {28, "twenty-eight dollars"},
                {29, "twenty-nine dollars"},
                {30, "thirty dollars"},
                {31, "thirty-one dollars"},
                {32, "thirty-two dollars"},
                {33, "thirty-three dollars"},
                {34, "thirty-four dollars"},
                {35, "thirty-five dollars"},
                {36, "thirty-six dollars"},
                {37, "thirty-seven dollars"},
                {38, "thirty-eight dollars"},
                {39, "thirty-nine dollars"},
                {40, "forty dollars"},
                {41, "forty-one dollars"},
                {42, "forty-two dollars"},
                {43, "forty-three dollars"},
                {44, "forty-four dollars"},
                {45, "forty-five dollars"},
                {46, "forty-six dollars"},
                {47, "forty-seven dollars"},
                {48, "forty-eight dollars"},
                {49, "forty-nine dollars"},
                {50, "fifty dollars"},
                {51, "fifty-one dollars"},
                {52, "fifty-two dollars"},
                {53, "fifty-three dollars"},
                {54, "fifty-four dollars"},
                {55, "fifty-five dollars"},
                {56, "fifty-six dollars"},
                {57, "fifty-seven dollars"},
                {58, "fifty-eight dollars"},
                {59, "fifty-nine dollars"},
                {60, "sixty dollars"},
                {61, "sixty-one dollars"},
                {62, "sixty-two dollars"},
                {63, "sixty-three dollars"},
                {64, "sixty-four dollars"},
                {65, "sixty-five dollars"},
                {66, "sixty-six dollars"},
                {67, "sixty-seven dollars"},
                {68, "sixty-eight dollars"},
                {69, "sixty-nine dollars"},
                {70, "seventy dollars"},
                {71, "seventy-one dollars"},
                {72, "seventy-two dollars"},
                {73, "seventy-three dollars"},
                {74, "seventy-four dollars"},
                {75, "seventy-five dollars"},
                {76, "seventy-six dollars"},
                {77, "seventy-seven dollars"},
                {78, "seventy-eight dollars"},
                {79, "seventy-nine dollars"},
                {80, "eighty dollars"},
                {81, "eighty-one dollars"},
                {82, "eighty-two dollars"},
                {83, "eighty-three dollars"},
                {84, "eighty-four dollars"},
                {85, "eighty-five dollars"},
                {86, "eighty-six dollars"},
                {87, "eighty-seven dollars"},
                {88, "eighty-eight dollars"},
                {89, "eighty-nine dollars"},
                {90, "ninety dollars"},
                {91, "ninety-one dollars"},
                {92, "ninety-two dollars"},
                {93, "ninety-three dollars"},
                {94, "ninety-four dollars"},
                {95, "ninety-five dollars"},
                {96, "ninety-six dollars"},
                {97, "ninety-seven dollars"},
                {98, "ninety-eight dollars"},
                {99, "ninety-nine dollars"}
            };
            var c = new Converter();
            foreach (var test in tests)
            {
                var result = c.ToWords(test.Key);
                Assert.AreEqual(test.Value, result);
            }
        }
    }
}
