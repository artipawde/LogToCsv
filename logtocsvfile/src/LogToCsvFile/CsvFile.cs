using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;


namespace LogToCsvFile
{
    public class CsvFile : ICsvFile
    {
        private int no;
        private String level;
        private DateTime date;
        private String descr;
        private Regex regex;
        private HashSet<string> levelList = new HashSet<string>();

        public int GetNo()
        {
            return no;
        }
        public string GetLevel()
        {
            return this.level;
        }
        public Regex GetRegex()
        {
            return this.regex;
        }
        public string GetDesc()
        {
            return this.descr;
        }
        public DateTime GetDate()
        {
            return this.date;
        }
        public void SetNo(int no1)
        {
            no = no1;
        }
        public void SetLevel(string level)
        {
            this.level = level;
        }
        public void SetRegex(string str)
        {
            this.regex = new Regex(str);
        }
        public void SetDesc(string descr)
        {
            this.descr = descr;
        }
        public void SetDate(DateTime date)
        {
            this.date = date;
        }
        public void SetLevelList(HashSet<string> l1)
        {
            if (l1.Count > 0)
                this.levelList = l1;
        }
        public override string ToString()
        {
            string str = " " + no + date + level + descr + levelList.ToString();
            return str;
        }
        public string AddExtension(string destinationPath)
        {
            string str = destinationPath;
            if (!Path.HasExtension(destinationPath))
            {
                if (Path.EndsInDirectorySeparator(destinationPath))
                {
                    str += "log.csv";
                }
                else
                {
                    str += ".csv";
                }
            }
            else
            {
                string getExtension = Path.GetExtension(destinationPath);
                if (getExtension != ".csv")
                {
                    str = Path.ChangeExtension(destinationPath, ".csv");
                }
            }
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(str));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return str;
        }
    }
}