using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PetStore.utilities
{
    public class CSharpHelpers
    {
        public int GenerateRandomNumber()
        {
            var random = new Random();
            return random.Next();
        }

        public readonly char[] separator = [' '];
        public string GetLastWord(string text)
        {
            string[] words = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return words.Length > 0 ? words[^1] : string.Empty;  // Using ^1 to get the last element
        }

        public decimal ConvertCurrencyToNumber(string currency)
        {
            return decimal.Parse(currency.Replace(".00", ""), NumberStyles.Currency);
        }

        public IList<string> GetJsonObjectChildrenToStringList(string arrayName, JObject jObject)
        {
            IList<JToken> jSonList = jObject[arrayName].Children().ToList();
            IList<string> list = new List<string>();
            foreach (JToken result in jSonList)
            {
                var searchResult = result.ToObject<string>();
                list.Add(searchResult);
            }
            return list;
        }
    }
}
