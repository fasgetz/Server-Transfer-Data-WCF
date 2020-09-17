using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServerFunctions
{


    [ServiceContract(CallbackContract = typeof(IMyContractCallBack))]
    //[ServiceContract(SessionMode = SessionMode.Allowed, CallbackContract = typeof(IMyContractCallBack))]
    public interface IService
    {

        /// <summary>
        /// Подключение к серверу
        /// </summary>
        /// <param name="name">Имя юзера</param>
        /// <returns>Айди юзера</returns>
        [OperationContract]
        int ConnectOnServer(string name);


        // Получает список пользователей онлайн
        [OperationContract(IsOneWay = true)]
        void GetOnlineUsers();


        /// <summary>
        /// Отключение юзера от сервера
        /// </summary>
        /// <param name="id">Айди юзера</param>
        [OperationContract]
        void Disconnect(int id);


        /// <summary>
        /// Метод для отправки сообщения
        /// </summary>
        /// <param name="Message">Сообщение</param>
        /// <param name="UserID">Айди пользователя</param>
        [OperationContract(IsOneWay = true)]
        void SendMessage(string Message, int UserID);
    }

    [ServiceContract]
    public interface IMyContractCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);


        /// <summary>
        /// Метод, который вызывается, когда кто-то вошел в чат (нужно прогрузить юзеров в чате)
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void UserConnected(string json);



        /// <summary>
        /// Проверка онлайна со стороны клиента к серверу
        /// </summary>
        /// <returns></returns>

        [OperationContract(IsOneWay = false)]
        bool HasOnline();

        /// <summary>
        /// Список пользователей онлайн
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void GetUsersOnline(string usersOnlineJson);
    }
}
