using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WcfServiceLibrary1;

namespace WCFHostApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(TranslationService));

            host.Open();

            Console.WriteLine("Service Running.");
            Console.ReadKey(true);

            host.Close();
        }
    }
}
