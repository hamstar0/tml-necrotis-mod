using System;
using Terraria;


namespace Necrotis {
	public static class NecrotisAPI {
		public static void SubtractAnimaPercentFromPlayer( Player player, float percent, bool quiet, bool sync=true ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.SubtractAnimaPercent( percent, quiet, sync );
		}
	}
}
