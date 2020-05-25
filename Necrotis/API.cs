using System;
using Terraria;
using static Terraria.ModLoader.ModContent;


namespace Necrotis {
	public static class NecrotisAPI {
		public static void AddNecrotisAffliction( Player player, float percent, bool quiet ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.AfflictNecrotis( percent, quiet );
		}
	}
}
