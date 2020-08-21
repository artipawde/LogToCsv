using System;
using Xunit;
using System.IO;

namespace LogToCsvFile.Tests
{
    public class CsvFileTest
    {
        CsvFile csvFile = new CsvFile();

        [Fact]
        public void CheckingFileExtension()
        {
            string path = @"E:\CsvFile\seven";
            var result = csvFile.AddExtension(path);
            var expected = ".csv";
            var actual = Path.GetExtension(result);
            Assert.Equal(expected,actual);
        }
    }
}