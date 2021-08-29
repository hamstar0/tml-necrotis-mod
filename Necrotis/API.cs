using System;
using Terraria;


namespace Necrotis {
	public delegate void AnimaChangeHook( Player player, float oldPercent, ref float percentLost, ref bool quiet );




	public static class NecrotisAPI {
		public static float GetAnimaPercentOfPlayer( Player player ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			return myplayer.AnimaPercent;
		}


		public static void SubtractAnimaPercentFromPlayer( Player player, float percent, bool quiet, bool sync=true ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.SubtractAnimaPercent( percent, quiet, sync );
		}


		////
		
		public static void AddAnimaChangeHook( AnimaChangeHook hook ) {
			NecrotisMod.Instance.AnimaChangeHooks.Add( hook );
		}
	}
}
