using System.IO;
using Newtonsoft.Json;

namespace BingDesktop.Utilities
{
    internal static class StreamExtensions
    {
        public static T ParseJson<T>(this Stream stream) 
        {
            JsonSerializer serializer = new JsonSerializer();

            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
