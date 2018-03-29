using Harmony;
using DuckGame;

namespace Nametags
{
    [HarmonyPatch( typeof( ModLoader ) )]
    [HarmonyPatch( "<GetModHash>b__4" )]
    [HarmonyPatch( new[] { typeof( DuckGame.Mod ) } )]
    class ModLoader_GetModHash
    {
        static bool Prefix( ref bool __result, DuckGame.Mod a )
        {
            if ( a is Nametags.Mod )
            {
                __result = false;
                return false;
            }

            return true;
        }
    }
}
