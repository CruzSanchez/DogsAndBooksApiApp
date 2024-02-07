using DogsAndBooksApi.CoreLibrary.Helpers;

namespace DogsAndBooksApi.ConsoleUI.ConsoleInteractions
{
    internal static class ConsoleLogging
    {
        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the standard output stream.
        /// Sets the color of the output stream based on the Status code arguments and resets the color to the default after printing the message.
        /// </summary>
        /// <param name="message">The string value to write to the standard output stream</param>
        /// <param name="statusCode">An enum to represent the current status</param>
        internal static void PassMessage(string message, StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(message);
                    Console.ResetColor();
                    break;

                case StatusCode.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(message);
                    Console.ResetColor();
                    break;

                case StatusCode.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Console.ResetColor();
                    break;

                case StatusCode.NoCode:
                    PassMessage(message);
                    break;
            }
        }

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="message">The string value to write to the standard output stream</param>
        internal static void PassMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Supplys a line break to the standard output stream
        /// </summary>
        internal static void NewLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Clears the output stream
        /// </summary>
        internal static void ClearConsole()
        {
            Console.Clear();
        }
    }
}
