using Frogomen.Messaging.Domain.Entities.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.Common.Interfaces
{
    public interface IS3Repository
    {
        public const string MetadataFilePathKey = "FilePath";
        public const string MetadataContentTypeKey = "ContentType";
        public Task CreateBucketIfItDoesNotExistAsync();
        public Task DeleteFileAsync(string path);
        public Task<bool> FileExistsAsync(string path);
        public Task<S3File> ReadFileAsync(string path);
        public Task<S3File> ReadFileAsync(string path, string fileName);
        public Task WriteFileAsync(S3File file, string bucketName);
    }
}
