using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TranslationService : ITranslationService
    {
        public string Translate(string ClientString)
        {
            string[] words = ClientString.Split(' ');
            string result = "";

            foreach (string word in words)
            {
                result += word.Substring(1);
                result += word.Substring(0, 1) + "ay ";
            }
            return result;
        }
    }    
}
