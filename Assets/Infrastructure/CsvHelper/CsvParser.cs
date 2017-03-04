using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace CsvHelper
{
    public class CsvParser
    {
        private TextReader reader;
        private CsvConfiguration configuration;

        public CsvParser(TextReader reader, CsvConfiguration configuration)
        {
            this.reader = reader;
            this.configuration = configuration;
        }
        public string[] Read()
        {
            var line = reader.ReadLine();
            return (line != null) ? line.Split(configuration.Delimiter) : null;
        }
    }
}
