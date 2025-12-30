using System;
using System.IO;
using System.Net;

namespace IAsyncResultExample
{
    // does this version fix the prior version? we've got a thread now...
    //
    // unfortunately, it still blocks a thread and uses up an ASP.NET thread
    //
    // in fact, we're now using *two* threads whereas before, we only had one
    internal class ResourceDownloader : IResourceDownloader
    {
        public byte[] Download(string resourcePath)
        {
            Task.WaitAll(Task.Run(() => {
                using (var client = new WebClient())
                {
                    return client.DownloadData(resourcePath);
                }
            }));
        }
    }
}
