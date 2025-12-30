using System;
using System.IO;
using System.Net;

namespace IAsyncResultExample
{
    // no threading at all here...
    // 
    // what happens to the web request while it waits for the other server to serve the image?
    // it just hangs and blocks a thread that IIS could be using instead
    internal class ResourceDownloader : IResourceDownloader
    {
        public byte[] Download(string resourcePath)
        {
            using (var client = new WebClient())
            {
                return client.DownloadData(resourcePath);
            }
        }
    }
}
