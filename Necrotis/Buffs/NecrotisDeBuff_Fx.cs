using System;
using Terraria;
using Terraria.ModLoader;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		public static void ApplyVisualFX( Player player, ref float r, ref float g, ref float b ) {
			r *= 0.7f;
			//g *= 0.85f;
			b *= 0.7f;
		}
	}
}
