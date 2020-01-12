using System;
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
                var result = c.ToWords(Convert.ToInt64(test.Key));
                Assert.AreEqual(test.Value, result);
            }
        }

        [TestMethod]
        public void TestOne()
        {
            var number = 1019;
            var text = "one thousand nineteen dollars";
            var c = new Converter();
            var result = c.ToWords(number);
            Assert.AreEqual(text, result);

        }
    }
}