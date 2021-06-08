using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAerodrome;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.Send
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ListOfNavigationPoint : Header
    {
        public int CountPoints;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public PpmPoint[] Points = new PpmPoint[20];

        private byte[] _head;

        MeasureHelper _measureHelper = new MeasureHelper();
        CoordinateHelper _coordinateHelper = new CoordinateHelper();
        AircraftPosition _aircraft = new AircraftPosition();
        AerodromePoint _aerodromePoint = new AerodromePoint();



        public ListOfNavigationPoint()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
            EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
            EventsHelper.ChangeAircraftCoordinateEvent += ChangeAircraftCoordinate;
            _head = GetHead("TacticalEditor_PpmPoints");
        }

        private void ChangeAerodrome(AerodromePoint e)
        {
            _aerodromePoint = e;
        }

        private void ChangeAircraftCoordinate(AircraftPosition aircraft)
        {
            _aircraft = aircraft;

        }

        private void PpmCollection(PpmPoint[] ppmPoints)
        {
            CountPoints = 0;
            Points = ppmPoints;
            foreach (var ppmPoint in ppmPoints)
                if (ppmPoint != null)
                    CountPoints++;
        }

        public byte[] GetByte()
        {

            List<byte> result = new List<byte>();
            result.AddRange(_head);
            result.AddRange(BitConverter.GetBytes(CountPoints));
            for (int i = 0; i < Points.Length; i++)
                result.AddRange(Points[i] == null
                    ? ConvertHelper.ObjectToByte(new NavigationPoint() {Type = -1})
                    : ConvertHelper.ObjectToByte(Points[i].NavigationPoint));
            return result.ToArray();
        }
    }
}