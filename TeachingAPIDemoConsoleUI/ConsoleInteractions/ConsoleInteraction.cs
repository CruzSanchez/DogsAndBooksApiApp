using DogsAndBooksApi.CoreLibrary.Factories;
using DogsAndBooksApi.CoreLibrary.Helpers;
using DogsAndBooksApi.CoreLibrary.Models;
using DogsAndBooksApi.InfrastructureLibrary.Factories;
using DogsAndBooksApi.InfrastructureLibrary.Infrastructure;

namespace DogsAndBooksApi.ConsoleUI.ConsoleInteractions
{
    /// <summary>
    /// Static class that encapsulates functionality for interacting with the user in the console application.
    /// </summary>
    internal static class ConsoleInteraction
    {
        private static ConsoleKey mainMenuKey = ConsoleKey.None;
        private static ConsoleKey menuTwoKey = ConsoleKey.None;

        /// <summary>
        /// Root of the application. This method will run continuously until the application is closed. Choice 3 will also close app. 
        /// </summary>
        internal static void MainMenu()
        {
            while (true)
            {
                ConsoleLogging.PassMessage("1.Dogs 2.Books 3.Exit");
                mainMenuKey = GetKeyDown();
                SwitchMenus();
            }
        }

        /// <summary>
        /// Gets data from the user via the console
        /// </summary>
        /// <returns>The data the user types in as a string</returns>
        private static string GetUserResponse()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Gets the users choice as a Console Key. ex: user presses the number on on the number pad, this will return the Key pressed
        /// </summary>
        /// <returns>The key pressed as a Console Key enum</returns>
        private static ConsoleKey GetKeyDown()
        {
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Switches between the Main Menu and Menu number two to display CRUD commands. 
        /// This will also set the endpoint of the api URL to dog or book
        /// </summary>
        private static void SwitchMenus()
        {
            switch (mainMenuKey)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    ApiPropertyHandler.UserEndpoint = "dog";
                    ConsoleLogging.ClearConsole();
                    CrudMenu();
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ApiPropertyHandler.UserEndpoint = "book";
                    ConsoleLogging.ClearConsole();
                    CrudMenu();
                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    ConsoleLogging.ClearConsole();
                    Environment.Exit(0);
                    break;
            }
        }

        /// <summary>
        /// Displays the Crud menu options available to the user
        /// </summary>
        private static void CrudMenu()
        {
            ConsoleLogging.PassMessage("1.Create \n2.Read \n3.Inspect One \n4.Update \n5.Delete \n6.<= Go Back ");
            menuTwoKey = GetKeyDown();
            SetCrudMenuType();
        }

        /// <summary>
        /// The main purpose of this method is to pass the correct type to the CrudMenuSelection based off the endpoint
        /// of the ApiPropertyHandler class.<para/>
        /// Since the CrudMenuSelection method is generic the proper type must be passed down the chain
        /// </summary>
        /// <returns>A boolean to see if the CrudMenu needs to remain on the screen</returns>
        private static bool SetCrudMenuType()
        {
            if (ApiPropertyHandler.UserEndpoint == "dog")
            {
                return CrudMenuSelection<Dog>();
            }
            else
            {
                return CrudMenuSelection<Book>();
            }
        }

        /// <summary>
        /// This method kicks off the correct CRUD command that the user selects
        /// </summary>
        /// <typeparam name="T">The Type of object CRUD needs to be performed on. Dog or Book</typeparam>
        /// <returns>A boolean value to see if the menu should still display on the screen</returns>
        private static bool CrudMenuSelection<T>() where T : IApiType
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

                        obj = repo.Create(ApiPropertyHandler.UserEndpoint, ApiTypeFactory.CreateDogOrBook(typeof(T).Name,
                                                                ConsoleLogging.PassMessage, GetUserResponse, ParseFriends).ToJson());
                        DisplayData<T>(obj);

                        CrudMenu();
                        ConsoleLogging.ClearConsole();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        list = repo.GetAll(ApiPropertyHandler.UserEndpoint);
                        DisplayData<IList<T>, T>(list);

                        CrudMenu();
                        ConsoleLogging.ClearConsole();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        list = repo.GetAll(ApiPropertyHandler.UserEndpoint);
                        DisplayData<IList<T>, T>(list);

                        obj = repo.GetById(ApiPropertyHandler.UserEndpoint, GetIdFromUser());
                        DisplayData(obj);

                        CrudMenu();
                        ConsoleLogging.ClearConsole();
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        list = repo.GetAll(ApiPropertyHandler.UserEndpoint);
                        DisplayData<IList<T>, T>(list);

                        _ = repo.UpdateById(ApiPropertyHandler.UserEndpoint, GetIdFromUser(),
                            ApiTypeFactory.CreateDogOrBook(typeof(T).Name, ConsoleLogging.PassMessage, GetUserResponse, ParseFriends).ToJson());

                        CrudMenu();
                        ConsoleLogging.ClearConsole();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        repo = new ApiObjectRepo<T>(HttpClientFactory.Create());

                        list = repo.GetAll(ApiPropertyHandler.UserEndpoint);
                        DisplayData<IList<T>, T>(list);

                        _ = repo.DeleteById(ApiPropertyHandler.UserEndpoint, GetIdFromUser());

                        CrudMenu();
                        ConsoleLogging.ClearConsole();
                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        goBack = true;
                        cont = false;
                        ConsoleLogging.ClearConsole();
                        break;
                }

            } while (cont);

            return goBack;
        }

        /// <summary>
        /// Parses a csv string of friend IDs that a user types into the console
        /// </summary>
        /// <param name="friends">A comma separated list of IDs that represent friends of an instance of a dog</param>
        /// <returns>A list whose elements represent friend IDs</returns>
        private static List<int> ParseFriends(string friends)
        {
            return friends.Split(",").Select(x => int.Parse(x)).ToList();
        }

        /// <summary>
        /// Iterates through a collection of IApiType. 
        /// </summary>
        /// <typeparam name="T">The collection that contains the U type data</typeparam>
        /// <typeparam name="U">The type in the collection</typeparam>
        /// <param name="data">The data to iterate over and display to the user on the Console</param>
        private static void DisplayData<T, U>(T data) where T : IList<U>
                                                      where U : IApiType
        {
            foreach (var item in data)
            {
                ConsoleLogging.PassMessage(item.ToJson());
            }
        }

        /// <summary>
        /// Displays the data of the object passed in to the Console
        /// </summary>
        /// <typeparam name="T">An IApiType</typeparam>
        /// <param name="data">The data to display to the user</param>
        private static void DisplayData<T>(T data) where T : IApiType
        {
            ConsoleLogging.PassMessage(data.ToJson());
        }

        /// <summary>
        /// Requests the ID of a json object to do work on
        /// </summary>
        /// <returns>The ID of the json object, as an <c>int</c>; that the user wants to manipulate</returns>
        private static int GetIdFromUser()
        {
            ConsoleLogging.PassMessage("What is the Id?");
            string response = GetUserResponse();

            int id = ParseInteger(response);

            return id;
        }

        /// <summary>
        /// Parses a <c>string</c> to an <c>int</c>. If an invalid string is supplied, the method will request further attempts until a 
        /// valid string to parse is supplied
        /// </summary>
        /// <param name="numberToParse">The <c>string</c> to parse</param>
        /// <returns>Returns <paramref name="numberToParse"/> as it's numerical equivalent</returns>
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
