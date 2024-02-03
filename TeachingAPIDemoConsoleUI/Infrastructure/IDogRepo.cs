namespace TeachingAPIDemoConsoleUI.Infrastructure
{
    internal interface IDogRepo
    {
        public IList<Dog> GetAllDogs();
        public Dog GetDogById(int id);
        public Dog GetDogByName(string name);
        public Dog GetDogByOwner(string owner);
    }
}
