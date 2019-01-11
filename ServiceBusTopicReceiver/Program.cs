using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusTopicReceiver
{
    class Program
    {

        static string serviceBusConnectionString = "Endpoint=sb://alextrench.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=oPGSnetzLIfJKBRhzJr8i2ohUULi0lG/T6/QQO/eS+0=";

        static string topicName = "topic1";
        static string subscriptionName = "subscription1";
        static SubscriptionClient subscriptionClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topicName, subscriptionName);
            RegisterOnMessageHandlerAndReceiveMessages();
            Console.ReadKey();
            await subscriptionClient.CloseAsync();
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlersOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlersOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber}  Body: {Encoding.UTF8.GetString(message.Body)}");
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            //Logs
            return Task.CompletedTask;
        }
    }
}
