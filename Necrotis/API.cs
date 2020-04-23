using System;
using Terraria;
using static Terraria.ModLoader.ModContent;


namespace Necrotis {
	public static class NecrotisAPI {
		public static void SetNecrotisResist( Player player, float percent ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.NecrotisResistPercent = percent;
		}
	}
}
