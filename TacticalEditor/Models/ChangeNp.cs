using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TacticalEditor.Models
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	class ChangeNp: Header
	{
		[Description("Действие")]
		// 1 - сделать np по текущему idNp исполнительной
		// 2 - изменить np по текущему idNp 
		// 2 - удалить np по текущему idNp 
		public double Action;
		[Description("Тип навигационной точки")]
		// 1 - поворотный пункт маршрута
		// 2 - аэродром
		public double TypeOfNp;
		[Description("Идентификатор точки")]
		public double IdNp;
	}
}
