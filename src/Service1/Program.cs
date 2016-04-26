using System;
using System.Net.Configuration;
using System.Threading.Tasks;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using Core;
using RawRabbit;

namespace Service1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bus = GetBusClient();
            var tester = new PrimesHandler();
            tester.Handle(bus);

            Console.WriteLine("================================================");
            Console.WriteLine("Hit <Enter> to exit...");
            Console.WriteLine("================================================");
            Console.ReadLine();
        }

        public static IBusClient GetBusClient()
        {
            var config = new RawRabbitConfiguration
            {
                Hostnames = { "192.168.99.100" },
                Port = 5672,
                VirtualHost = "/",
                Username = "guest",
                Password = "guest"
            };
            var client = BusClientFactory.CreateDefault(config);
            return client;
        }
    }

    public class PrimesHandler
    {
        public void Handle(IBusClient bus)
        {
            bus.SubscribeAsync<IsPrimeCommand>(async (msg, context) =>
            {
                Console.WriteLine($"need to calculate isPrime for: {msg.Number}.");
                await Task.FromResult(true);
            });
        }
    }

}
