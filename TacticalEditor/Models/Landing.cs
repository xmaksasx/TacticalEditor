using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TacticalEditor.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Landing:Header
    {
        [Description("Удаление от торца ВПП")]
        public double DistanceToRwy;
        [Description("Признак прохода БПРМ")]
        public double PassedLocatorMiddle;
        [Description("Признак прохода ДПРМ")]
        public double PassedLocatorOuter;
        [Description("Положение курсой планки")]
        public double IndicatorLoc;
        [Description("Положение глиссадной планки")]
        public double IndicatorGs;
        [Description("Признак отображения курсовой планки")]
        public double IndicatorLocIsVisible;
        [Description("Признак отображения глиссадной планки")]
        public double IndicatorGsIsVisible;
        [Description("Курс на ДПРМ")]
        public double CourseOM;
        [Description("Удаление от ДПРМ")]
        public double DistanceToOM;
    }
}
