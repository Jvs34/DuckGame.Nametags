using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using DuckGame;

namespace Nametags
{
	[HarmonyPatch( typeof( GameLevel ), nameof( GameLevel.PostDrawLayer ) )]
	internal static class GameLevel_PostDrawLayer
	{
		static readonly FieldInfo _gameMode = typeof( GameLevel ).GetField( "_mode", BindingFlags.NonPublic | BindingFlags.Instance );
		static readonly FieldInfo _waitDings = typeof( GameMode ).GetField( "_waitAfterSpawnDings", BindingFlags.NonPublic | BindingFlags.Instance );

		private static void Postfix( GameLevel __instance, Layer layer )
		{
			if( layer != Layer.Foreground )
			{
				return;
			}

			//level is not ready yet, so it may not be safe to get anything from it in multiplayer
			if( ( Network.isActive && __instance.networkStatus != NetLevelStatus.Ready ) || !__instance.initialized )
			{
				return;
			}

			GameMode gm = (GameMode) _gameMode.GetValue( __instance );

			int waitAfterDings = (int) _waitDings.GetValue( gm );

			if( waitAfterDings > 2 )
			{
				return;
			}

			foreach( var profile in Profiles.activeNonSpectators )
			{
				DrawName( profile );
			}
		}

		private static void DrawName( Profile profile )
		{
			var duck = profile.duck;
			var font = profile.font;
			var name = profile.name;

			if( duck == null )
			{
				return;
			}

			font.scale = Vec2.One;
			var fontPos = new Vec2( duck.position.x - font.GetWidth( name ) / 2.0f, duck.top - 16.0f );
			profile.font.DrawOutline( name, fontPos, profile.persona.colorUsable, Color.Black, 2.0f );
		}
	}

}
