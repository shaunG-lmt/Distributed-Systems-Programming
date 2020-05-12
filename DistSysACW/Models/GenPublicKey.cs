using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CoreExtensions;
namespace DistSysACW.Models
{
    public class Keys
    {
        private string publicKey;
        private string privateKey;
        private static Keys instance = null;
        private static readonly object padlock = new object();
        private Keys() { }

        public static Keys Instance
        {
            get 
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Keys();
                    }
                    return instance;
                }
            }
        }

        public void SetKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            publicKey = CoreExtensions.RSACryptoExtensions.ToXmlStringCore22(rsa, false);
            privateKey = CoreExtensions.RSACryptoExtensions.ToXmlStringCore22(rsa, true);
        }
        public string GetPublic()
        {
            return publicKey;
        }
        public string GetPrivate()
        {
            return privateKey;
        }
    }
}