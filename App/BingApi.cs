using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BingDesktop.Models;
using BingDesktop.Utilities;

namespace BingDesktop
{
    public class BingApi
    {
        public const string DefaultMarket = "en-US";
        public string ApiEndpoint => $"{Constants.BingDotCom}{Constants.BingHPImageArchiveApiBase}";

        public async Task<BingArchiveImageResponse> GetImageArchiveAsync(string market = DefaultMarket, int count = 1)
        {
            var uri = new Uri($"{ApiEndpoint}&n={count}&mkt={market}");
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStreamAsync(uri);
                return response.ParseJson<BingArchiveImageResponse>();
            }
        }
    }
}
