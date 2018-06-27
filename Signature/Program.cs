using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signature
{
    public class Program
    {
       public static void Main(string[] args)
        {
            CloudStorageAccount acc = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=mbafiapstorage;AccountKey=uGB5fWizvpeuJUvruCGRwtZ2EjpwlN1KqeSwexwOFz7EyXmXLaTl3BnepVesNx409vkf08gCyfKBJULtUPZkhQ==;EndpointSuffix=core.windows.net");
            var client = acc.CreateCloudBlobClient();
            var container = client.GetContainerReference("alunos");
            var blob = container.GetBlockBlobReference("45240.txt");
                blob.UploadText("mba fiap 2018 - 12NET");
            var sas = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy() {
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(2),
                Permissions = SharedAccessBlobPermissions.Read 
                });

            Console.WriteLine(blob.Uri + sas);
            Console.Read();
        }
    }
}
