using Microsoft.AspNetCore.Http;

namespace booklistDomain.Services
{
    public interface IFileService
    {
        Task<Uri> GetUrlOfImage(IFormFile image);//传入图片，实现云存储，返回url
        Task<bool> DeleteImage(Uri url);//删除图片

    }
}
