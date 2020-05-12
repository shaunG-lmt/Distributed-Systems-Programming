﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CoreExtensions;
namespace DistSysACW.Models
{
    public class Crypto
    {
        private string publicKey;
        private string privateKey;
        private static Crypto instance = null;
        private static readonly object padlock = new object();
        private Crypto() 
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                publicKey = CoreExtensions.RSACryptoExtensions.ToXmlStringCore22(rsa, false);
                privateKey = CoreExtensions.RSACryptoExtensions.ToXmlStringCore22(rsa, true);
            }
        }

        public static Crypto Instance
        {
            get 
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Crypto();
                    }
                    return instance;
                }
            }
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