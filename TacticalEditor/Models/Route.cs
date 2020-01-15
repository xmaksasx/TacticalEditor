using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace TacticalEditor.Models
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Route
    {
        public double CountAirPoints;
        public List<AirPoint> AirPoints = new List<AirPoint>();

        public byte[] GetByte()
        {
            CountAirPoints = 120;
            AirPoints.Add(new AirPoint() { H = 23.444 });
            var bytes =  Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
            Route route = JsonConvert.DeserializeObject<Route>(Encoding.UTF8.GetString(bytes));
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
        }

        public static byte[] ObjectToByte<T>(T obj)
        {
            var size = Marshal.SizeOf(obj);
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, false);
            Marshal.Copy(ptr, bytes, 0, size);
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }
    }
}
