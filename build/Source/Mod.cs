using System.Reflection;
using HarmonyLib;

namespace Nametags
{
	public class NameTagsMod : DuckGame.ClientMod
	{
		private Harmony HarmonyInstance { get; }

		public NameTagsMod()
		{
			HarmonyInstance = new Harmony( GetType().Namespace );
		}

		protected override void OnPreInitialize()
		{
#if false
			System.Diagnostics.Debugger.Launch();
#endif

			HarmonyInstance.PatchAll( Assembly.GetExecutingAssembly() );
		}
	}
}
