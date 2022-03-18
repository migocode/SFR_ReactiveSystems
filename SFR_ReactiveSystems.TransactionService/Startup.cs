using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFR_ReactiveSystems.TransactionService
{
    public class Startup : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                Thread.Sleep(10000);
                Console.WriteLine("Tick");
            }
        }
    }
}
