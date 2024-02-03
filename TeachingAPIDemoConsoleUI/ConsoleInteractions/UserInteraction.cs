using TeachingAPIDemoConsoleUI.Factory;
using TeachingAPIDemoConsoleUI.Infrastructure;

namespace TeachingAPIDemoConsoleUI.ConsoleInteractions
{
    internal static class UserInteraction
    {
        public static ConsoleKey MainMenu()
        {
            ConsoleLogging.PassMessage("1.Dogs 2.Books 3.Exit");
            return GetKeyDown();
        }

        public static ConsoleKey MenuLevelTwo()
        {
            ConsoleLogging.PassMessage("1.Create \n2.Read \n3.Update \n4.Delete \n5.<= Go Back ");
            return GetKeyDown();
        }

        public static ConsoleKey GetKeyDown()
        {
            return Console.ReadKey(true).Key;
        }

        private static void DisplayData<T, U>(T data) where T : IList<U>
                                                      where U : IApiType
        {
            foreach (var item in data)
            {
                Console.WriteLine(item.ToJson());
            }
        }

        internal static void BookMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    BookRepo bookRepo = new BookRepo(HttpClientFactory.Create());

                    var books = bookRepo.GetAllBooks(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint);

                    DisplayData<IList<Book>, Book>(books);
                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    break;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:

                    break;

                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    break;
            }
        }

        internal static void DogMenu(ConsoleKey key, IList<Dog> dogs)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    break;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:

                    break;

                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    break;
            }
        }
    }
}
