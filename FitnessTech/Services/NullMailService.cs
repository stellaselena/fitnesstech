using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FitnessTech.Services
{
    public class NullMailService : IMailService
    {
        //inject a logger
        private readonly ILogger<NullMailService> _logger;
        public NullMailService(ILogger<NullMailService>logger)
        {
            _logger = logger;
        }
        public void SendMessage(string name, string email, string description)
        {
            //log the message
            _logger.LogInformation($"Name: {name} Email: {email} Description: {description}");
        }
    }
}
