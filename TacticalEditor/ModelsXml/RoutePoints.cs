using System.Collections.Generic;

namespace TacticalEditor.ModelsXml
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class RoutePoints
    {
        public RoutePoints()
        {
            PPM = new List<Ppm>();
        }

        private List<Ppm> pPMField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PPM")]
        public List<Ppm> PPM
        {
            get
            {
                return this.pPMField;
            }
            set
            {
                this.pPMField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Ppm
    {

        private double relativeXField;

        private double relativeYField;

        /// <remarks/>
        public double RelativeX
        {
            get
            {
                return this.relativeXField;
            }
            set
            {
                this.relativeXField = value;
            }
        }

        /// <remarks/>
        public double RelativeY
        {
            get
            {
                return this.relativeYField;
            }
            set
            {
                this.relativeYField = value;
            }
        }
    }


}
