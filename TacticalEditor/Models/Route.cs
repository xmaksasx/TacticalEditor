using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TacticalEditor.Models
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Route
    {
        public double CountAirPoints;
        public List<AirPoint> AirPoints = new List<AirPoint>();

        public byte[] ToByte(Route route)
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(CountAirPoints));
            foreach (var airPoint in route.AirPoints)
                result.AddRange(ObjectToByte(airPoint));
            return result.ToArray();
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
