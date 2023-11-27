using Test_Serensia.Dto;
using Test_Serensia.Interfaces;

namespace Test_Serensia.Services
{
    public class AmTheTestService : IAmTheTestServices
    {
        ///<inheritdoc cref="IAmTheTestServices.GetSuggestionAsync(TestRequest)"/>
        public async Task<IEnumerable<string>> GetSuggestionAsync(TestRequest testRequest)
        {
            if (string.IsNullOrEmpty(testRequest.Term) || !testRequest.Choices.Contains(testRequest.Term))
            {
                return new List<string>();
            }

            var suggestions = await Task.Run(() =>
            {
                return testRequest.Choices
                                  .Where(choice => choice.Length >= testRequest.Term.Length)
                                  .Select(choice => new
                                  {
                                      Choice = choice,
                                      Score = GetDifferenceScore(testRequest.Term, choice),
                                      LengthDifference = Math.Abs(testRequest.Term.Length - choice.Length)
                                  })
                                  .OrderBy(x => x.Score)
                                  .ThenBy(x => x.LengthDifference)
                                  .ThenBy(x => x.Choice)
                                  .Take(testRequest.NumberOfSuggestions)
                                  .Select(x => x.Choice)
                                  .ToList();
            });

            return suggestions;
        }

        /// <summary>
        /// Get the difference score between two strings of the same length
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="src"></param>
        /// <returns>int score</returns>
        private static int GetDifferenceScore(string dest, string src)
        {
            int score = int.MaxValue;

            for (int i = 0; i <= src.Length - dest.Length; i++)
            {
                int tempScore = dest.Zip(src.Substring(i, dest.Length), (t, c) => t == c ? 0 : 1).Sum();
                if (tempScore < score)
                {
                    score = tempScore;
                }
            }

            return score;
        }
    }
}
