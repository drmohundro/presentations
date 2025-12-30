using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace IAsyncResultExample02
{
    // does this version fix the prior version? we've got a thread now...
    //
    // unfortunately, it still blocks a thread and uses up an ASP.NET thread
    //
    // in fact, we're now using *two* threads whereas before, we only had one
    internal class ResourceDownloader02 : IResourceDownloader
    {
        public byte[] Download(string resourcePath)
        {
            byte[] data = {};
            Task.WaitAll(Task.Run(() => {
                using (var client = new WebClient())
                {
                    data = client.DownloadData(resourcePath);
                }
            }));
            return data;
        }
    }

    internal interface IResourceDownloader
    {
        byte[] Download(string resourcePath);
    }
}
