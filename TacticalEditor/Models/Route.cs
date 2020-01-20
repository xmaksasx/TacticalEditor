using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;

namespace TacticalEditor.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Route: Header
    {
        public long CountPoints;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public AirPoint[] AirPoints = new AirPoint[20];

        public Route()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
        }
      
        private void PpmCollection(AirPoint[] airPoint)
        {
            AirPoints = airPoint;
            CountPoints = airPoint.Length;
        }

        public byte[] GetByte()
        {
            List<byte> result = new List<byte>();

            result.AddRange(Head);
            result.AddRange(BitConverter.GetBytes(CountPoints));
            for (int i = 0; i < AirPoints.Length; i++)
                if (AirPoints[i] == null)
                    result.AddRange(ObjectToByte(new AirPoint() {Type = -1}));
                else
                    result.AddRange(ObjectToByte(AirPoints[i]));
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
