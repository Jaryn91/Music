using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
using Musiction.API.Resources;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UrlCombineLib;

namespace Musiction.API.BusinessLogic
{
    public class Account : IAccount
    {
        private readonly string _apiKey;
        private readonly Uri _baseUri = new Uri(MagicString.ZamzarApiUrl);

        public Account(IGetValue valueRetrieval)
        {
            _apiKey = valueRetrieval.Get(KeyConfig.ZamzarKey);
        }

        public int GetRemainingCredits()
        {
            var accountUri = _baseUri.Combine("account");
            var accountInfo = Query(accountUri);
            var account = accountInfo.Result;
            return account.credits_remaining;
        }

        private async Task<AccountInfo> Query(Uri url)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(_apiKey, "") })
            using (HttpClient client = new HttpClient(handler))
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                string json = await content.ReadAsStringAsync();
                var accountInfo = JsonConvert.DeserializeObject<AccountInfo>(json);
                return accountInfo;

            }
        }
    }
}
