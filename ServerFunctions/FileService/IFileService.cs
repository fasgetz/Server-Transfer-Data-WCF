using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerFunctions.FileService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IFileService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IFileService
    {
        /// <summary>
        /// Метод получения файла с сервера
        /// </summary>
        /// <param name="path">Путь файла</param>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        Stream UploadFile(string path);
    }
}
