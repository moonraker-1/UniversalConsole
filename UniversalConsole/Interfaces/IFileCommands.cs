using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Tool_Kit.Interfaces
{
	internal interface IFileCommands
	{
		public enum Commands
		{
			CONVERTDOCPDF,
			CONVERTPDFDOC,
			BCOLOR,
			FCOLOR,
			OPEN,
			CLOSE,
			CREATE,
			DELETE,
			SWEEP,
			MOVE,
			DIR,
			SETDIR,
			UP,
			ROOT,
			RENAME
		}

	}
}
