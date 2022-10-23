using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        List<User> users = new List<User>();
        int nextId = 1;

        public int Connect(string name)
        {
            User user = new User()
            {
                ID = nextId,
                UserName = name,
                operationContext = OperationContext.Current,
            };
            nextId++;

            SendMessage(": " + user.UserName + " conectado!", 0);
            users.Add(user);

            return user.ID;
        }

        public void Disconnect(int identificator)
        {
            var user = users.FirstOrDefault(i => i.ID == identificator);
            if (user != null)
            {
                users.Remove(user);
                SendMessage(": " + user.UserName + " ha abandonado el chat", 0);

            }
        }


        public void SendMessage(string message, int identificator)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(i => i.ID == identificator);
                if (user != null)
                {
                    answer += ": " + user.UserName + " ";
                }
                answer += message;
                item.operationContext.GetCallbackChannel<IChatServiceCallback>().MessageCallBack(answer);
            }
        }
    }
}
