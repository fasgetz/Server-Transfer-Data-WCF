using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileServiceServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(ServerFunctions.FileService.FileService)))
            {
                host.Open(); // Открываем хост
                Console.WriteLine($"{DateTime.Now}) FileServer стартовал!\n");
                Console.ReadLine();
            }
        }
    }
}
