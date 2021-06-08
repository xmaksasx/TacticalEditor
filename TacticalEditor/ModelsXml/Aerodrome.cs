using System.Xml.Serialization;

namespace TacticalEditor.ModelsXml
{

	[XmlRoot(ElementName = "Threshold")]
	public class Threshold
	{

		[XmlAttribute(AttributeName = "latitude")]
		public double Latitude { get; set; }

		[XmlAttribute(AttributeName = "longitude")]
		public double Longitude { get; set; }

		[XmlAttribute(AttributeName = "x")]
		public double X { get; set; }

		[XmlAttribute(AttributeName = "z")]
		public double Z { get; set; }

		[XmlAttribute(AttributeName = "rwy")]
		public int Rwy { get; set; }

		[XmlAttribute(AttributeName = "heading")]
		public double Heading { get; set; }
	}

	[XmlRoot(ElementName = "LocatorMiddle")]
	public class LocatorMiddle
	{

		[XmlAttribute(AttributeName = "y")]
		public double Y { get; set; }

		[XmlAttribute(AttributeName = "x")]
		public double X { get; set; }

		[XmlAttribute(AttributeName = "latitude")]
		public double Latitude { get; set; }

		[XmlAttribute(AttributeName = "longitude")]
		public double Longitude { get; set; }
	}

	[XmlRoot(ElementName = "LocatorOuter")]
	public class LocatorOuter
	{

		[XmlAttribute(AttributeName = "y")]
		public double Y { get; set; }

		[XmlAttribute(AttributeName = "x")]
		public double X { get; set; }

		[XmlAttribute(AttributeName = "latitude")]
		public double Latitude { get; set; }

		[XmlAttribute(AttributeName = "longitude")]
		public double Longitude { get; set; }
	}

	[XmlRoot(ElementName = "LOC")]
	public class LOC
	{

		[XmlAttribute(AttributeName = "y")]
		public double Y { get; set; }

		[XmlAttribute(AttributeName = "x")]
		public double X { get; set; }

		[XmlAttribute(AttributeName = "latitude")]
		public double Latitude { get; set; }

		[XmlAttribute(AttributeName = "longitude")]
		public double Longitude { get; set; }
	}

	[XmlRoot(ElementName = "GS")]
	public class GS
	{

		[XmlAttribute(AttributeName = "y")]
		public double Y { get; set; }

		[XmlAttribute(AttributeName = "x")]
		public double X { get; set; }

		[XmlAttribute(AttributeName = "latitude")]
		public double Latitude { get; set; }

		[XmlAttribute(AttributeName = "longitude")]
		public double Longitude { get; set; }
	}

	[XmlRoot(ElementName = "ILS")]
	public class ILS
	{

		[XmlElement(ElementName = "LOC")]
		public LOC LOC { get; set; }

		[XmlElement(ElementName = "GS")]
		public GS GS { get; set; }
	}

	[XmlRoot(ElementName = "DirectCourse")]
	public class DirectCourse
	{

		[XmlElement(ElementName = "Threshold")]
		public Threshold Threshold { get; set; }

		[XmlElement(ElementName = "LocatorMiddle")]
		public LocatorMiddle LocatorMiddle { get; set; }

		[XmlElement(ElementName = "LocatorOuter")]
		public LocatorOuter LocatorOuter { get; set; }

		[XmlElement(ElementName = "ILS")]
		public ILS ILS { get; set; }
	}

	[XmlRoot(ElementName = "InverseCourse")]
	public class InverseCourse
	{

		[XmlElement(ElementName = "Threshold")]
		public Threshold Threshold { get; set; }

		[XmlElement(ElementName = "LocatorMiddle")]
		public LocatorMiddle LocatorMiddle { get; set; }

		[XmlElement(ElementName = "LocatorOuter")]
		public LocatorOuter LocatorOuter { get; set; }

		[XmlElement(ElementName = "ILS")]
		public ILS ILS { get; set; }
	}

	[XmlRoot(ElementName = "Runway")]
	public class Runway
	{

		[XmlElement(ElementName = "DirectCourse")]
		public DirectCourse DirectCourse { get; set; }

		[XmlElement(ElementName = "InverseCourse")]
		public InverseCourse InverseCourse { get; set; }

		[XmlAttribute(AttributeName = "length")]
		public double Length { get; set; }

		[XmlAttribute(AttributeName = "width")]
		public double Width { get; set; }
	}

	[XmlRoot(ElementName = "Aerodrome")]
	public class Aerodrome
	{

		[XmlElement(ElementName = "Runway")]
		public Runway[] Runway { get; set; }

		[XmlAttribute(AttributeName = "Guid")]
		public string Guid { get; set; }

		[XmlAttribute(AttributeName = "latitude")]
		public double Latitude { get; set; }

		[XmlAttribute(AttributeName = "longitude")]
		public double Longitude { get; set; }

		[XmlAttribute(AttributeName = "altitude")]
		public double Altitude { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "rusname")]
		public string Rusname { get; set; }

		[XmlAttribute(AttributeName = "country")]
		public string Country { get; set; }
	}


}
