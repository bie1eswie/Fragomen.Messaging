using Amazon.S3;
using Amazon.S3.Model;
using Fragomen.Messaging.Application.Common.Interfaces;
using Frogomen.Messaging.Domain.Configurations;
using Frogomen.Messaging.Domain.Entities.S3;
using Microsoft.Extensions.Options;

namespace Fragomen.Messaging.Infrastructure.Data
{
    public class S3Repository : IS3Repository
    {
        private readonly AmazonS3Client _client;
        private readonly IOptions<ConfigurationOptions> _options;
        public S3Repository(IOptions<ConfigurationOptions> options)
        {
            _client = new AmazonS3Client();
            _options = options;
        }

        public Task CreateBucketIfItDoesNotExistAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteFileAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FileExistsAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<S3File> ReadFileAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<S3File> ReadFileAsync(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task WriteFileAsync(S3File file,string bucketName)
        {
            var request = new PutObjectRequest();

            request.BucketName = bucketName;
            if (!string.IsNullOrWhiteSpace(file.Path))
            {
                request.Key = CreateKeyFromPath(file.Path);
            }
            request.InputStream = file.Stream;
            request.ContentType = file.ContentType?.Trim();

            if (!string.IsNullOrWhiteSpace(file.Path))
            {
                request.Metadata[IS3Repository.MetadataFilePathKey] = file.Path.Trim();
            }
            request.Metadata[IS3Repository.MetadataContentTypeKey] = request.ContentType?.Trim();

            await _client.PutObjectAsync(request);
        }

        private string CreateKeyFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return path.Trim().TrimStart('/').ToLower();
        }
    }
}
