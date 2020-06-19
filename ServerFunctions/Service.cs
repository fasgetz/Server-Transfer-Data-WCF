using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerFunctions
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class Service : IService
    {
        #region Свойства

        // Список подключенных пользователей
        ObservableCollection<ServerUser> users = new ObservableCollection<ServerUser>();
        int nextId = 1; // Идентефикаторы юзеров


        private string GetUsers()
        {
            // Преобразуем JSON
            string serialized = JsonConvert.SerializeObject(users);
            Console.WriteLine(serialized);


            return serialized;
        }
        #endregion


        #region Методы контракта


        public void Disconnect(int id)
        {
            try
            {

                var user = users.FirstOrDefault(i => i.ID == id);
                users.Remove(users.FirstOrDefault(i => i.ID == id));
                
                SendMessage($"{System.DateTime.Now} {user.Name} отключился от чата", 0);
                Console.WriteLine($"{System.DateTime.Now} пользователь {id} отключился от чата");

                // Теперь нужно отправить всем обновленный список пользователей, находящихся в сети
                GetOnlineUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        // Метод отправки сообщения
        public void SendMessage(string Message, int UserID)
        {
            // Отправляем сообщения всем юзерам
            foreach (var item in new List<ServerUser>(users))
            {
                // Формируем ответ
                string answer = string.Empty;

                // Если пользователь только вошел сформируй сообщение от имени системы
                if (UserID == 0)
                {
                    answer = $"{Message}";
                }
                // Иначе сформируй сообщение от имени отправителя
                else
                {
                    var user = users.FirstOrDefault(i => i.ID == UserID);

                    // Если пользователь найден (значит, что подключен к чату, то отправь сообщение)
                    if (user != null)
                    {
                        answer = $"{System.DateTime.Now}) {user.Name}: {Message}";
                    }
                }

                // Проверяем находится ли юзер в сети
                // Получаем ответ
                try
                {
                    bool HasOnline = item.operationContext.GetCallbackChannel<IMyContractCallBack>().HasOnline();

                    // Если пользователь в сети, то отправить сообщение
                    if (HasOnline == true)
                    {
                        // Отправляем ответ
                        item.operationContext.GetCallbackChannel<IMyContractCallBack>().MsgCallback(answer);
                        //item.operationContext.GetCallbackChannel<IMyContractCallBack>().GetUsersOnline(GetUsers());


                        Console.WriteLine($"{item.ID} в сети");
                    }
                }
                catch (Exception ex)
                {
                    // Иначе отключаем пользователя
                    Disconnect(item.ID);
                }
                


            }
        }



        // Метод подключения к серверу
        public int ConnectOnServer(string name)
        {
            try
            {
                ServerUser user = new ServerUser()
                {
                    ID = nextId,
                    Name = name,
                    operationContext = OperationContext.Current
                };
                nextId++;

                //Если уже есть подключенные пользователи то отправить им сообщение
                if (users.Count != 0)
                    SendMessage($"{System.DateTime.Now}) {name} подключился к чату!", 0);

                Console.WriteLine($"{System.DateTime.Now}) {user.Name} - id {user.ID} подключился к чату");

                users.Add(user);

                test(user);

                return user.ID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }

        private async void test(ServerUser user)
        {
            await Task.Run(() =>
            {
                // Отправляем всем списки пользователей
                foreach (var item in new List<ServerUser>(users.Where(i => i.ID != user.ID)))
                {
                    item.operationContext.GetCallbackChannel<IMyContractCallBack>().UserConnected(GetUsers());
                }
            });
        }



        public void GetOnlineUsers()
        {
            // Получаем список всех активных юзеров
            foreach (var item in new List<ServerUser>(users))
            {
                // Проверяем находится ли юзер в сети
                // Получаем ответ
                try
                {
                    bool HasOnline = item.operationContext.GetCallbackChannel<IMyContractCallBack>().HasOnline();

                    // Если пользователь в сети, то отправить сообщение
                    if (HasOnline == true)
                    {
                        // Отправляем ответ
                        //item.operationContext.GetCallbackChannel<IMyContractCallBack>().MsgCallback(answer);
                        item.operationContext.GetCallbackChannel<IMyContractCallBack>().UserConnected(GetUsers());


                        Console.WriteLine($"{item.ID} в сети");
                    }
                }
                catch (Exception ex)
                {
                    // Иначе отключаем пользователя
                    //Disconnect(item.ID);
                }
            }
        }



        #endregion



    }
}
