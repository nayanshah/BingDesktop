using System.Web;
using Newtonsoft.Json;

namespace BingDesktop.Models
{
    public class BingArchiveImage
    {
        public string StartDate;
        public string FullStartDate;
        public string EndDate;
        public string Url;
        public string Copyright;
        public string CopyrightLink;
        public string Title;
        public bool Wp;

        [JsonProperty("hsh")]
        public string Hash;

        public string GetDownloadUrl(bool useDirectUrl = false) => 
            useDirectUrl
            ? $"{Constants.BingDotCom}{Url}"
            : $"{Constants.BingDotCom}/hpwp/{Hash}";

        public string ParseFileName()
        {
            var iqs = Url.IndexOf('?');
            var queryParams = HttpUtility.ParseQueryString(Url.Substring(iqs + 1));
            return StartDate + "_" + queryParams["id"];
        }
    }
}
