using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationInterface;

namespace TranslationServer
{
    public class Translator : MarshalByRefObject, ITranslation
    {
        string ITranslation.Translate(string EnglishString) {
            string[] words = EnglishString.Split(' ');
            string result = "";

            foreach(string word in words)
            {
                result += word.Substring(1);
                result += word.Substring(0, 1) + "ay ";
            }
            return result;
        }
        string ITranslation.GetName()
        {
            return "Shaun Gill";
        }
        string ITranslation.GetStudentID()
        {
            return "201710478";
        }
    }
}
