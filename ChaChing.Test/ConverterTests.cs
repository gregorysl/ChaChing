using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChaChing.Test
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void TestsFromFile()
        {
            var tests = new Helper().LoadTestDataDictionary();
            var c = new Converter();
            foreach (var test in tests)
            {
                var result = c.ToWords(test.Key);
                Assert.AreEqual(test.Value, result, $"Test:{test.Key}");
            }
        }
        [TestMethod]
        public void CheckHundreds()
        {
            var tests = LoadTestDataForNumbers(1000);
            var c = new Converter();
            foreach (var test in tests)
            {
                var result = c.ConvertHundreds(Convert.ToDecimal(test.Key, Consts.CultureInfo));
                Assert.AreEqual(test.Value.Replace(" dollars", ""), result);
            }
        }
        [TestMethod]
        public void CheckTens()
        {
            var tests = LoadTestDataForNumbers(100);
            var c = new Converter();
            foreach (var test in tests)
            {
                var result = c.ConvertTens(Convert.ToDecimal(test.Key));
                Assert.AreEqual(test.Value.Replace(" dollars", ""), result);
            }
        }

        [TestMethod]
        public void TestOne()
        {
            const string number = "1071339";
            const string text = "one million seventy-one thousand three hundred thirty-nine dollars";
            var c = new Converter();
            var result = c.ToWords(number);
            Assert.AreEqual(text, result);
        }

        [TestMethod]
        public void ShouldReturnErrorWrongInput()
        {
            var tests = new[] { "9999999999", "-1", "Number", "1.01", null };
            var c = new Converter();
            foreach (var test in tests)
            {
                var result = c.ToWords(test);
                Assert.AreEqual(Consts.ErrorMessage, result, $"Test input: '{test}'");
            }
        }


        private static IEnumerable<KeyValuePair<string, string>> LoadTestDataForNumbers(int max)
        {
            var data = new Helper().LoadTestDataDictionary();
            data.Remove("1");
            var tests = data.Where(x =>
            {
                var currentNumber = Convert.ToDouble(x.Key, Consts.CultureInfo);
                var numberAsInteger = currentNumber - Math.Truncate(currentNumber);
                return numberAsInteger.Equals(0) && currentNumber < max;
            });
            return tests;
        }
    }
}