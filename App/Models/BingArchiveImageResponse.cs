using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BingDesktop.Models
{
    public class BingArchiveImageResponse
    {
        public IList<BingArchiveImage> Images;

        public static BingArchiveImageResponse Parse(string path)
        {
            return JsonConvert.DeserializeObject<BingArchiveImageResponse>(File.ReadAllText(path));
        }

        public void Serialize(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
    }
}
