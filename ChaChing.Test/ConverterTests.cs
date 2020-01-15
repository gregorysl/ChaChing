﻿using System;
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
                Assert.AreEqual(test.Value, result);
            }
        }
        [TestMethod]
        public void CheckHundreds()
        {
            var tests = LoadTestDataForNumbers(1000);
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
        public void ShouldReturnErrorOnTooLargeNumber()
        {
            const string number = "9999999999";
            const string text = "Error! Input number should be between 0 and 999 999 999,99";
            var c = new Converter();
            var result = c.ToWords(number);
            Assert.AreEqual(text, result);
        }
        
        [TestMethod]
        public void ShouldReturnErrorOnTooSmallNumber()
        {
            const string number = "-1";
            const string text = "Error! Input number should be between 0 and 999 999 999,99";
            var c = new Converter();
            var result = c.ToWords(number);
            Assert.AreEqual(text, result);
        }
        
        private static IEnumerable<KeyValuePair<string, string>> LoadTestDataForNumbers(int max)
        {
            var data = new Helper().LoadTestDataDictionary();
            data.Remove("1");
            var tests = data.Where(x=>
            {
                var currentNumber = Convert.ToDouble(x.Key);
                var numberAsInteger = currentNumber - Math.Truncate(currentNumber);
                return numberAsInteger.Equals(0) && currentNumber < max;
            });
            return tests;
        }
    }
}