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
        LoggerConverter lc = new LoggerConverter();
        CsvFile csvfile = new CsvFile();
        public HashSet<string> GetUserLevel()
        {
            return userLevel;
        }
        public string[] GetAllFilePath()
        {
            return allFilePath;
        }
        public void SetAllFilePath(string[] allFilePath)
        {
            this.allFilePath = allFilePath;
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
