using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using StackExchange.Redis;

namespace TableStorage
{
    class CacheItem : TableEntity
    {
        public CacheItem()
        {

        }
        public CacheItem(int key, string value) : base("cache", key.ToString())
        {
            Key = key;
            Value = value;
        }

        public int Key { get; set; }

        public string Value { get; set; }
    }

    public class Program
    {
        static CloudStorageAccount acc = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=mbafiapstorage;AccountKey=uGB5fWizvpeuJUvruCGRwtZ2EjpwlN1KqeSwexwOFz7EyXmXLaTl3BnepVesNx409vkf08gCyfKBJULtUPZkhQ==;EndpointSuffix=core.windows.net");

        static void Main(string[] args)
        {
            //TableStorage();
            TableStorageCacheReddis();
            Console.Read();
        }

        public static void TableStorage()
        {
            var client = acc.CreateCloudTableClient();
            var table = client.GetTableReference("cache");
            table.CreateIfNotExists();

            LoadCache(table);

            Console.WriteLine("Digite o id");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            TableOperation op = TableOperation.Retrieve<CacheItem>("cache", id.ToString());
            var cacheItem = table.Execute(op);
            if (cacheItem.Result != null)
            {
                var item = cacheItem.Result as CacheItem;

                Console.WriteLine($"id: {item.RowKey} - valor: {item.Value}");
            }
            else
            {
                AddItem(id, Guid.NewGuid().ToString(), table);
            }
        }
        static void LoadCache(CloudTable table)
        {
            for (int i = 1; i <= 100; i++)
            {
                AddItem(i, Guid.NewGuid().ToString(), table);
            }
        }

        static void AddItem(int id, string value, CloudTable table)
        {
            var cacheItem = new CacheItem(id, value);
            var operation = TableOperation.InsertOrReplace(cacheItem);
            table.Execute(operation);
        }


        public static void TableStorageCacheReddis()
        {
            var connection = ConnectionMultiplexer.Connect("mbafiapredis.redis.cache.windows.net:6380,password=0HhNtNatKRkFitAWl+Ou8uhcwWK+Ie+5ryac6F5bn6s=,ssl=True,abortConnect=False");
            
            IDatabase db = connection.GetDatabase();
            for (int i = 1; i < 100; i++)
            {
                db.StringSet(i.ToString(), Guid.NewGuid().ToString());
            }
            Console.WriteLine("Digite o id");
            var id = Console.ReadLine();
            var item = db.StringGet(id);

            if (string.IsNullOrWhiteSpace(item) == false)
                Console.WriteLine(item);
            else
                Console.WriteLine($"Não existe valor para o id {id}");

        }
    }
}