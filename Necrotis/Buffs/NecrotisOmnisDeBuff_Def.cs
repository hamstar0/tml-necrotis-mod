using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace Necrotis.Buffs {
	partial class NecrotisOmnisDeBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis Omnis" );
			this.Description.SetDefault(
				"You feel horriby drained"
				+ "\n" + "Reduces max life, speed, life regen"
				+ "\n" + "You are now being stalked by a shadow when in deep places"
			);

			Main.debuff[this.Type] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			float necrotisPercent = myplayer.NecrotisPercent;

			if( necrotisPercent > 0f ) {
				this.UpdateNecrotisBehaviors( player, necrotisPercent );
			}
		}

		private void UpdateNecrotisBehaviors( Player player, float necrotisPercent ) {
			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, necrotisPercent );

			if( necrotisPercent >= 1f ) {
				string timerName = "NecrotisOmnisDeBuff_" + player.whoAmI;

				if( Timers.GetTimerTickDuration(timerName) == 0 ) {
					Timers.SetTimer( timerName, 5, false, () => {
						NecrotisBehavior.ApplyWorldBehaviorsOn5TickIntervals( player, necrotisPercent );

						return necrotisPercent >= 1f;
					} );
				}
			}
		}
	}
}
