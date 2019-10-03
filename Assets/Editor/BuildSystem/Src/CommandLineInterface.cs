using System;
namespace editor
{
  public class CommandLineInterface : ICommandLineInterface
  {
    public string[] GetCommandLineArgs()
    {
      return Environment.GetCommandLineArgs();
    }
  }
}
