using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ChaChing.Test
{
    public class Helper
    {
        public string ReadResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public Dictionary<string, string> LoadTestDataDictionary()
        {
            var fileContents = ReadResource("TestData.csv");
            return fileContents.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(','))
                .ToDictionary(line => line[0], line => line[1]);
        }
    }
}