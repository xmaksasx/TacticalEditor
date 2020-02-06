using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TacticalEditor.Helpers;
using TacticalEditor.Models.NavPoint;

namespace TacticalEditor.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Route: Header
    {

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public NavigationPoint[] Points = new NavigationPoint[20];
        public long CountPoints;

        public Route()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
        }
      
        private void PpmCollection(NavigationPoint[] airPoint)
        {
            Points = airPoint;
            CountPoints = airPoint.Length;
        }

        public byte[] GetByte()
        {
            List<byte> result = new List<byte>();
            CountPoints = 0;
            result.AddRange(Head);
            for (int i = 0; i < Points.Length; i++)
                if (Points[i] == null)
                    result.AddRange(ObjectToByte(new NavigationPoint(){TypePpm = -1}));
                else
                {
                    result.AddRange(ObjectToByte(Points[i]));
                    CountPoints++;
                }
            
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
