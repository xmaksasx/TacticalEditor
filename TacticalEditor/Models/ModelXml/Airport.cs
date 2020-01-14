using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TacticalEditor.Models.ModelXml
{

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Airport
    {

        private Local localField;

        private Runway runwayField;

        public Local Local
        {
            get
            {
                return this.localField;
            }
            set
            {
                this.localField = value;
            }
        }

        public Runway Runway
        {
            get
            {
                return this.runwayField;
            }
            set
            {
                this.runwayField = value;
            }
        }
    }


    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class Local
    {

        private double latitudeField;

        private double longitudeField;

        private double altitudeField;

        private string nameField;

        private string rusnameField;

        private string countryField;

        [XmlAttribute()]
        public double latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

        [XmlAttribute()]
        public double longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }

        [XmlAttribute()]
        public double altitude
        {
            get
            {
                return this.altitudeField;
            }
            set
            {
                this.altitudeField = value;
            }
        }

        [XmlAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [XmlAttribute()]
        public string rusname
        {
            get
            {
                return this.rusnameField;
            }
            set
            {
                this.rusnameField = value;
            }
        }

        [XmlAttribute()]
        public string country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }
    }


    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class Runway
    {

        private decimal xField;

        private decimal yField;

        private double headingField;

        private decimal lengthField;

        private decimal widthField;

        private double latitudeField;

        private double longitudeField;

        [XmlAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }


        [XmlAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        [XmlAttribute()]
        public double heading
        {
            get
            {
                return this.headingField;
            }
            set
            {
                this.headingField = value;
            }
        }

        [XmlAttribute()]
        public decimal length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }


        [XmlAttribute()]
        public decimal width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }


        [XmlAttribute()]
        public double latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

      
        [XmlAttribute()]
        public double longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }
    }

}
