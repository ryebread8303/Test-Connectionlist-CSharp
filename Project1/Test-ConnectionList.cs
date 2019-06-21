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

        //class CSV
        //{
        //    public string FilePath { get; set; }
        //    public string InputObject { get; set; }
        //    private bool ValidateFile(string filePath)
        //    {
        //        if (File.Exists(filePath))
        //        {
        //            return false;
        //        }
        //    }
        //    public void OutFile(string[] InputObject, string FilePath)
        //    {

        //    }

        //}

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

            public AsyncPinger()
            {

            }

            public List<Task<System.Net.NetworkInformation.PingReply>> Send()
            {
                Ping ping = new Ping();
                List<Task<System.Net.NetworkInformation.PingReply>> tasks = new List<Task<System.Net.NetworkInformation.PingReply>>();
                foreach (string host in this.hosts)
                {
                    tasks.Add(ping.SendPingAsync(host));
                }
                Task.WaitAll();
                return tasks;
            }

        }

        static void Main(string[] args)
        {
            //Ping pinger = new Ping();
            HostList hosts = new HostList(@"C:\Users\oryan\source\repos\Test-Connectionlist\Project1\pinglist.txt");
            AsyncPinger pinger = new AsyncPinger();
            pinger.Hosts = hosts.hosts;
            pinger.Send();

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
