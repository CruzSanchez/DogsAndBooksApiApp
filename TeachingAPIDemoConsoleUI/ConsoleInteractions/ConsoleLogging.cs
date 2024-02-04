namespace DogsAndBooksApi.ConsoleUI.ConsoleInteractions
{
    internal static class ConsoleLogging
    {
        public static void PassMessage(string message, StatusCode statusCode)
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

                default:
                    PassMessage(message);
                    break;
            }
        }

        public static void PassMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }
    }
}
