using System.Text.Json;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace MyMockAPI.Controllers
{
    [Route("api")]
    public class HomeController : Controller
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(string content, string contentType)
        {
            var name = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(content))
            {
                content = string.Empty;
            }
            
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "text/plain";
            }

            var blobServiceClient = new BlobServiceClient(
                "");

            var containerClient = blobServiceClient.GetBlobContainerClient("mymockapi");

            var mockData = new MockData { Content = content, ContentType = contentType };

            var json = JsonSerializer.Serialize(mockData);

            var blobClient = containerClient.GetBlobClient($"{name}.json");

            await blobClient.UploadAsync(BinaryData.FromString(json));

            return this.Ok(name);
        }

        [Route("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var blobServiceClient = new BlobServiceClient(
                "");

            var containerClient = blobServiceClient.GetBlobContainerClient("mymockapi");

            var file = $"{name}.json";

            var blobClient = containerClient.GetBlobClient(file);

            if (!await blobClient.ExistsAsync())
            {
                return this.NotFound();
            }

            var result = await blobClient.DownloadContentAsync();

            var mockData = result.Value.Content.ToObjectFromJson<MockData>();

            return this.Content(mockData.Content, mockData.ContentType);
        }
    }
}
