using System;
using System.Collections.Generic;

namespace LogToCsvFile
{
    public interface ILoggerConverter
    {
        void AddDataToTheFile(int no, DateTime date, string level, string text, string filepath);
        void AddDataToTheFile(DateTime date, string level, string text, string filepath, HashSet<string> list1);
        void ReadDataFromFile(string sourcePath, string destinationPath);
        void SetLevelList(HashSet<string> l1);
        void WriteDataTotheFileInFormat(string line);
    }
}