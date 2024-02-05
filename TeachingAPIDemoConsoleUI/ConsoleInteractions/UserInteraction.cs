using DogsAndBooksApi.CoreLibrary.Factories;
using DogsAndBooksApi.CoreLibrary.Helpers;
using DogsAndBooksApi.CoreLibrary.Models;
using DogsAndBooksApi.InfrastructureLibrary.Factories;
using DogsAndBooksApi.InfrastructureLibrary.Infrastructure;

namespace DogsAndBooksApi.ConsoleUI.ConsoleInteractions
{
    internal static class UserInteraction
    {
        private static ConsoleKey mainMenuKey = ConsoleKey.None;
        private static ConsoleKey menuTwoKey = ConsoleKey.None;

        public static void MainMenu()
        {
            while (true)
            {
                ConsoleLogging.PassMessage("1.Dogs 2.Books 3.Exit");
                mainMenuKey = GetKeyDown();
                SwitchMenus();
            }
        }

        private static string GetUserResponse()
        {
            return Console.ReadLine();
        }

        private static ConsoleKey GetKeyDown()
        {
            return Console.ReadKey(true).Key;
        }

        private static void SwitchMenus()
        {
            switch (mainMenuKey)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    ApiPropertyHandler.UserEndpoint = "dog";
                    ConsoleLogging.ClearConsole();
                    MenuLevelTwo();
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ApiPropertyHandler.UserEndpoint = "book";
                    ConsoleLogging.ClearConsole();
                    MenuLevelTwo();
                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    ConsoleLogging.ClearConsole();
                    Environment.Exit(0);
                    break;
            }
        }

        private static void MenuLevelTwo()
        {
            ConsoleLogging.PassMessage("1.Create \n2.Read \n3.Inspect One \n4.Update \n5.Delete \n6.<= Go Back ");
            menuTwoKey = GetKeyDown();
            GoToCrudMenu();
        }

        private static bool GoToCrudMenu()
        {
            if (ApiPropertyHandler.UserEndpoint == "dog")
            {
                return CrudMenu<Dog>();
            }
            else
            {
                return CrudMenu<Book>();
            }
        }

        private static bool CrudMenu<T>() where T : IApiType
        {
            bool goBack = false;
            bool cont = true;

            do
            {
                IApiObjectRepo<T> repo;
                T obj;
                IList<T> list;

                switch (menuTwoKey)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        obj = repo.Create(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint,
                            ApiTypeFactory.CreateDogOrBook(typeof(T).Name, ConsoleLogging.PassMessage, GetUserResponse, ParseFriends).ToJson());
                        DisplayData<T>(obj);

                        MenuLevelTwo();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        list = repo.GetAll(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint);
                        DisplayData<IList<T>, T>(list);

                        MenuLevelTwo();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        list = repo.GetAll(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint);
                        DisplayData<IList<T>, T>(list);

                        obj = repo.GetById(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint, GetIdFromUser());
                        DisplayData(obj);

                        MenuLevelTwo();
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        list = repo.GetAll(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint);
                        DisplayData<IList<T>, T>(list);

                        _ = repo.UpdateById(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint, GetIdFromUser(),
                            ApiTypeFactory.CreateDogOrBook(typeof(T).Name, ConsoleLogging.PassMessage, GetUserResponse, ParseFriends).ToJson());

                        MenuLevelTwo();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        list = repo.GetAll(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint);
                        DisplayData<IList<T>, T>(list);

                        _ = repo.DeleteById(ApiPropertyHandler.BASE_URL, ApiPropertyHandler.UserEndpoint, GetIdFromUser());

                        MenuLevelTwo();
                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        goBack = true;
                        cont = false;
                        break;
                }

            } while (cont);

            return goBack;
        }


        private static List<int> ParseFriends(string friends)
        {
            return friends.Split(",").Select(x => int.Parse(x)).ToList();
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


        private static int GetIdFromUser()
        {
            ConsoleLogging.PassMessage("What is the Id?");
            string response = GetUserResponse();

            int id = ParseInteger(response);

            return id;
        }

        private static int ParseInteger(string numberToParse)
        {
            int id;
            while (!int.TryParse(numberToParse, out id))
            {
                ConsoleLogging.PassMessage("Invalid response, please enter a valid number! ex: 1", StatusCode.Error);
                numberToParse = GetUserResponse();
            }

            return id;
        }
    }
}
