using Musiction.API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UrlCombineLib;

namespace Musiction.API.BusinessLogic
{
    public class PptxToJpgConverter
    {
        private Uri baseUri = new Uri("https://sandbox.zamzar.com/v1/");
        private string apiKey = "";
        public string Convert(string sourceFile)
        {
            string targetFormat = "jpg";

            var uploadUri = baseUri.Combine("jobs");
            var jobId = Upload(apiKey, uploadUri, sourceFile, targetFormat).Result;

            var getJobUri = baseUri.Combine("jobs", jobId);

            Job job;
            do
            {
                job = Query(apiKey, getJobUri).Result;
            } while (job.status != "successful");

            var zipFile = job.target_files.Last();

            var getFilebUri = baseUri.Combine("files", zipFile.id.ToString(), "content");
            var folder = Directory.GetCurrentDirectory();
            var localFilename = Path.Combine(folder, zipFile.name);
            Download(apiKey, getFilebUri, localFilename).Wait();
            return localFilename;
        }

        static async Task<string> Upload(string key, Uri url, string sourceFile, string targetFormat)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(key, "") })
            using (HttpClient client = new HttpClient(handler))
            {
                var request = new MultipartFormDataContent();
                request.Add(new StringContent(targetFormat), "target_format");
                request.Add(new StreamContent(File.OpenRead(sourceFile)), "source_file", new FileInfo(sourceFile).Name);
                using (HttpResponseMessage response = await client.PostAsync(url, request).ConfigureAwait(false))
                using (HttpContent content = response.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    var jsonData = (JObject)JsonConvert.DeserializeObject(data);
                    return jsonData["id"].ToString();
                }
            }
        }

        static async Task<Job> Query(string key, Uri url)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(key, "") })
            using (HttpClient client = new HttpClient(handler))
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                string json = await content.ReadAsStringAsync();
                var job = JsonConvert.DeserializeObject<Job>(json);
                return job;
            }
        }

        static async Task<JObject> Download(string key, Uri url, string file)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(key, "") })
            using (HttpClient client = new HttpClient(handler))
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            using (Stream stream = await content.ReadAsStreamAsync())
            using (FileStream writer = File.Create(file))
            {
                stream.CopyTo(writer);
                return null;
            }
        }
    }
}