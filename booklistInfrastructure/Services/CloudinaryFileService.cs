using booklistDomain.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace booklistInfrastructure.Services
{
    public class CloudinaryFileService : IFileService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryFileService()
        {
            var acc = new Account(
                "dexylkwa6",
                "914735594528766",
                "kZhgbpOZyeQ6XALsYLCwrIVfPwk"
                );
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<bool> DeleteImage(Uri url)
        {
            var publicId = url.ToString().Split('/').Last().Split('.')[0];
            var deleteParams = new DeletionParams(publicId);
            try
            {
                await _cloudinary.DestroyAsync(deleteParams);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<Uri> GetUrlOfImage(IFormFile image)
        {
            var uploadResult = new ImageUploadResult();
            if (image != null)
            {
                using var stream = image.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult.Url;
        }
    }
}
