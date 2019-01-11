using System;

namespace ServiceBusRequest
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
    }
}
