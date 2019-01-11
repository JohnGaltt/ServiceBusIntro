using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusTopicSender
{
    class Program
    {
        static string serviceBusConnectionString = "Endpoint=sb://alextrench.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=oPGSnetzLIfJKBRhzJr8i2ohUULi0lG/T6/QQO/eS+0=";

        static string topicName = "topic1";

        static ITopicClient topicClient;

        static void Main(string[] args)

        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 5;
            topicClient = new TopicClient(serviceBusConnectionString, topicName);
            await SendMessagesAsync(numberOfMessages);
            Console.ReadKey();
            await topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (int i = 0; i < numberOfMessagesToSend; i++)
                {
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    await topicClient.SendAsync(message);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Exception occured: {exception.InnerException}");
            }
        }
    }
}
