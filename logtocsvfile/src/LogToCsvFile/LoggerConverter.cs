using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogToCsvFile
{
    public class LoggerConverter : ILoggerConverter
    {
        private int num = 0;
        private string destWithExt;
        private HashSet<string> levelList = new HashSet<string>();
        ICsvFile _csvfile;
        public LoggerConverter(ICsvFile csvFile)
        {
            _csvfile = csvFile;
        }
        public void ReadDataFromFile(string sourcePath, string destinationPath)
        {
            this.destWithExt = _csvfile.AddExtension(destinationPath);
            List<string> lines = File.ReadAllLines(sourcePath).ToList();

            foreach (string line in lines)
            {
                string strRegex = @"(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])\s(0[1-9]|1[012])[:](0[1-9]|[12345][0-9])[:](0[1-9]|[12345][0-9])\s(INFO|WARN|DEBUG|TRACE|ERROR|EVENT)";
                _csvfile.SetRegex(strRegex);
                WriteDataTotheFileInFormat(line);
            }
        }
        public void WriteDataTotheFileInFormat(string line)
        {
            if (_csvfile.GetRegex().IsMatch(line))
            {
                string[] words = line.Split(" ");
                for (int i = 0; i < words.Length; i++)
                {
                    string[] dateString = words[0].Split('/');
                    string[] timeString = words[1].Split(':');

                    try
                    {
                        DateTime datetime = new DateTime(DateTime.Now.Year, Int32.Parse(dateString[0]), Int32.Parse(dateString[1]), Int32.Parse(timeString[0]), Int32.Parse(timeString[1]), Int32.Parse(timeString[2]));
                        _csvfile.SetDate(datetime);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    _csvfile.SetLevel(words[2]);
                }
                var logText = string.Join(" ", words.Skip(3));
                _csvfile.SetDesc(logText);

                if (levelList.Count > 0)
                {
                    AddDataToTheFile(_csvfile.GetDate(), _csvfile.GetLevel(), _csvfile.GetDesc(), destWithExt, levelList);
                }
                else
                {
                    AddDataToTheFile(_csvfile.GetNo(), _csvfile.GetDate(), _csvfile.GetLevel(), _csvfile.GetDesc(), destWithExt);
                }
            }
        }
        public void AddDataToTheFile(int no, DateTime date, string level, string text, string filepath)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, true))
                {
                    _csvfile.SetNo(++no);
                    if (new FileInfo(filepath).Length == 0)
                    {
                        string header = "No|  Date  | Time    |Level  |      Text      |\n";
                        file.WriteLine(header);
                    }
                    file.WriteLine(no + "|" + date + "|" + level + "|" + text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddDataToTheFile(DateTime date, string level, string text, string filepath, HashSet<string> list1)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, true))
                {
                    foreach (string str in list1)
                    {
                        if (level == str)
                        {
                            _csvfile.SetNo(++num);
                            if (new FileInfo(filepath).Length == 0)
                            {
                                string header = "No|  Date  | Time    |Level  |      Text      |\n";
                                file.WriteLine(header);
                            }
                            file.WriteLine(num + "|" + date + "|" + level + "|" + text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SetLevelList(HashSet<string> l1)
        {
            if (l1.Count > 0)
                this.levelList = l1;
        }
    }
}