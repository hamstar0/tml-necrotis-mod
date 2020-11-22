﻿using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace Necrotis.Buffs {
	partial class NecrotisNatusDeBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis Natus" );
			this.Description.SetDefault(
				"You begin feel horriby drained"
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
			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, necrotisPercent );
		}
	}
}
