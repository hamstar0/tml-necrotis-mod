using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		public static void ApplyWorldBehaviors( Player player, float necrotisPercent ) {
			if( necrotisPercent == 0f ) {
				NecrotisDeBuff.ApplyWorldStalkingBramblesBehavior( player );
			}
		}


		public static void ApplyWorldStalkingBramblesBehavior( Player player ) {
			f
		}
	}
}
