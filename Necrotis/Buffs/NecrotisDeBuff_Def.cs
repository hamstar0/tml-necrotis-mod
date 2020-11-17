using HamstarHelpers.Services.Timers;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis" );
			this.Description.SetDefault(
				"You feel horriby drained"
				+ "\n" + "Reduces max life, speed, life regen"
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
			NecrotisDeBuff.ApplyPlayerMovementBehaviors( player, necrotisPercent );
			NecrotisDeBuff.ApplyPlayerJumpingBehaviors( player, necrotisPercent );
			NecrotisDeBuff.ApplyPlayerHealthBehaviors( player, necrotisPercent );
			NecrotisDeBuff.ApplyPlayerDebuffBehaviors( player, necrotisPercent );

			if( necrotisPercent >= 1f ) {
				string timerName = "NecrotisDeBuff_" + player.whoAmI;

				if( Timers.GetTimerTickDuration(timerName) == 0 ) {
					Timers.SetTimer( timerName, 5, false, () => {
						NecrotisDeBuff.ApplyWorldBehaviors( player, necrotisPercent );

						return necrotisPercent >= 1f;
					} );
				}
			}
		}
	}
}
