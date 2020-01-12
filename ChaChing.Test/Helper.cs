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

        public Dictionary<int, string> LoadTestDataDictionary()
        {
            var fileContents = ReadResource("TestData.csv");
            return fileContents.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(','))
                .Select(x => new KeyValuePair<int, string>(Convert.ToInt32(x[0]), x[1]))
                .ToDictionary(line => line.Key, line => line.Value);
        }
    }
}