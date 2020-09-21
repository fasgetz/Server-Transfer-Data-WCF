using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerFunctions.FileService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = true)]
    public class FileService : IFileService
    {
        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Stream UploadFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            return fs;
        }
    }
}
