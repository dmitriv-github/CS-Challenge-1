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
    class JsonFeed
    {
        protected HttpClient client;

        public JsonFeed(string endpoint)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
        }
    }

    class JokesFeed : JsonFeed
    {
        private static string _url = "https://api.chucknorris.io/jokes/";

        public JokesFeed() : base(_url)
        { }

        public async Task<string[]> GetCategories()
        {
            string categoriesJson = await client.GetStringAsync("categories");
            List<string> categoriesList = JsonConvert.DeserializeObject<List<string>>(categoriesJson);
            return categoriesList.ToArray();
        }

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

    class NamesFeed : JsonFeed {
        private static string _url = "https://uinames.com/api/";
        public NamesFeed() : base(_url)
        { }

        public async Task<(string firstName, string lastName)> GetName()
        {
            string json = await client.GetStringAsync("");
            dynamic result = JsonConvert.DeserializeObject<dynamic>(json);
            return (result.name, result.surname);
        }
    }

}