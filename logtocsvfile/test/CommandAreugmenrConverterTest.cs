using System;
using Xunit;
using System.IO;
using System.Collections.Generic;

namespace LogToCsvFile.Tests
{
    public class TypeTests
    {
        // private HashSet<string> actualLevel = new HashSet<string>{ "WARN" , "DEBUG" , "TRACE" , "ERROR" , "EVENT", "INFO"};

        private HashSet<string> userLevel = new HashSet<string>();

        CommandArgumentConverter cmdArgsConverter = new CommandArgumentConverter();

        [Fact]
        public void CheckLevelValidate()
        {
            userLevel.Add("ERROR");
            userLevel.Add("INFO");
           // userLevel.Add("ABC");
            cmdArgsConverter.SetLevel(userLevel);
            bool actual = cmdArgsConverter.IsValidateLevels();

            var expected = true;
            Assert.Equal(expected,actual);
        }
        

        [Fact]
        public void CheckSourceFileExits()
        {
            string path = @"D:\LogFiles\demo.log";
            
          //  string path = @"D:\LogFiles\one";
        
            cmdArgsConverter.SetSourcePath(path);
            bool actual = cmdArgsConverter.IsSourceFileOrDiecoryValidate();

            var expected = true;
            Assert.Equal(expected,actual);
        }
    }
}
