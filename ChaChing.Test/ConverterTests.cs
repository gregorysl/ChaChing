using System;
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
                var result = c.ToWords(Convert.ToDecimal(test.Key));
                Assert.AreEqual(test.Value, result);
            }
        }
        [TestMethod]
        public void CheckHundreds()
        {
            var data = new Helper().LoadTestDataDictionary();
            data.Remove("1");
            var tests = data.Where(x => (Convert.ToDouble(x.Key) - Math.Truncate(Convert.ToDouble(x.Key))).Equals(0) && Convert.ToDouble(x.Key) < 1000);
            var c = new Converter();
            foreach (var test in tests)
            {
                var result = c.ConvertHundreds(Convert.ToDecimal(test.Key));
                Assert.AreEqual(test.Value.Replace(" dollars", ""), result);
            }
        }
        [TestMethod]
        public void CheckTens()
        {
            var data = new Helper().LoadTestDataDictionary();
            data.Remove("1");
            var tests = data.Where(x => (Convert.ToDouble(x.Key) - Math.Truncate(Convert.ToDouble(x.Key))).Equals(0) && Convert.ToDouble(x.Key) < 100);
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
            const int number = 1071339;
            const string text = "one million seventy-one thousand three hundred thirty-nine dollars";
            var c = new Converter();
            var result = c.ToWords(number);
            Assert.AreEqual(text, result);
        }
    }
}