using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Portolo.Utility.FileSystem
{
    public interface IFileSystemWrapper
    {
        Task<bool> ExistsAsync(string rootFolder, string path);
        Task UploadAsync(string rootFolder, string path, Stream data);
        Task UploadAsync(string rootFolder, string path, byte[] data);
        Task<byte[]> DownloadFileAsync(string rootFolder, string path);
        string GetPreSignedUrlEscaped(string rootFolder, string path);
        Task DeleteFileAsync(string rootFolder, string path);
        string FromAzureToBase64(string rootFolder, string path);
    }
}
