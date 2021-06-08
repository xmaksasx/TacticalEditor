using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;
using TacticalEditor.VisualObject.VisAerodrome;
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
        public AerodromePoint[] Aerodromes = new AerodromePoint[20];
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
            EventsHelper.AerodromeCollectionEvent += OnAerodromeCollectionEvent;
            EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
        }

        private void ChangeAerodrome(AerodromePoint e)
        {
            _fp.Departure = SetAerodrome(e);
            _fp.Arrival = SetAerodrome(e);
        }


        private void OnAerodromeCollectionEvent(AerodromePoint[] aerodromePoints)
        {
            Aerodromes = aerodromePoints;
            for (int i = 0; i < aerodromePoints.Length; i++)
                if (aerodromePoints[i] != null && aerodromePoints[i].AerodromeInfo.ActiveAerodrome)
                {
                    _fp.Departure= SetAerodrome(aerodromePoints[i]);
                    _fp.Arrival= SetAerodrome(aerodromePoints[i]);
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

        private T_Aerodrome_FP SetAerodrome(AerodromePoint airpoints)
        {
            T_Aerodrome_FP Aerodrome = new T_Aerodrome_FP();
            Aerodrome.KOD = new char[4];
            Aerodrome.NAME = new char[25];
            Aerodrome.KOD = "LPSK".ToCharArray();
            Aerodrome.LAT =  airpoints.NavigationPoint.GeoCoordinate.Latitude;
            Aerodrome.LON = airpoints.NavigationPoint.GeoCoordinate.Longitude;
            Aerodrome.dM = 0;
            Aerodrome.Elev = airpoints.NavigationPoint.GeoCoordinate.H;
            Array.Copy(airpoints.AerodromeInfo.Name, Aerodrome.NAME, Aerodrome.NAME.Length);
            Aerodrome.end_code = 06;
            Aerodrome.end_code_side = 3;
            Aerodrome.LAT_RWY = airpoints.AerodromeInfo.Runway.Threshold.Latitude;
            Aerodrome.LON_RWY = airpoints.AerodromeInfo.Runway.Threshold.Longitude;
            Aerodrome.dM_RWY = 0;
            Aerodrome.Elev_RWY = 0;
            Aerodrome.L_RWY = airpoints.AerodromeInfo.Runway.Length;
            Aerodrome.Brg_RWY = airpoints.AerodromeInfo.Runway.Heading;
            return Aerodrome;
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
        struct T_Aerodrome_FP
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
            public T_Aerodrome_FP Departure; /**< Структура аэродрома вылета                  -		[б/р]*/
            public T_Aerodrome_FP Arrival; /**< Структура аэродрома посадки                   -		[б/р]*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 149)]
            public T_PPM_FP[] WayPoint; /**< Структура ППМ маршрута                         -		[б/р]*/
        }
    }
}