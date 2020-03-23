using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    /// <summary> Class for getting JSON data from API calls. </summary>
    class JsonFeed
    {
        protected HttpClient client;

        public JsonFeed(string endpoint)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
        }
    }

    /// <summary>Specialty class for connecting to the Chuck Norris jokes API.</summary>
    class JokesFeed : JsonFeed
    {
        private static string _url = "https://api.chucknorris.io/jokes/";

        public JokesFeed() : base(_url)
        { }

        /// <summary> Performs a GET request to fetch all the possible Chuck Norris joke categories from the API. </summary>
        public async Task<string[]> GetCategories()
        {
            string categoriesJson = await client.GetStringAsync("categories");
            List<string> categoriesList = JsonConvert.DeserializeObject<List<string>>(categoriesJson);
            return categoriesList.ToArray();
        }

        /// <summary> Performs a numJokes GET requests to get random Chuck Norris jokes with the specified category,
        /// optionally replacing Chuck Norris's name with the provided firstname and lastname </summary>
        public async Task<string[]> GetRandomJokes(int numJokes, string category, string firstname, string lastname)
        {
            string url = "random";
            string[] jokes = new string[numJokes];

            if (category != null)
            {
                var query = HttpUtility.ParseQueryString(String.Empty);
                query["category"] = category;
                url += '?' + query.ToString();
            }

            for (int i = 0; i < numJokes; i++) {
                string jokesJson = await client.GetStringAsync(url);
                string joke = JsonConvert.DeserializeObject<dynamic>(jokesJson).value;

                if (firstname != null && lastname != null)
                {
                    joke = joke.Replace("Chuck", firstname).Replace("Norris", lastname);
                }

                jokes[i] = joke;
            }

            return jokes;
        }
    }

    ///<summary>Specialty class for getting random names from the UINames API.</summary>
    class NamesFeed : JsonFeed {
        private static string _url = "https://uinames.com/api/";
        public NamesFeed() : base(_url)
        { }

        /// Performs a GET request to get a random name, and returns it as a tuple of firstName and lastName </summary>
        public async Task<(string firstName, string lastName)> GetName()
        {
            string json = await client.GetStringAsync("");
            dynamic result = JsonConvert.DeserializeObject<dynamic>(json);
            return (result.name, result.surname);
        }
    }

}