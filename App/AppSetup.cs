using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingDesktop
{
    public class AppSetup : IDisposable
    {
        public readonly string AppFolder = Path.Combine(Path.GetTempPath(), "BingDesktop");
        public string ImagesFolder => Path.Combine(AppFolder, "images");
        public string ResponseFolder => Path.Combine(AppFolder, "responses");
        public string TrackerFile => Path.Combine(AppFolder, "v1.tracker");

        public AppSetup()
        {
            if (!File.Exists(TrackerFile))
            {
                // first run, create folders
                Directory.CreateDirectory(ImagesFolder);
                Directory.CreateDirectory(ResponseFolder);
            }
        }

        private void UpdateTrackerFile()
        {
            File.WriteAllText(TrackerFile, DateTime.Now.ToString());
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    UpdateTrackerFile();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
