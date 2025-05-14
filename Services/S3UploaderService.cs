using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace TFG_BACK.Services
{
    public class S3UploaderService : IS3UploaderService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3UploaderService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            _bucketName = "archivos-academiq"; 
        }

        public async Task<string> SubirArchivoAsync(IFormFile archivo, string sufijo)
        {
            if (archivo.Length > 0)
            {
                var fileTransferUtility = new TransferUtility(_s3Client);

                var key = $"{sufijo}/{Path.GetFileName(archivo.FileName)}"; // Genera la clave en el bucket
                using (var stream = archivo.OpenReadStream())
                {
                    var request = new TransferUtilityUploadRequest
                    {
                        InputStream = stream,
                        Key = key,
                        BucketName = _bucketName,
                        ContentType = archivo.ContentType
                        // Se ha eliminado la línea de CannedACL
                    };

                    await fileTransferUtility.UploadAsync(request);
                    return $"https://{_bucketName}.s3.amazonaws.com/{key}";
                }
            }

            return null;
        }
    }
}