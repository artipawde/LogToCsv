using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace LogToCsvFile
{
    public interface ICsvFile
    {
        string AddExtension(string destinationPath);
        DateTime GetDate();
        string GetDesc();
        string GetLevel();
        int GetNo();
        Regex GetRegex();
        void SetDate(DateTime date);
        void SetDesc(string descr);
        void SetLevel(string level);
        void SetLevelList(HashSet<string> l1);
        void SetNo(int no1);
        void SetRegex(string str);
        string ToString();
    }
}