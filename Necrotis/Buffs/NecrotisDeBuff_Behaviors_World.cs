using System;
using Terraria;
using Terraria.ModLoader;
using CursedBrambles;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		private static void ApplyWorldBehaviors( Player player, float necrotisPercent ) {
			NecrotisDeBuff.ApplyWorldStalkingBramblesBehavior( player, necrotisPercent >= 1f );
		}


		private static void ApplyWorldStalkingBramblesBehavior( Player player, bool isMaxNecrotis ) {
			if( isMaxNecrotis ) {
				CursedBramblesAPI.SetPlayerToCreateBrambleWake( player, 64, 15 );
			} else {
				CursedBramblesAPI.UnsetPlayerToCreateBrambleWake( player );
			}
		}
	}
}
