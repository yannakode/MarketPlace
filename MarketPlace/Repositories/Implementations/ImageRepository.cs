using MarketPlace.Repositories.Abstract;

namespace MarketPlace.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webEnvironment;

        public ImageRepository(IWebHostEnvironment webEnvironment)
        {
            _webEnvironment = webEnvironment;
        }

        public async Task<IFormFile> UploadImage(IFormFile file)
        {
                string uniqueString = Guid.NewGuid() + file.FileName;
                string pathUpload = Path.Combine(_webEnvironment.ContentRootPath, "Uploads", uniqueString);

                using (var stream = new FileStream(pathUpload, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return file;
        }

        public bool ValidateFile(IFormFile file)
        {
            Dictionary<string, List<byte[]>> signatures = new Dictionary<string, List<byte[]>>()
            {
                {".jpg", new List<byte[]>
                {
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                     new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
                }
                },

                {".png", new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
                },

                {".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
                }
                }
            };

            foreach(string exts in  signatures.Keys)
            {
                using(var reader = new BinaryReader(file.OpenReadStream()))
                {
                    var Signatures = signatures[exts];
                    var readerBytes = reader.ReadBytes(Signatures.Max(s => s.Length));

                    if(Signatures.Any(s => readerBytes.Take(s.Length).SequenceEqual(s)) && file.Length <= 10485760)
                    {
                        return true;
                    };
                }
            }

            return false;
        }
    }
}
