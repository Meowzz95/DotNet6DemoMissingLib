using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoMissingLib
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            Console.Out.WriteLine(await (await new HttpClient().GetAsync("https://google.com")).Content.ReadAsStringAsync());
            
            _hostApplicationLifetime.StopApplication();
        }
    }
}