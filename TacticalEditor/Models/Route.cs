using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;

namespace TacticalEditor.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Route: Header
    {

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public InfoPoint[] Points = new InfoPoint[20];
        public long CountPoints;

        public Route()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
        }
      
        private void PpmCollection(InfoPoint[] airPoint)
        {
            Points = airPoint;
            CountPoints = airPoint.Length;
        }

        public byte[] GetByte()
        {
            List<byte> result = new List<byte>();
            result.AddRange(Head);
            for (int i = 0; i < Points.Length; i++)
                if (Points[i] == null)
                    result.AddRange(ObjectToByte(new InfoPoint() {Type = -1}));
                else
                    result.AddRange(ObjectToByte(Points[i]));
            result.AddRange(BitConverter.GetBytes(CountPoints));
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
