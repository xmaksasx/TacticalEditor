using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TacticalEditor.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Header
    {
        // <summary>
        // всего заголовок 68 символов
        // TacticalEditor UTF8toDecimal
        [Description("header")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 68)]
        public byte[] Head = new byte[]
        {
            84, 97, 99, 116, 105, 99, 97, 108, 69, 100, 105, 116, 111, 114, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
    }
}
