using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.Timers;
using CursedBrambles;


namespace Necrotis.NecrotisBehaviors {
	partial class NecrotisBehavior {
		internal static void ApplyWorldBehaviors( Player player ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			string timerName = "NecrotisOmnisDeBuff_" + player.whoAmI;

			if( Timers.GetTimerTickDuration(timerName) > 0 ) {
				return;
			}

			Timers.SetTimer( timerName, 5, false, () => {
				var myplayer = player.GetModPlayer<NecrotisPlayer>();
				bool hasFullNecrotis = myplayer.NecrotisPercent >= 1f;

				if( hasFullNecrotis ) {
					CursedBramblesAPI.SetPlayerToCreateBrambleWake( player, true, 64, 15 );
					return true;
				} else {
					CursedBramblesAPI.UnsetPlayerBrambleWakeCreating( player );
					return false;
				}
			} );
		}
	}
}
