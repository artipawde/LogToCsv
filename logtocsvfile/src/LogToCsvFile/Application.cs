
namespace LogToCsvFile
{
    public class Application : IApplication
    {
        IUserInput _userInput;
        public Application(IUserInput userInput)
        {
            _userInput = userInput;
        }
        public void Run(string[] args)
        {
            _userInput.AddToLogger(args);
        }
    }
}