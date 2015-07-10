using System;
using System.IO;
using System.Net;

namespace IAsyncResultExample
{
    // this one solves the issue for us by using APM, but it is more complex
    //
    // we have a Begin* request that starts the download and then promptly returns, but with a callback
    // we then have an End* request that gets spun up when the download is completed
    //
    // there aren't any major blocking threads here (aside from the `stream.Read` call)
    //
    // NOTE: ONE BIG CAVEAT... THIS ISN'T EVEN COMPLETE, IN ACTUALITY, YOU'D HAVE TO ALSO IMPLEMENT
    // IHttpAsyncHandler (versus IHttpHandler)
    internal class ResourceDownloader03 : IResourceDownloader
    {
        public IAsyncResult BeginDownload(string resourcePath, AsyncCallback callback, AsyncData asyncData)
        {
            var downloader = WebRequest.Create(resourcePath);
            asyncData.Request = downloader;
            asyncData.Downloader = this;
            return downloader.BeginGetResponse(callback, asyncData);
        }

        public byte[] EndDownload(IAsyncResult iar)
        {
            var downloader = ((AsyncData) iar.AsyncState).Request;
            if (downloader == null) return new byte[] {};

            var buffer = new byte[16*1024];
            using (var response = downloader.EndGetResponse(iar))
            using (var stream = response.GetResponseStream())
            using (var ms = new MemoryStream())
            {
                if (stream == null) return new byte[] {};

                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }

    internal interface IResourceDownloader
    {
        IAsyncResult BeginDownload(string resourcePath, AsyncCallback callback, AsyncData asyncData);
        byte[] EndDownload(IAsyncResult iar);
    }

    internal class AsyncData
    {
        public WebRequest Request { get; set; }
        public IResourceDownloader Downloader { get; set; }
    }
}
