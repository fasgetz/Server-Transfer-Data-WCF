using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient
{
    class Program
    {
        // Метод, который запускает сервер
        private static void StartServer()
        {


            Uri httpUrl = new Uri("http://localhost:8000/");

            // Запускаем сервис Service
            using (var host = new ServiceHost(typeof(ServerFunctions.Service), httpUrl))
            {
                //Create a URI to serve as the base address

                //Add a service endpoint
                host.AddServiceEndpoint(typeof(ServerFunctions.IService), new WSDualHttpBinding() { SendTimeout = new TimeSpan(0, 0, 5)}, "");
                
                //Enable metadata exchange
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);



                host.Open(); // Открываем хост
                Console.WriteLine($"{DateTime.Now}) Хост Serivce стартовал!");
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            StartServer();
        }
    }
}
