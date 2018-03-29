using System.Reflection;
using Harmony;

namespace Nametags
{
	public class Mod : DuckGame.Mod
	{
		protected override void OnPreInitialize()
		{
#if false
			System.Diagnostics.Debugger.Launch();
#endif

			HarmonyInstance.Create( "Nametags" ).PatchAll( Assembly.GetExecutingAssembly() );
		}
	}
}
