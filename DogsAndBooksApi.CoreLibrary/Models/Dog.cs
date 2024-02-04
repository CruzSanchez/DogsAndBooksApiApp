using Newtonsoft.Json;

namespace TeachingAPIDemoConsoleUI
{
    public class Dog : IApiType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Breed { get; set; }
        public List<int> Friends { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
