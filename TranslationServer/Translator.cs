using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationServer
{
    public class Translator : MarshalByRefObject
    {
        public string Translate(string EnglishString) {
            string[] words = EnglishString.Split(' ');
            string result = "";

            foreach(string word in words)
            {
                result += word.Substring(1);
                result += word.Substring(0, 1) + "ay ";
            }
            return result;
        }
    }
}
