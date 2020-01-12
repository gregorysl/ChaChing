﻿using System.Linq;
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
                Assert.AreEqual(test.Value, result);
            }
        }
        [TestMethod]
        public void CheckHundreds()
        {
            var data = new Helper().LoadTestDataDictionary();
            data.Remove(1);
                var tests = data.Where(x => x.Key < 1000);
            var c = new Converter();
            foreach (var test in tests)
            {
                var result = c.ConvertHundreds(test.Key);
                Assert.AreEqual(test.Value.Replace(" dollars",""), result);
            }
        }

        [TestMethod]
        public void TestOne()
        {
            const int number = 999993;
            const string text = "nine hundred ninety-nine thousand nine hundred ninety-three dollars";
            var c = new Converter();
            var result = c.ToWords(number);
            Assert.AreEqual(text, result);
        }
    }
}