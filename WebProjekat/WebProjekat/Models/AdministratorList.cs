using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    [XmlRoot("Administrators")]
    public class AdministratorList
    {
        [XmlElement("Administrator")]
        public List<Administrator> Administrators { get; set; }

        public AdministratorList()
        {
            Administrators = new List<Administrator>();
        }
    }
}