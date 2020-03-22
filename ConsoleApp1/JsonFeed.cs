using System;
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
        HttpClient client;

        public JsonFeed(string endpoint, int results)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
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

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
        public dynamic Getnames()
        {
            var result = client.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<dynamic>(result);
        }

        public string[] GetCategories()
        {
            return new string[] { Task.FromResult(client.GetStringAsync("categories").Result).Result };
        }
    }
}
