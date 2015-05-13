using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebSpellCheck.Core
{
    public class WebSpellChecker
    {
        const string baseUri = "http://dictionary.reference.com/browse/";
        public bool IsMisspelled(string word)
        {
            using (var client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false }))
            {
                var response = client.GetAsync(baseUri + word).Result;
                return !response.IsSuccessStatusCode; // we get a 301 redirect for misspelled words
            }
        }
    }
}