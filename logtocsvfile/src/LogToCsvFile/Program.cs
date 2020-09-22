using System;

namespace LogToCsvFile
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandArgumentConverter cmdArgConverter = new CommandArgumentConverter();
            cmdArgConverter.CheckArgument(args);
            try{
            if( args.Length == 1 && args != null)
            {
              if(args[0].ToLower() == "help")
                cmdArgConverter.DisplayHelp();
            }
            else if(cmdArgConverter.GetUserLevel().Count == 0 &&  cmdArgConverter.IsSourceFileOrDiecoryValidate())
            {
                cmdArgConverter.SetAllFilePath(cmdArgConverter.GetAllLogFiles());
                if(cmdArgConverter.GetAllFilePath().Length != 0)
                cmdArgConverter.LevelGivenByUser();
            }
            else if(cmdArgConverter.IsSourceFileOrDiecoryValidate() && cmdArgConverter.IsValidateLevels() )
            {    
                cmdArgConverter.SetAllFilePath(cmdArgConverter.GetAllLogFiles());
                if(cmdArgConverter.GetAllFilePath().Length != 0)
                cmdArgConverter.LevelGivenByUser();     
            }
            else{
                Console.WriteLine("\nPlease Give correct Input Level");
                cmdArgConverter.DisplayHelp();
            }
        }
           catch(Exception ex)
           {
               Console.WriteLine(ex.Message);
           }
        }
    }
}
