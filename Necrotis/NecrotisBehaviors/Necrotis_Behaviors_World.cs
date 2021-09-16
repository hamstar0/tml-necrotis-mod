using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using CursedBrambles;
using Necrotis.Buffs;


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

				if( !player.dead && NecrotisOmnisDeBuff.CanBuff(player, myplayer.AnimaPercent) ) {
					CursedBramblesAPI.SetPlayerToCreateBrambleWake(
						player: player,
						isElevationChecked: true,
						radius: 64,
						tickRate: 10,
						validateAt: CursedBramblesAPI.CreatePlayerAvoidingBrambleValidator( 4 )
					);
				} else {
					CursedBramblesAPI.UnsetPlayerBrambleWakeCreating( player );
				}
				return false;
			} );
		}
	}
}
