using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;
using TacticalEditor.ModelsXml;
using TacticalEditor.VisualObject.VisAirport;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    class MieaPacket
    {
        private T_FP _fp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public PpmPoint[] Points = new PpmPoint[20];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public AirBasePoint[] AirBases = new AirBasePoint[20];
        private UdpClient _sendClient;

        public MieaPacket()
        {
            _sendClient = new UdpClient();
            _fp = new T_FP();
            var t = DateTime.Now;
            string hh = "Route" + t.Day + t.Millisecond;
            _fp.NameRoute= new char[10];
            hh.ToCharArray().CopyTo(_fp.NameRoute, 0);
            _fp.WayPoint = new T_PPM_FP[149];
            EventsHelper.PpmCollectionEvent += OnPpmCollectionEvent;
            EventsHelper.AirBaseCollectionEvent += OnAirBaseCollectionEvent;
            EventsHelper.ChangeAirportEvent += OnChangeAirportEvent;
        }

        private void OnChangeAirportEvent(AirBasePoint e)
        {
            _fp.Departure = SetAirport(e);
            _fp.Arrival = SetAirport(e);
        }


        private void OnAirBaseCollectionEvent(AirBasePoint[] airbases)
        {
            AirBases = airbases;
            for (int i = 0; i < airbases.Length; i++)
                if (airbases[i] != null && airbases[i].AirportInfo.ActiveAirport)
                {
                    _fp.Departure= SetAirport(airbases[i]);
                    _fp.Arrival= SetAirport(airbases[i]);
                }
        }

        private void OnPpmCollectionEvent(PpmPoint[] ppmPoints)
        {
            int countPpm = 0;
            Points = ppmPoints;
            for(int i = 0; i < _fp.WayPoint.Length; i++)
                    _fp.WayPoint[i] = new T_PPM_FP();


            for (int i = 0; i < ppmPoints.Length; i++)
                if (ppmPoints[i] != null)
                {
                    _fp.WayPoint[i] = SetPpm(ppmPoints[i]);
                    countPpm++;
                }
            _fp.PM = countPpm;
            var dgram = ConvertHelper.ObjectToByte(_fp);
            _sendClient?.Send(dgram, dgram.Length, "127.0.0.1", 11180);

        }

        private T_Airport_FP SetAirport(AirBasePoint airpoints)
        {
            T_Airport_FP Airport = new T_Airport_FP();
            Airport.KOD = new char[4];
            Airport.NAME = new char[25];
            Airport.KOD = "LPSK".ToCharArray();
            Airport.LAT =  airpoints.NavigationPoint.GeoCoordinate.Latitude;
            Airport.LON = airpoints.NavigationPoint.GeoCoordinate.Longitude;
            Airport.dM = 0;
            Airport.Elev = airpoints.NavigationPoint.GeoCoordinate.H;
            Array.Copy(airpoints.AirportInfo.Name, Airport.NAME, airpoints.AirportInfo.Name.Length);
            Airport.end_code = 06;
            Airport.end_code_side = 3;
            Airport.LAT_RWY = airpoints.AirportInfo.Runway.Threshold.Latitude;
            Airport.LON_RWY = airpoints.AirportInfo.Runway.Threshold.Longitude;
            Airport.dM_RWY = 0;
            Airport.Elev_RWY = 0;
            Airport.L_RWY = airpoints.AirportInfo.Runway.Length;
            Airport.Brg_RWY = airpoints.AirportInfo.Runway.Heading;
            return Airport;
        }

        private T_PPM_FP SetPpm(PpmPoint ppm)
        {
            T_PPM_FP wayPoint;
            wayPoint.KOD = new char[7];
            Array.Copy(ppm.NavigationPoint.Name, wayPoint.KOD, wayPoint.KOD.Length);
            wayPoint.LAT = ppm.NavigationPoint.GeoCoordinate.Latitude;
            wayPoint.LON = ppm.NavigationPoint.GeoCoordinate.Longitude;
            wayPoint.dM = 0;
            wayPoint.Hz = 0;
            wayPoint.VIz = 0;
            return wayPoint;
        }


        public byte[] GetByte()
        {
            return ConvertHelper.ObjectToByte(_fp);
        }


        /**
        * \brief Структура аэродрома и ВПП
        **/
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        struct T_Airport_FP
        {
          [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
          public char[] KOD;                 /* Kод аэродрома												-	[б/р]*/
          public double LAT;                 /* Широта  географическая аэродрома							+/-90		[град]*/
          public double LON;                 /* Долгота  географическая аэродрома							+/-180		[град]*/
          public double dM;                  /* Maгнитнoe cклонениe аэродрома								+/-180		[град]*/
          public double Elev;                /* Превышeние аэродрома										-300-3000	[м]*/
          [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
          public char[] NAME;                /* Нaзвaниe аэродрома											-			[б/р]*/
          public byte end_code;              /* Код порога ВПП (1…36, 0-нет данных)						0-36		[б/р]*/
          public byte end_code_side;         /* Расположение ВПП (0-нет, 1-левая, 2-правая, 3-центральная)	0-3			[б/р]*/
          public double LAT_RWY;             /* Широта торца ВПП												+/-90		[град]*/
          public double LON_RWY;             /* Долгота торца ВПП											+/-180		[град]*/
          public double dM_RWY;              /* Maгнитнoe cклонениe торца ВПП								+/-180		[град]*/
          public double Elev_RWY;            /* Превышeние торца ВПП											-300-3000	[м]*/
          public double L_RWY;               /* Длинa  ВПП													0-10000		[м]*/
          public double Brg_RWY;             /* Напрaвлeние ВПП истинное										+/-180		[град]*/
        };

        /**
        * \brief Структура ППМ
        */
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        struct T_PPM_FP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] KOD;                 /* Kод ППM				-		[б/р]*/
            public double LAT;                 /* Широта ППM			+/-90	[град] */
            public double LON;                 /* Долгота ППM			+/-180	[град]*/
            public float dM;                   /* Maгнитнoe cклонениe	+/-180	[град]*/
            public float Hz;                   /* Заданная высота		0-15000	[м]*/
            public float VIz;                  /* Заданная истинная	0-330	[м/с]*/
        };
        /**
        * \brief Структура загружаемого маршрута
        */
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        class T_FP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] NameRoute; /**< Имя загружаемого маршрута			                -		[б/р]*/
            public int PM; /**< Количество ППМ в загружаемом маршруте	                    0-149	[б/р]*/
            public T_Airport_FP Departure; /**< Структура аэродрома вылета                  -		[б/р]*/
            public T_Airport_FP Arrival; /**< Структура аэродрома посадки                   -		[б/р]*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 149)]
            public T_PPM_FP[] WayPoint; /**< Структура ППМ маршрута                         -		[б/р]*/
        }
    }
}