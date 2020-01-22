using System;
using System.IO;
using System.Threading.Tasks;
using BingDesktop.Utilities;

namespace BingDesktop
{
    class BingDesktopApp
    {
        public enum Command
        {
            Latest,
            Download,
            SetWallpaper,
            Install,
        }

        public async Task Run(Command command, string[] args)
        {
            switch (command)
            {
                case Command.Download:
                    await DownloadLatestImageAsync();
                    break;

                case Command.SetWallpaper:
                    if (args.Length != 1)
                    {
                        throw new ArgumentException("Image path not specified");
                    }

                    UpdateWallpaper(args[0]);
                    break;

                case Command.Latest:
                    var image = await DownloadLatestImageAsync();
                    UpdateWallpaper(image);
                    break;

                case Command.Install:
                default:
                    throw new NotImplementedException();
            }
        }

        private void UpdateWallpaper(string image)
        {
            if (!File.Exists(image))
            {
                throw new ArgumentException($"File not found: {image}");
            }

            Wallpaper.Set(image);
            Console.WriteLine("Successfully updated wallpaper");
        }

        private async Task<string> DownloadLatestImageAsync()
        {
            using (var setup = new AppSetup())
            {
                var mgr = new DownloadManager(setup);
                var image = await mgr.DownloadLatestImageAsync();
                Console.WriteLine("LatestImage: " + image);
                return image;
            }
        }
    }
}
