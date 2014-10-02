namespace SnakeUnMess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    public class Configurator
    {
        public readonly Dictionary<string, string> Properties = new Dictionary<string, string>(); 
        
        private XmlDocument configDocument = new XmlDocument();

        public Configurator()
        {
            this.LoadConfig();
        }

        private void LoadConfig()
        {
            Console.WriteLine("Constructor called.");

            var settings = XElement.Load(@"settings.xml");
            var bull = new XmlDocument();
            bull.Load(@"settings.xml");
            Console.WriteLine(bull["settings"]["input"]);
        }
    }
}
