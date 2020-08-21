using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogToCsvFile
{
    class LogToCsv
    {
        static void Main(string[] args)
        {
            CommandArgumentConverter cmdArgConverter = new CommandArgumentConverter();
            cmdArgConverter.CheckArgument(args);
           try{
            cmdArgConverter.ConvertToLoggerFile(args);
           }
           catch(DirectoryNotFoundException ex)
           {
               Console.WriteLine(ex.Message);
           }
           catch(StackOverflowException ex)
           {
               Console.WriteLine(ex.Message);
           }
           catch(FileNotFoundException ex)
           {
               Console.WriteLine(ex.Message);
           }
           catch(UnauthorizedAccessException ex)
           {
               Console.WriteLine(ex.Message);
           }
           catch(NullReferenceException ex)
           {
               Console.WriteLine(ex.Message);
           }
           catch(ArgumentNullException ex)
           {
               Console.WriteLine(ex.Message);
           }
           catch(IndexOutOfRangeException ex)
           {
               Console.WriteLine(ex.Message);
           }

       //     cmdArgConverter.Display();
        }
    }
}
