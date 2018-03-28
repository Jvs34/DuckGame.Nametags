using System;
using System.Linq;
using System.Reflection;
using Harmony;
using DuckGame;

namespace Nametags
{
	[HarmonyPatch( typeof( GameLevel ) )]
	[HarmonyPatch( nameof( GameLevel.PostDrawLayer ) )]
	[HarmonyPatch( new [] { typeof( Layer ) } )]
	class GameLevel_PostDrawLayer
	{
		static FieldInfo _gameMode = typeof( GameLevel ).GetField( "_mode" , BindingFlags.NonPublic | BindingFlags.Instance );
		static FieldInfo _waitDings = typeof( GameMode ).GetField( "_waitAfterSpawnDings" , BindingFlags.NonPublic | BindingFlags.Instance );

		private static void Postfix( GameLevel __instance , Layer layer )
		{
			if( layer != Layer.Foreground )
				return;

			//level is not ready let, so it may not be safe to get anything from it yet in multiplayer
			if( (Network.isActive && __instance.networkStatus != NetLevelStatus.Ready ) || !__instance.initialized )
				return;

			GameMode gm = (GameMode) _gameMode.GetValue( __instance );

			int waitAfterDings = (int) _waitDings.GetValue( gm );

			if( waitAfterDings > 2 )
				return;

			foreach( var profile in Profiles.active )
			{
				DrawName( profile );
			}
		}

		private static void DrawName( Profile profile )
		{
			if( profile.duck == null )
				return;

			var duck = profile.duck;
			var font = profile.font;
			var name = profile.name;

			font.scale = Vec2.One;
			var fontPos = new Vec2( duck.position.x - font.GetWidth( name ) / 2.0f , duck.top - 16.0f );
			profile.font.DrawOutline( name , fontPos , profile.persona.colorUsable , Color.Black , 1.0f );
		}
	}

}
