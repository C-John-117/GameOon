using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Game_On.Models
{
    internal class apiRecuperateur
    {
        public async Task<Sudoku?> GetApiData(string difficulte)
        {
            using HttpClient client = new HttpClient();
            string url = "https://youdosudoku.com/api/";

            var postData = new { difficulty = difficulte.ToLower(), solution = true, array = false };
            string jsonContent = JsonConvert.SerializeObject(postData);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseBody}");

                var apiResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

                Sudoku sudoku = new Sudoku
                {
                    Puzzle = apiResponse.puzzle,
                    Solution = apiResponse.solution,
                    Difficulte = difficulte,
                    Date = DateTime.Today
                };

                return sudoku;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request Error: {e.Message}");
                return null;
            }
        }
    }
}
