using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using TacticalEditor.Helpers;

namespace TacticalEditor.Models
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Route
    {
        public double CountAirPoints;
        public List<AirPoint> AirPoints = new List<AirPoint>();

        public Route()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
        }

        private void PpmCollection(List<AirPoint> airPoint)
        {
            CountAirPoints++;
            AirPoints = airPoint;
        }


        public byte[] GetByte()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
        }
    }
}
