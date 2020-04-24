using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis" );
			this.Description.SetDefault(
				"You feel your life energy diminished"
				+ "\n" + "Reduces max life, speed, life regen"
			);

			Main.debuff[this.Type] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();

			float percent = -myplayer.NecrotisResistPercent;
			if( percent <= 0f ) {
				return;
			}

			NecrotisDeBuff.ApplyEffect( player, percent );
		}
	}
}
