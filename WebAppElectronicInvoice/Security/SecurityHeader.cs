using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml;

namespace WebAppElectronicInvoice.Security
{
    public class SecurityHeader : MessageHeader
    {
        private readonly string username;
        private readonly string password;

        public SecurityHeader(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public override string Name => "Security";
        public override string Namespace => "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteStartElement("wsse", "UsernameToken", Namespace);
            writer.WriteElementString("wsse", "Username", Namespace, username);
            writer.WriteElementString("wsse", "Password", Namespace, password);
            writer.WriteEndElement();
        }
    }
}
