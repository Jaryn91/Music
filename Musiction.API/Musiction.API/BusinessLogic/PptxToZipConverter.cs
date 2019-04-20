using Musiction.API.IBusinessLogic;
using Musiction.API.Models;
using Musiction.API.Resources;
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
    public class PptxToZipConverter : IConvertPresentation
    {
        private readonly Uri _baseUri;
        private readonly string _apiKey;
        private readonly IFileAndFolderPathsCreator _fileAndFolderPath;

        public PptxToZipConverter(IFileAndFolderPathsCreator fileAndFolderPath, IGetValue valueRetrieval)
        {
            _fileAndFolderPath = fileAndFolderPath;
            _baseUri = new Uri(valueRetrieval.Get(KeyConfig.ZamzarUrl));
            _apiKey = valueRetrieval.Get(KeyConfig.ZamzarKey);
        }
        public string Convert(string sourceFile)
        {
            var targetFormat = "jpg";

            var uploadUri = _baseUri.Combine("jobs");
            var jobId = Upload(uploadUri, sourceFile, targetFormat).Result;

            var zipFile = WaitUntilSuccess(jobId);
            var getFilebUri = _baseUri.Combine("files", zipFile.id.ToString(), "content");
            var localFileName = _fileAndFolderPath.GetPathToZipFiles(zipFile.name);

            Download(getFilebUri, localFileName).Wait();

            return localFileName;
        }

        private TargetFile WaitUntilSuccess(string jobId)
        {
            var getJobUri = _baseUri.Combine("jobs", jobId);

            Job job;
            do
            {
                job = Query(getJobUri).Result;
            } while (job.status != "successful");

            var zipFile = job.target_files.Last();
            return zipFile;
        }

        private async Task<string> Upload(Uri url, string sourceFile, string targetFormat)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(_apiKey, "") })
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

        private async Task<Job> Query(Uri url)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(_apiKey, "") })
            using (HttpClient client = new HttpClient(handler))
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                string json = await content.ReadAsStringAsync();
                var job = JsonConvert.DeserializeObject<Job>(json);
                return job;
            }
        }

        private async Task<JObject> Download(Uri url, string file)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(_apiKey, "") })
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