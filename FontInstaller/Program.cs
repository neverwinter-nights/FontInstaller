using System;
using System.IO;

namespace FontInstaller
{
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

        static void WaitForEnter()
        {
            // While this application runs in the Administrator mode,
            // it opens a separate console for output. To show the output
            // of this separate console to the user we need to "pause" this
            // separate console.
            Console.WriteLine("Press ENTER to exit ...");
            Console.ReadKey();
        }


        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                LogError("Font file is not specified.");
                ResetConsoleColour();
                WaitForEnter();

                return (int)ExitCode.FontFileIsNotSpecified;
            }

            string fontArg = args[0];

            if (!File.Exists(fontArg))
            {
                LogError("Font file does not exist.");
                ResetConsoleColour();
                WaitForEnter();

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
                WaitForEnter();

                return (int)ExitCode.FontInstallationError;
            }

            return (int)ExitCode.Success;
        }
    }
}
