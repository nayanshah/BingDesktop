using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BingDesktop.Models;

namespace BingDesktop
{
    public class DownloadManager
    {
        private AppSetup Setup { get; }

        public DownloadManager(AppSetup setup)
        {
            Setup = setup;
        }

        public async Task<string> DownloadLatestImageAsync()
        {
            var image = await DownloadLatestArchiveImageAsync();
            var imagePath = Path.Combine(Setup.ImagesFolder, image.ParseFileName());

            if (File.Exists(imagePath))
            {
                // image previously downloaded
                return imagePath;
            }

            using (var webClient = new WebClient())
            {
                try
                {
                    await webClient.DownloadFileTaskAsync(image.GetDownloadUrl(true), imagePath);
                }
                catch
                {
                    if (File.Exists(imagePath))
                    {
                        // delete potentially empty/corrupt file
                        File.Delete(imagePath);
                    }

                    throw;
                }
            }

            return imagePath;
        }

        private async Task<BingArchiveImage> DownloadLatestArchiveImageAsync()
        {
            var responseFile = Path.Combine(Setup.ResponseFolder, DateTime.Now.ToString("yyyyMMdd") + ".json");
            if (File.Exists(responseFile))
            {
                return BingArchiveImageResponse.Parse(responseFile).Images[0];
            }

            var bingApi = new BingApi();
            var archive = await bingApi.GetImageArchiveAsync();
            archive.Serialize(responseFile);

            return archive.Images[0];
        }
    }
}
