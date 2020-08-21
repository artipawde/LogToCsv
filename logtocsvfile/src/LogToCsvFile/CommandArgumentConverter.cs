using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace LogToCsvFile
{
    public class CommandArgumentConverter
    {
        private string sourcePath;
        private string destinationPath;
        private HashSet<string> actualLevel = new HashSet<string>{ "WARN" , "DEBUG" , "TRACE" , "ERROR" , "EVENT", "INFO"};
        private HashSet<string> userLevel = new HashSet<string>();
        private string[] allFilePath;

        public string GetSourcePath()
        {
            return this.sourcePath;
        }
        public string GetDestinationPath()
        {
            return this.destinationPath;
        }
        public HashSet<string> GetUserLevel()
        {
            return this.userLevel;
        }
        public void SetSourcePath(string path)
        {
            this.sourcePath = path;
        }   
        public void SetDestinationPath(string path)
        {
            this.destinationPath = path;
        }
        public void SetLevel(HashSet<string> list)
        {
            this.userLevel = list;
        }

        public void CheckArgument(string[] args)
        {
            for(int i =0; i < args.Length; i++)
            {
            string extenion =  Path.GetExtension(sourcePath);
            if(args[i].ToLower() == "--log-dir")
            {
                this.sourcePath = args[i + 1];
            }
            else if(args[i].ToLower() == "--csv")
            {
                this.destinationPath = args[i + 1];
            }
            else if(args[i].ToLower() == "--log-level")
            {
                userLevel.Add(args[i + 1].ToUpper());
            }
            }
        }

        public bool IsValidateLevels()
        {
            return userLevel.IsSubsetOf(actualLevel);
        }

        public bool IsSourceFileOrDiecoryValidate () {
            if(Path.HasExtension(this.sourcePath))
                return true;
            return Directory.Exists (this.sourcePath);
        }
        public string[] GetAllLogFiles (){
            if(Path.HasExtension(this.sourcePath))
            {
                if(Path.GetExtension(this.sourcePath) == ".log")
                    return new string[]{sourcePath};
                else
                {
                    Console.WriteLine("Please Give correct Input Log File.");
                    DisplayHelp();
                }
            }
           return Directory.GetFiles (this.sourcePath, "*.log", SearchOption.AllDirectories);
        }

        public void Display()
        {
            Console.WriteLine($"source path {this.sourcePath}");
            Console.WriteLine($"destnation Path {this.destinationPath}");
            foreach(var no in userLevel)
                Console.WriteLine(no);
            Console.WriteLine(IsValidateLevels());
            for(int  i =0 ; i< allFilePath.Length; i++)
                Console.WriteLine($"allLogfiles: = {allFilePath[i]}");
        }

        LoggerConverter lc = new LoggerConverter();
        CsvFile csvfile = new CsvFile();


         public void ConvertToLoggerFile(string[] args)
        {
            if( args.Length == 1 && args != null)
            {
              if(args[0].ToLower() == "help")
                    DisplayHelp();
            }
            else if(userLevel.Count == 0 &&  IsSourceFileOrDiecoryValidate())
            {
                allFilePath = GetAllLogFiles();
                if(allFilePath.Length != 0)
                 LevelGivenByUser();

            }
            else if(IsSourceFileOrDiecoryValidate() && IsValidateLevels() )
            {    
                allFilePath = GetAllLogFiles();
                if(allFilePath.Length != 0)
                  LevelGivenByUser();     
            }
            else{
                Console.WriteLine("\nPlease Give correct Input Level");
                DisplayHelp();
            }
        }
        public void NoLevelGivenByUser()
        {
            foreach(string str in allFilePath)
                lc.ReadDataFromFile(str,this.destinationPath);
        }
        public void LevelGivenByUser()
        {
            foreach(string str in allFilePath)
            {
                lc.SetLevelList(userLevel);
                csvfile.SetLevelList(userLevel);
                lc.ReadDataFromFile(str, this.destinationPath);
            }
        }
        public void DisplayHelp()
        {
            Console.WriteLine("\n  LogParser --log-dir <dir> --log-level <level> --csv <out>");
            Console.WriteLine("  --log-dir      Directory to parse resursively for .log files");
            Console.WriteLine("  --csv          Out file path ");
            Console.WriteLine("  --log-level    INFO|WARN|DEBUG|TRACE|ERROR|EVENT");
        }
    }
}
