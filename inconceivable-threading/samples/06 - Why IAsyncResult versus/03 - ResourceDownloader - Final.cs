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
    // there aren't *any* blocking threads here (aside from maybe the `stream.Read` call)
    internal class ResourceDownloader : IResourceDownloader
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
}
