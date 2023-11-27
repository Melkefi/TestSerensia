namespace Test_Serensia.Dto
{
    public class TestRequest
    {
        public string Term { get; set; }
        public int NumberOfSuggestions { get; set; }
        public List<string> Choices { get; set; }
    }
}
