using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsFormsApp.Model
{
    public class LanguageManager
    {
        private XmlDocument xmlDoc = new XmlDocument();
        private string currentLanguage = "en";

        public LanguageManager(string language)
        {
            xmlDoc.Load("./Resources/Languages.xml");
            currentLanguage = language;
        }

        public string Get(string key)
        {
            XmlNode node = xmlDoc.SelectSingleNode($"//Language[@code='{currentLanguage}']/Text[@key='{key}']");
            return node?.InnerText ?? key;
        }

    }
}
