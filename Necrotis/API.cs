using System;
using Terraria;


namespace Necrotis {
	public static class NecrotisAPI {
		public static void SubtractAnimaPercentFromPlayer( Player player, float percent, bool quiet ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.SubtractAnimaPercent( percent, quiet );
		}
	}
}
