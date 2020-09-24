using System;

namespace LogToCsvFile
{
    public class UserInput : IUserInput
    {
        ICommandArgumentConverter _commandArgumentConverter;
        public UserInput(ICommandArgumentConverter commandArgumentConverter)
        {
            _commandArgumentConverter = commandArgumentConverter;
        }
        public void AddToLogger(string[] args)
        {
            _commandArgumentConverter.CheckArgument(args);
            try
            {
                if (args.Length == 1 && args != null)
                {
                    if (args[0].ToLower() == "help")
                        _commandArgumentConverter.DisplayHelp();
                }
                else if (_commandArgumentConverter.GetUserLevel().Count == 0 && _commandArgumentConverter.IsSourceFileOrDiecoryValidate())
                {
                    _commandArgumentConverter.SetAllFilePath(_commandArgumentConverter.GetAllLogFiles());
                    if (_commandArgumentConverter.GetAllFilePath().Length != 0)
                        _commandArgumentConverter.LevelGivenByUser();
                }
                else if (_commandArgumentConverter.IsSourceFileOrDiecoryValidate() && _commandArgumentConverter.IsValidateLevels())
                {
                    _commandArgumentConverter.SetAllFilePath(_commandArgumentConverter.GetAllLogFiles());
                    if (_commandArgumentConverter.GetAllFilePath().Length != 0)
                        _commandArgumentConverter.LevelGivenByUser();
                }
                else
                {
                    Console.WriteLine("\nPlease Give correct Input Level");
                    _commandArgumentConverter.DisplayHelp();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}