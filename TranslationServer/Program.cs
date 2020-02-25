using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using TranslationInterface;

namespace TranslationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel channel = new TcpChannel(5000);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Translator), "Translate", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Press the enter key to exit...");
            Console.ReadLine();
        }
    }

}
