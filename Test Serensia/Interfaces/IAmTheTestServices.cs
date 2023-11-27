using Test_Serensia.Dto;

namespace Test_Serensia.Interfaces
{
    public interface IAmTheTestServices
    {
        /// <summary>
        /// Get the list of terms
        /// </summary>
        /// <param name="testRequest"></param>
        /// <returns>IEnumerable with ordered terms</returns>
        Task<IEnumerable<string>> GetSuggestionAsync(TestRequest testRequest);
    }
}
