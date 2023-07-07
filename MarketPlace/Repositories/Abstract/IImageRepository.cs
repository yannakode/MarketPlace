namespace MarketPlace.Repositories.Abstract
{
    public interface IImageRepository
    {
        public Task<IFormFile> UploadImage(IFormFile file);
        public bool ValidateFile(IFormFile file);
    }
}
