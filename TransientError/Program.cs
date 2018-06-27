using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TransientError
{
   public class Program
    {
       public static void Main(string[] args)
        {
            int retryCount = 5;
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    //Chamada a uma API Externa
                    var response = CallApi();
                    if (response) break;

                    var policyResult = Policy
                        .Handle<HttpRequestException>()
                        .RetryAsync()
                        .ExecuteAndCapture(() => CallApi());

                }
                catch (Exception ex)
                {
                    if (ex.HResult == 502) throw; //Erro não transiente (por exemplo)
                    if (ex.InnerException != null && i + 1 == retryCount)
                    {
                        throw;
                    }
                    
                }
            }
        }

        public static bool CallApi()
        {
            Console.WriteLine("Chamando api de pagamento");
            return true;
        }
    }
}
