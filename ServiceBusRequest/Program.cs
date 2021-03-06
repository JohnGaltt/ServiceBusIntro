﻿using Microsoft.Azure.ServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusRequest
{
    class Program
    {
        static string serviceBusConnectionString = "Endpoint=sb://alextrench.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=oPGSnetzLIfJKBRhzJr8i2ohUULi0lG/T6/QQO/eS+0=";

        static string queueName = "queue1";

        static QueueClient queueClient;
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            queueClient = new QueueClient(serviceBusConnectionString, queueName);
            RegisterOnMessageHandlerAndReceiveMessages();
            Console.ReadKey();
            await queueClient.CloseAsync();
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlersOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlersOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body: {message.Body}");
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            return Task.CompletedTask;
        }
    }
}
