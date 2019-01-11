using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    class Program
    {
        static string serviceBusConnectionString = "Endpoint=sb://alextrench.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=oPGSnetzLIfJKBRhzJr8i2ohUULi0lG/T6/QQO/eS+0=";

        static string queueName = "queue1";

        static IQueueClient queueClient;
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        } 
        
        static async Task MainAsync()
        {
            const int numberOfMessages = 10;
            queueClient = new QueueClient(serviceBusConnectionString, queueName);
            await SendMessagesAsync(numberOfMessages);
            await queueClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessages)
        {
            try
            {
                for (int i = 0; i < numberOfMessages; i++)
                {
                    var messageContent = $"Message {i}";

                    var messageToSend = new Message(Encoding.UTF8.GetBytes(messageContent));

                    Console.WriteLine(messageContent);

                    await queueClient.SendAsync(messageToSend);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine($"{exception.InnerException}");
            }

            }
    }
}
