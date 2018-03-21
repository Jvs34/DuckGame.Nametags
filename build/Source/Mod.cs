using System.Reflection;
using System.Threading;
using Harmony;
using System.Diagnostics;

namespace Nametags
{
	public class Mod : DuckGame.DisabledMod
	{
		protected override void OnPreInitialize()
		{
#if false
			Debugger.Launch();
#endif

			HarmonyInstance.Create( "Nametags" ).PatchAll( Assembly.GetExecutingAssembly() );
		}
	}
}
