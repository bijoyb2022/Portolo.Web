using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Portolo.Utility.Configuration;

namespace Portolo.Utility.FileSystem
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        private readonly string rootFolderPrefix;
        private readonly CloudBlobClient client;

        public FileSystemWrapper(IConfiguration config)
        {
            var fileSystemConfig = config.GetTypedSection<FileSystemConfig>();
            this.rootFolderPrefix = fileSystemConfig.RootFolderPrefix;
            this.client = CloudStorageAccount
                .Parse(fileSystemConfig.ConnectionString)
                .CreateCloudBlobClient();
        }

        public Task<bool> ExistsAsync(string rootFolder, string path)
        {
            path = this.MakeValidBlobName(path);
            return this
                .client
                .GetContainerReference(this.GetContainerName(rootFolder))
                .GetBlockBlobReference(path)
                .ExistsAsync();
        }

        public Task UploadAsync(string rootFolder, string path, Stream data)
        {
            path = this.MakeValidBlobName(path);
            return this
                .client
                .GetContainerReference(this.GetContainerName(rootFolder))
                .GetBlockBlobReference(path)
                .UploadFromStreamAsync(data);
        }

        public Task UploadAsync(string rootFolder, string path, byte[] data)
        {
            path = this.MakeValidBlobName(path);
            return this
                .client
                .GetContainerReference(this.GetContainerName(rootFolder))
                .GetBlockBlobReference(path)
                .UploadFromByteArrayAsync(data, 0, data.Length);
        }

        public async Task<byte[]> DownloadFileAsync(string rootFolder, string path)
        {
            path = this.MakeValidBlobName(path);
            using (var memoryStream = new MemoryStream())
            {
                var blockReference = this
                    .client
                    .GetContainerReference(this.GetContainerName(rootFolder))
                    .GetBlockBlobReference(path);

                if (await blockReference.ExistsAsync().ConfigureAwait(false))
                {
                    await blockReference
                        .DownloadToStreamAsync(memoryStream)
                        .ConfigureAwait(false);

                    memoryStream.Position = 0;

                    return memoryStream.ToArray();
                }

                return null;
            }
        }

        public string GetPreSignedUrlEscaped(string rootFolder, string path)
        {
            path = this.MakeValidBlobName(path);            
            CloudBlockBlob blob = this.GetBlob(rootFolder, path);
            string escapedUri = Uri.EscapeUriString(blob.Uri.ToString());
            string blobToken = this.GetSasBlobToken(blob);
            return new Uri(escapedUri + blobToken).OriginalString;
        }

        public async Task DeleteFileAsync(string rootFolder, string path)
        {
            path = this.MakeValidBlobName(path);
            var blockReference = this
                .client
                .GetContainerReference(this.GetContainerName(rootFolder))
                .GetBlockBlobReference(path);

            await blockReference.DeleteIfExistsAsync().ConfigureAwait(false);
        }

        public string FromAzureToBase64(string rootFolder, string path)
        {
            CloudBlockBlob blob = this.GetBlob(rootFolder, path);
            blob.FetchAttributes(); 
            byte[] arr = new byte[blob.Properties.Length];
            blob.DownloadToByteArray(arr, 0);
            var azureBase64 = Convert.ToBase64String(arr);
            return azureBase64;
        }

        private string GetContainerName(string rootFolder)
        {
            if (string.IsNullOrEmpty(this.rootFolderPrefix))
            {
                return rootFolder.ToLower();
            }

            return $"{this.rootFolderPrefix}{rootFolder}".ToLower();
        }

        private CloudBlockBlob GetBlob(string rootFolder, string path)
        {
            var containerReference = this.client.GetContainerReference(this.GetContainerName(rootFolder));
            return containerReference.GetBlockBlobReference(path);
        }

        private string GetSasBlobToken(CloudBlockBlob blob)
        {
            var sasConstraints = new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddYears(1),
                Permissions = SharedAccessBlobPermissions.Read
            };
            return blob.GetSharedAccessSignature(sasConstraints);
        }

        private string MakeValidBlobName(string input)
        {
            if (input.Length > 256)
            {
                input = input.Substring(0, 256);
            }

            return Uri.EscapeUriString(new string(input.Select(c => char.IsControl(c) ? '_' : c).ToArray()));
        }
    }
}
