using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

namespace Tools.Checklink
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Check if connection is up or down with determined ip
            var ip = args.FirstOrDefault();

            if (ip == null)
                ip = "8.8.8.8";

            Console.WriteLine($"Testing with {ip}");

            var currentStatus = "";
            while (true)
            {
                using var ping = new Ping();
                var reply = ping.Send(ip);
                if (!(reply.Status.ToString().Equals(currentStatus)))
                {
                    if (reply.Status != IPStatus.Success)
                        Console.WriteLine($"Down | {DateTime.Now}");
                    else
                        Console.WriteLine($"Up   | {DateTime.Now}");
                }
                currentStatus = $"{reply.Status}";
                Thread.Sleep(100);
            }
        }
    }
}
