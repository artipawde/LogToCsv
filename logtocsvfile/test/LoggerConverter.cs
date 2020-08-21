using System;
using System.Collections.Generic;
using Xunit;
using System.IO;


namespace LogToCsvFile.Tests
{
    public class CommandArgumentConverterTest
    {
        LoggerConverter loggerConverter = new LoggerConverter();
        CommandArgumentConverter cmdArgsConverter = new CommandArgumentConverter();

        private HashSet<string> actualLevel = new HashSet<string>{ "WARN" , "DEBUG" , "TRACE" , "ERROR" , "EVENT", "INFO"};

        private HashSet<string> userLevel = new HashSet<string>();

         [Fact]
        public void CheckSourceFileExits()
        {
            string sourcePath = @"..\..\..\InputOutputFile\demo.log";
            string destinationPath = @"..\..\..\InputOutputFile\demo.csv";
                    
            cmdArgsConverter.SetSourcePath(sourcePath);
            cmdArgsConverter.SetDestinationPath(destinationPath);
            loggerConverter.ReadDataFromFile(sourcePath,destinationPath);
            Assert.True(File.ReadAllLines (@"..\..\..\InputOutputFile\demo.csv")[0].Equals("No|  Date  | Time    |Level  |      Text      |"));
            File.Delete (@"..\..\..\InputOutputFile\demo.csv");
        }
    }
}