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
            ConsoleLogging.PassMessage("1.Create \n2.Read \n3.Inspect One \n4.Update \n5.Delete \n6.<= Go Back ");
            return GetKeyDown();
        }

        public static ConsoleKey GetKeyDown()
        {
            return Console.ReadKey(true).Key;
        }
        public static string GetUserResponse()
        {
            return Console.ReadLine();
        }

        public static void CrudMenu<T>(ConsoleKey key) where T : IApiType
        {
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    IApiObjectRepo<T> repo = new ApiObjectRepo<T>(HttpClientFactory.Create());
                    var obj = repo.Create(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint, CreateBook().ToJson());
                    DisplayData<T>(obj);
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    repo = new ApiObjectRepo<T>(HttpClientFactory.Create());
                    var data = repo.GetAll(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint);
                    DisplayData<IList<T>, T>(data);
                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    repo = new ApiObjectRepo<T>(HttpClientFactory.Create());
                    obj = repo.GetById(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint, 1);
                    DisplayData(obj);
                    break;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:

                    break;

                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    break;

                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    break;
            }
        }

        private static void DisplayData<T, U>(T data) where T : IList<U>
                                                      where U : IApiType
        {
            foreach (var item in data)
            {
                Console.WriteLine(item.ToJson());
            }
        }

        private static void DisplayData<T>(T data) where T : IApiType
        {
            Console.WriteLine(data.ToJson());
        }

        private static Book CreateBook()
        {
            var book = new Book();

            ConsoleLogging.PassMessage("Title");
            book.Title = GetUserResponse();

            ConsoleLogging.PassMessage("Description?");
            book.Description = GetUserResponse();

            ConsoleLogging.PassMessage("Author Name?");
            book.Author = GetUserResponse();

            ConsoleLogging.PassMessage("When was this created?\n mm/dd/yyyy");

            DateOnly created;
            while (!DateOnly.TryParse(GetUserResponse(), out created))
            {
                ConsoleLogging.PassMessage("Invalid response, try again! mm/dd/yyyy", StatusCode.Error);
            }

            book.CreatedDate = created;

            ConsoleLogging.PassMessage("Genre?");
            book.Genre = GetUserResponse();

            return book;
        }


    }
}
