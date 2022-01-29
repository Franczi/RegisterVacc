using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Services
{
    public class MessageConsumer
    {
        private ServiceBusClient _client;
        private ServiceBusProcessor _processor;

        public MessageConsumer(IConfiguration configuration)
        {
            _client = new ServiceBusClient(configuration.GetConnectionString("ServiceBusConnection"));
        }
        public async Task Register()
        {
            _processor = _client.CreateProcessor("messages");
            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync();
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            Console.WriteLine("Error occured: " + arg.Exception);
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            var payload = arg.Message.Body.ToObjectFromJson<MessagePayload>();
            if (payload.EventName == "New_Registration")
            {
                await arg.CompleteMessageAsync(arg.Message);
            }
        }

    }

    public class MessagePayload
    {
        public string EventName { get; set; }
    }
}
