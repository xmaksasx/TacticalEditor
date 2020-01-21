using System.Runtime.InteropServices;

namespace TacticalEditor.Models.NavPoint
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Measure
    {
        /// <summary>
        /// курс заданный на НТ
        /// </summary>
        public double Psi { get; set; }

        /// <summary>
        /// заданный путевой угол на НТ
        /// </summary>
        public double PsiPath { get; set; }

        /// <summary>
        /// дистанция относительно ЛА
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// оставшееся время полета до НТ (сек)
        /// </summary>
        public double RemainingTime;
    }
}
