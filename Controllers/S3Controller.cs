using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/s3")]
public class S3Controller : ControllerBase
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName = "academiq-videos";

    public S3Controller(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    [HttpPost("obtener-url")]
    public async Task<IActionResult> ObtenerUrlPresignada([FromBody] ObtenerUrlRequest request)
    {
        var key = $"{request.Tipo}/{Guid.NewGuid()}-{request.NombreArchivo}";
        var requestPresign = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddMinutes(15),
            ContentType = request.Tipo == "video" ? "video/mp4" : "image/jpeg"
        };

        var url = _s3Client.GetPreSignedURL(requestPresign);

        return Ok(url);
    }
}

public class ObtenerUrlRequest
{
    public string Tipo { get; set; } // "video" o "miniatura"
    public string NombreArchivo { get; set; }
}
