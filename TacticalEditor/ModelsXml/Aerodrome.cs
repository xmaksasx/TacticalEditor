using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TacticalEditor.ModelsXml
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Aerodrome
    {

        private AerodromeRunway[] runwayField;

        private double latitudeField;

        private double longitudeField;

        private double altitudeField;

        private string nameField;

        private string rusnameField;

        private string countryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Runway")]
        public AerodromeRunway[] Runway
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunway
    {

        private AerodromeRunwayDirectCourse directCourseField;

        private AerodromeRunwayInverseCourse inverseCourseField;

        private double lengthField;

        private double widthField;

        /// <remarks/>
        public AerodromeRunwayDirectCourse DirectCourse
        {
            get
            {
                return this.directCourseField;
            }
            set
            {
                this.directCourseField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayInverseCourse InverseCourse
        {
            get
            {
                return this.inverseCourseField;
            }
            set
            {
                this.inverseCourseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double length
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double width
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayDirectCourse
    {

        private AerodromeRunwayDirectCourseThreshold thresholdField;

        private AerodromeRunwayDirectCourseLocatorMiddle locatorMiddleField;

        private AerodromeRunwayDirectCourseLocatorOuter locatorOuterField;

        private AerodromeRunwayDirectCourseILS iLSField;

        /// <remarks/>
        public AerodromeRunwayDirectCourseThreshold Threshold
        {
            get
            {
                return this.thresholdField;
            }
            set
            {
                this.thresholdField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayDirectCourseLocatorMiddle LocatorMiddle
        {
            get
            {
                return this.locatorMiddleField;
            }
            set
            {
                this.locatorMiddleField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayDirectCourseLocatorOuter LocatorOuter
        {
            get
            {
                return this.locatorOuterField;
            }
            set
            {
                this.locatorOuterField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayDirectCourseILS ILS
        {
            get
            {
                return this.iLSField;
            }
            set
            {
                this.iLSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayDirectCourseThreshold
    {

        private double latitudeField;

        private double longitudeField;

        private double xField;

        private double zField;

        private double rwyField;

        private double headingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double rwy
        {
            get
            {
                return this.rwyField;
            }
            set
            {
                this.rwyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayDirectCourseLocatorMiddle
    {

        private double yField;

        private double xField;

        private double latitudeField;

        private double longitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double y
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayDirectCourseLocatorOuter
    {

        private double yField;

        private double xField;

        private double latitudeField;

        private double longitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double y
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayDirectCourseILS
    {

        private AerodromeRunwayDirectCourseILSLOC lOCField;

        private AerodromeRunwayDirectCourseILSGS gsField;

        /// <remarks/>
        public AerodromeRunwayDirectCourseILSLOC LOC
        {
            get
            {
                return this.lOCField;
            }
            set
            {
                this.lOCField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayDirectCourseILSGS GS
        {
            get
            {
                return this.gsField;
            }
            set
            {
                this.gsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayDirectCourseILSLOC
    {

        private double yField;

        private double xField;

        private double latitudeField;

        private double longitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double y
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayDirectCourseILSGS
    {

        private double yField;

        private double xField;

        private double latitudeField;

        private double longitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double y
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayInverseCourse
    {

        private AerodromeRunwayInverseCourseThreshold thresholdField;

        private AerodromeRunwayInverseCourseLocatorMiddle locatorMiddleField;

        private AerodromeRunwayInverseCourseLocatorOuter locatorOuterField;

        private AerodromeRunwayInverseCourseILS iLSField;

        /// <remarks/>
        public AerodromeRunwayInverseCourseThreshold Threshold
        {
            get
            {
                return this.thresholdField;
            }
            set
            {
                this.thresholdField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayInverseCourseLocatorMiddle LocatorMiddle
        {
            get
            {
                return this.locatorMiddleField;
            }
            set
            {
                this.locatorMiddleField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayInverseCourseLocatorOuter LocatorOuter
        {
            get
            {
                return this.locatorOuterField;
            }
            set
            {
                this.locatorOuterField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayInverseCourseILS ILS
        {
            get
            {
                return this.iLSField;
            }
            set
            {
                this.iLSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayInverseCourseThreshold
    {

        private double latitudeField;

        private double longitudeField;

        private double xField;

        private double zField;

        private double rwyField;

        private double headingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double rwy
        {
            get
            {
                return this.rwyField;
            }
            set
            {
                this.rwyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayInverseCourseLocatorMiddle
    {

        private double yField;

        private double xField;

        private double latitudeField;

        private double longitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double y
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayInverseCourseLocatorOuter
    {

        private double yField;

        private double xField;

        private double latitudeField;

        private double longitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double y
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayInverseCourseILS
    {

        private AerodromeRunwayInverseCourseILSLOC lOCField;

        private AerodromeRunwayInverseCourseILSGS gsField;

        /// <remarks/>
        public AerodromeRunwayInverseCourseILSLOC LOC
        {
            get
            {
                return this.lOCField;
            }
            set
            {
                this.lOCField = value;
            }
        }

        /// <remarks/>
        public AerodromeRunwayInverseCourseILSGS GS
        {
            get
            {
                return this.gsField;
            }
            set
            {
                this.gsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayInverseCourseILSLOC
    {

        private double yField;

        private double xField;

        private double latitudeField;

        private double longitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double y
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AerodromeRunwayInverseCourseILSGS
    {

        private double yField;

        private double xField;

        private double latitudeField;

        private double longitudeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double y
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double x
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
