using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ibsys2.Models
{
    [XmlType(TypeName = "input")]
    public class Input
    {
        public User user { get; set; }
        public Qualitycontrol qualitycontrol { get; set; }
        public List<Selldirect> sellwish { get; set; }
        public List<Selldirect> selldirect { get; set; }
        public List<Orderlist> orderlist { get; set; }
        public List<Productionlist> productionlist { get; set; }
        public List<Workingtimelist> workingtimelist { get; set; }
    }


    [XmlType(TypeName = "user")]
    public class User
    {
        [XmlAttribute("game")]
        public int game { get; set; }

        [XmlAttribute("group")]
        public int group { get; set; }

        [XmlAttribute("period")]
        public int period { get; set; }
    }

    public class Qualitycontrol
    {
        [XmlAttribute("losequantity")]
        public int losequantity { get; set; }

        [XmlAttribute("delay")]
        public int delay { get; set; }

        [XmlAttribute("type")] 
        public string type { get; set; }
    }


    [XmlType(TypeName = "item")]
    public class Selldirect
    {
        [XmlAttribute("article")]
        public int article { get; set; }

        [XmlAttribute("quantity")]
        public int quantity { get; set; }

        [XmlAttribute("price")]
        public double price { get; set; }

        [XmlAttribute("penalty")]
        public double penalty { get; set; }
    }

    [XmlType(TypeName = "order")]
    public class Orderlist
    {
        [XmlAttribute("article")]
        public int article { get; set; }

        [XmlAttribute("quantity")]
        public int quantity { get; set; }

        [XmlAttribute("modus")]
        public int modus { get; set; }
    }

    [XmlType(TypeName = "production")]
    public class Productionlist
    {
        [XmlAttribute("article")]
        public int article { get; set; }

        [XmlAttribute("quantity")]
        public int quantity { get; set; }
    }

    [XmlType(TypeName = "workingtime")]
    public class Workingtimelist
    {
        [XmlAttribute("station")]
        public int station { get; set; }

        [XmlAttribute("shift")]
        public int shift { get; set; }

        [XmlAttribute("overtime")]
        public int overtime { get; set; }
    }
}