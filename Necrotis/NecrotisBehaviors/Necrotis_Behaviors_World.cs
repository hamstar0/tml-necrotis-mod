using System;
using Terraria;
using CursedBrambles;


namespace Necrotis.NecrotisBehaviors {
	partial class NecrotisBehavior {
		internal static void ApplyWorldBehaviorsOn5TickIntervals( Player player, float necrotisPercent ) {
			if( necrotisPercent >= 1f ) {
				CursedBramblesAPI.SetPlayerToCreateBrambleWake( player, 64, 15 );
			} else {
				CursedBramblesAPI.UnsetPlayerToCreateBrambleWake( player );
			}
		}
	}
}
