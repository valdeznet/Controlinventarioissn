using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Controlinventarioissn.Helpers
{
    public class BlobHelper : IBlobHelper //implementa la interfaz IBlobHelper
    {
        private readonly CloudBlobClient _blobClient;//creamos una propiedad que se llama CloudBlobClient - ahi instala el paquete de azure storage
        public BlobHelper(IConfiguration configuration)
        {
            string keys = configuration["Blob:ConnectionString"];//leemos el valor que esta en el helper 
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys);//llave blob - decimos que busque las keys
            _blobClient = storageAccount.CreateCloudBlobClient();

        }
        public async Task DeleteBlobAsync(Guid id, string containerName)
        {
            try
            {
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}");
                await blockBlob.DeleteAsync();
            }
            catch { }
        }

        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName)
        {
            Stream stream = file.OpenReadStream();
            return await UploadBlobAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(byte[] file, string containerName)
        {
            MemoryStream stream = new MemoryStream(file);
            return await UploadBlobAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(string image, string containerName)
        {
            Stream stream = File.OpenRead(image);
            return await UploadBlobAsync(stream, containerName);
        }
        private async Task<Guid> UploadBlobAsync(Stream stream, string containerName)
        {
            Guid name = Guid.NewGuid(); //si subo una foto como guid me da un ID unico
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName); //
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{name}");//creane un block con ese nombre
            await blockBlob.UploadFromStreamAsync(stream);//despues 
            return name;
        }
    }
}
