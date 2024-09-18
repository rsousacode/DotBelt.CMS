using Microsoft.AspNetCore.Http;

namespace DotBelt.CMS.Shared.CMS.Media;

public class UploadRequest
{
    public List<IFormFile>? Files { get; set; }
}