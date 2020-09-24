using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace LogToCsvFile
{
    public class CommandArgumentConverter : ICommandArgumentConverter
    {
        private string sourcePath;
        private string destinationPath;
        private HashSet<string> actualLevel = new HashSet<string> { "WARN", "DEBUG", "TRACE", "ERROR", "EVENT", "INFO" };
        private HashSet<string> userLevel = new HashSet<string>();
        private string[] allFilePath;
        private HashSet<string> allSourcePath = new HashSet<string>();
        ILoggerConverter _loggerConverter;
        ICsvFile _csvfile;
        public CommandArgumentConverter(ICsvFile csvFile, ILoggerConverter loggerConverter)
        {
            _csvfile = csvFile;
            _loggerConverter = loggerConverter;
        }
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
        public void SetAllSourcePath(string Path)
        {
            allSourcePath.Add(Path);
        }
        public HashSet<string> GetAllSourcePath()
        {
            return this.allSourcePath;
        }
        public void CheckArgument(string[] args)
        {
            try{
            for (int i = 0; i < args.Length; i++)
            {
                string extenion = Path.GetExtension(sourcePath);
                if (args[i].ToLower() == "--log-dir")
                {
                   this.sourcePath = args[i + 1];
                }
                else if (args[i].ToLower() == "--csv")
                {
                    this.destinationPath = args[i + 1];
                }
                else if (args[i].ToLower() == "--log-level")
                {
                    userLevel.Add(args[i + 1].ToUpper());
                }
            }
            }
            catch(Exception){
                Console.WriteLine($"Invalid Input Please Enter valid Input.");
                DisplayHelp();
                Environment.Exit(1);
            }
        }
        public bool IsValidateLevels()
        {
            return userLevel.IsSubsetOf(actualLevel);
        }
        public bool IsSourceFileOrDiecoryValidate()
        {
            if (Path.HasExtension(this.sourcePath))
                return true;
            return Directory.Exists(this.sourcePath);
        }
        public string[] GetAllLogFiles()
        {
            if (Path.HasExtension(this.sourcePath))
            {
                if (Path.GetExtension(this.sourcePath) == ".log")
                    return new string[] { sourcePath };
                else
                {
                    Console.WriteLine("Please Give correct Input Log File.");
                    DisplayHelp();
                }
            }
            return Directory.GetFiles(this.sourcePath, "*.log", SearchOption.AllDirectories);
        }
        
        public void LevelGivenByUser()
        {
            foreach (string str in allFilePath)
            {
                _loggerConverter.SetLevelList(userLevel);
                _csvfile.SetLevelList(userLevel);
                _loggerConverter.ReadDataFromFile(str, this.destinationPath);
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
