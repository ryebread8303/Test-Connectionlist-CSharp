using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace TestConnectionList
{
    class Program
    {
        class HostList
        {
            public string[] hosts;
            public HostList(string file)
            {
                this.FilePath = file;
                if (!File.Exists(this.FilePath))
                {
                    throw new Exception("The hosts file does not exist.");
                }
                this.hosts = File.ReadAllLines(this.FilePath);
            }
            public string[] Hosts
            {
                get
                {
                    return this.hosts;
                }
            }

            public string FilePath { get; }

        }
        static async Task<PingReply> AsyncPing(string host)
        {
            Ping ping = new Ping();
            PingReply reply = await ping.SendPingAsync(host);
            return reply;
        }


        static async Task Main(string[] args)
        {
            HostList hosts = new HostList(@"C:\Users\oryan\source\repos\Test-Connectionlist\Project1\pinglist.txt");
            List<PingReply> results = new List<PingReply>();
            foreach(string host in hosts.Hosts)
            {
                PingReply reply = await AsyncPing(host);
                results.Add(reply);

            }
            foreach (PingReply result in results)
            {
                Console.Write("Pinging {0}\r\n", result.Address.ToString());
                Console.Write("Reply from {0} : time={1} TTL={2}\r\n\r\n", result.Address.ToString(), result.RoundtripTime, result.Options.Ttl);
            }
            
            //foreach (string host in hosts.Hosts)
            //{
            //    PingReply reply = pinger.Send(host);
            //    Console.Write("Pinging {0} [{1}]\r\n", host, reply.Address.ToString());
            //    Console.Write("Reply from {0} : time={1} TTL={2}\r\n\r\n", reply.Address.ToString(), reply.RoundtripTime, reply.Options.Ttl);
            //}
            _ = Console.ReadKey();
        }
    }
}
