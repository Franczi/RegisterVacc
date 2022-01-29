using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterVacc.Services
{
    public class MessageService
    {
       
            private ServiceBusClient _client;

            public MessageService(IConfiguration configuration)
            {
                _client = new ServiceBusClient(configuration.GetConnectionString("ServiceBusConnection"));
            }

            public async Task SendMessage(MessagePayload payload)
            {
                await _client.CreateSender("bartosz_wieczorek").SendMessageAsync(new ServiceBusMessage(JsonConvert.SerializeObject(payload)));

            }
       
    }

    public class MessagePayload
    {
        public string EventName { get; set; }
    }
}
