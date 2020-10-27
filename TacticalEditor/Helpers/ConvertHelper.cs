using System;
using System.Runtime.InteropServices;

namespace TacticalEditor.Helpers
{
    class ConvertHelper
    {
        public static void ByteToObject<T>(byte[] receiveBytes, T obj)
        {
            int len = Marshal.SizeOf(obj);
            IntPtr i = Marshal.AllocHGlobal(len);
            Marshal.Copy(receiveBytes, 0, i, len);
            Marshal.PtrToStructure(i, obj);
            Marshal.FreeHGlobal(i);
        }

        public static byte[] ObjectToByte(object obj)
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
