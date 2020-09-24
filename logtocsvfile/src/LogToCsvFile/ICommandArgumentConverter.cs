using System.Collections.Generic;


namespace LogToCsvFile
{
    public interface ICommandArgumentConverter
    {
        void CheckArgument(string[] args);
        void DisplayHelp();
        string[] GetAllFilePath();
        string[] GetAllLogFiles();
        HashSet<string> GetUserLevel();
        bool IsSourceFileOrDiecoryValidate();
        bool IsValidateLevels();
        void LevelGivenByUser();
        void SetAllFilePath(string[] allFilePath);
    }
}
