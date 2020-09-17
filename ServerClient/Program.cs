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
            using (var host = new ServiceHost(typeof(ServerFunctions.Service)))
            {
                host.Open(); // Открываем хост
                Console.WriteLine($"{DateTime.Now}) Server стартовал!\n");
                Console.ReadLine();
            }

            //Uri httpUrl = new Uri("http://localhost:8000/");

            //// Запускаем сервис Service
            //using (var host = new ServiceHost(typeof(ServerFunctions.Service), httpUrl))
            //{
            //    //Create a URI to serve as the base address
            //    //var readerQuotas = new System.Xml.XmlDictionaryReaderQuotas()
            //    //{
            //    //    MaxDepth = 64,
            //    //    MaxStringContentLength = 5242880,
            //    //    MaxArrayLength = 16384,
            //    //    MaxBytesPerRead = 4096,
            //    //    MaxNameTableCharCount = 16384
            //    //};

            //    NetTcpSecurity security = new NetTcpSecurity();// { Mode = NetTcpSecurity.None };                
            //    security.Mode = SecurityMode.None;
            //    host.AddServiceEndpoint(typeof(ServerFunctions.IService), new NetTcpBinding() { Security = security, SendTimeout = new TimeSpan(0, 0, 5) }, "");



            //    //Enable metadata exchange
            //    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();                
                
            //    smb.HttpGetEnabled = true;
                
                
            //    host.Description.Behaviors.Add(smb);



            //    host.Open(); // Открываем хост
            //    Console.WriteLine($"{DateTime.Now}) Хост Serivce стартовал!");
            //    Console.ReadLine();
            //}
        }

        static void Main(string[] args)
        {
            StartServer();
        }
    }
}
