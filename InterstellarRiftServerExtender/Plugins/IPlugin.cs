using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace IRSE.Plugins
{
	public interface IPlugin
	{
		#region Fields
		#endregion

		#region Events
		#endregion

		#region Properties
		Guid Id
		{ get; }
		string Name
		{ get; }
		string Version
		{ get; }
		#endregion

		#region Methods
		void Init(String ModDirectory);
		void Shutdown();
		#endregion
	}
}
