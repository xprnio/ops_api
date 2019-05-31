using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using com.Messente.Api.Api;
using com.Messente.Api.Client;
using com.Messente.Api.Model;
using Microsoft.Extensions.Configuration;
using OPS_API.Domain.Services;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Services
{
    public class MessageService : IMessageService
    {
        private IOmnimessageApi _api;

        public MessageService(IConfiguration configuration)
        {
            if (configuration["AppSettings:Messente:Disabled"] != "true")
            {
                _api = new OmnimessageApi(ReadFromConfig(configuration));
            }
            else
            {
                Console.WriteLine("Messaging disabled");
            }
        }

        public async Task<Response> SendMessage(string phoneNumber, string text)
        {
            if (_api == null)
            {
                Console.WriteLine($"SendMessage({phoneNumber})");
                return new Response(true, "Messaging disabled");
            }

            if (phoneNumber.StartsWith("+666") || phoneNumber.StartsWith("666"))
            {
                Console.WriteLine($"Skipping fake number {phoneNumber}");
                return new Response(true, "Skipped fake number");
            }

            var message = new Omnimessage(
                phoneNumber,
                new List<object> {new SMS(text)}
            );

            try
            {
                await _api.SendOmnimessageAsync(message);

                return new Response(true, "Message sent");
            }
            catch (Exception e)
            {
                return new Response(false, e.Message);
            }
        }

        private static Configuration ReadFromConfig(IConfiguration config)
            => new Configuration
            {
                Username = config["AppSettings:Messente:Username"],
                Password = config["AppSettings:Messente:Password"]
            };
    }
}