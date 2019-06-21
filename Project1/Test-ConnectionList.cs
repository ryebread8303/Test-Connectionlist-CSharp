using System;
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

        class CSV
        {
            public string FilePath { set; }
            public string InputObject { set; }
            private bool ValidateFile(string filePath)
            {
                if (File.Exists(filePath))
                {
                    return false;
                }
            }
            public OutFile(string[] InputObject, string FilePath)
            {

            }

        }

        class AsyncPinger
        {
            private int count;

            private string[] hosts;
            public string[] Hosts
            {
                get
                {
                    return this.hosts;
                }
                set
                {
                    this.hosts = value;
                }
            }

        }

        static void Main(string[] args)
        {
            Ping pinger = new Ping();
            HostList hosts = new HostList(@"C:\Users\oryan\source\repos\Test-Connectionlist\Project1\pinglist.txt");
            foreach (string host in hosts.Hosts)
            {
                PingReply reply = pinger.Send(host);
                Console.Write("Pinging {0} [{1}]\r\n", host, reply.Address.ToString());
                Console.Write("Reply from {0} : time={1} TTL={2}\r\n\r\n", reply.Address.ToString(), reply.RoundtripTime, reply.Options.Ttl);
            }
            _ = Console.ReadKey();
        }
    }
}
