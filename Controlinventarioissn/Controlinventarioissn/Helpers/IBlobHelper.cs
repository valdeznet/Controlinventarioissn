namespace Controlinventarioissn.Helpers
{
	public interface IBlobHelper
	{
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);//cuando seleccione un archivo, contenedo de users o products

        Task<Guid> UploadBlobAsync(byte[] file, string containerName);

        Task<Guid> UploadBlobAsync(string image, string containerName);

        Task DeleteBlobAsync(Guid id, string containerName);

    }
}
