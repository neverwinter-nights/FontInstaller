using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontInstaller
{
    enum ExitCode : int
    {
        Success = 0,
        FontFileIsNotSpecified = 1,
        FontFileDoesNotExist = 2,
        FontInstallationError = 3
    }

    internal class Program
    {
        static ConsoleColor ConsoleNormalColour = Console.ForegroundColor;
        static ConsoleColor ConsoleErrorColour = ConsoleColor.Red;


        static void ResetConsoleColour()
        {
            Console.ForegroundColor = ConsoleNormalColour;
        }


        static void LogMessage(string Message)
        {
            Console.ForegroundColor = ConsoleNormalColour;
            Console.WriteLine(Message);
        }


        static void LogError(string Error)
        {
            Console.ForegroundColor = ConsoleErrorColour;
            Console.WriteLine(Error);
        }


        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                LogError("Font file is not specified.");
                ResetConsoleColour();

                return (int)ExitCode.FontFileIsNotSpecified;
            }

            string fontArg = args[0];

            if (!File.Exists(fontArg))
            {
                LogError("Font file does not exist.");
                ResetConsoleColour();

                return (int)ExitCode.FontFileDoesNotExist;
            }

            try
            {
                Font.Install(fontArg);
            }
            catch (Exception e)
            {
                LogMessage("Oops...");
                LogError(e.ToString());
                LogMessage("Make sure that you know what you are doing.");
                ResetConsoleColour();

                return (int)ExitCode.FontInstallationError;
            }

            return (int)ExitCode.Success;
        }
    }
}
