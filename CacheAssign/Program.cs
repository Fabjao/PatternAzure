using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheAssign
{
    public class Program
    {
        static Dictionary<int, string> cache = new Dictionary<int, string>();
        public static void Main(string[] args)
        {
            var key = "DefaultEndpointsProtocol=https;AccountName=mbafiapstorage;AccountKey=uGB5fWizvpeuJUvruCGRwtZ2EjpwlN1KqeSwexwOFz7EyXmXLaTl3BnepVesNx409vkf08gCyfKBJULtUPZkhQ==;EndpointSuffix=core.windows.net";

            //LoadCache();
            //Console.WriteLine("Digire o id");
            //int id = 0;
            //int.TryParse(Console.ReadLine(), out id);
            //if (cache.ContainsKey(id))
            //{
            //    Console.WriteLine($"id: {id} - valor: {cache[id]}");
            //}
            //else
            //{
            //    cache.Add(id, Guid.NewGuid().ToString());
            //    Console.WriteLine($"id: {id} - valor: {cache[id]}");
            //}

            Console.ReadLine();
        }

        public static void LoadCache()
        {
            for (int i = 0; i < 100; i++)
            {
                cache.Add(i, Guid.NewGuid().ToString());
            }
        }
    }
}
