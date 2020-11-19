using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Necrotis.Buffs {
	partial class ElixirBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Elixir" );
			this.Description.SetDefault(
				"You feel one with the universe"
				+"\nAnima drain effect reduced"
			);

			Main.debuff[this.Type] = false;
			Main.buffNoTimeDisplay[this.Type] = false;
			Main.buffNoSave[this.Type] = false;
		}
	}
}
