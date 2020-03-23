﻿using System;
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

        public string[] GetRandomJokes(string firstname, string lastname, string category)
        {
            string url = "jokes/random";

            if (category != null)
            {
                if (url.Contains('?')) url += "&";
                else url += "?";
                
                url += "category=";
                url += category;
            }

            string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

            if (firstname != null && lastname != null)
            {
                int index = joke.IndexOf("Chuck Norris");
                string firstPart = joke.Substring(0, index);
                string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
                joke = firstPart + " " + firstname + " " + lastname + secondPart;
            }

            return new string[] { JsonConvert.DeserializeObject<dynamic>(joke).value };
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