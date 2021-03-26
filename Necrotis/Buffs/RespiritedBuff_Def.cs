using System;
using Terraria;
using Terraria.ModLoader;


namespace Necrotis.Buffs {
	public partial class RespiritedBuff : ModBuff {
		public override void SetDefaults() {
			var config = NecrotisConfig.Instance;

			string desc;
			if( config.Get<float>( nameof(config.ElixirAnimaDrainMultiplier) ) > 0f ) {
				desc = "reduced";
			} else {
				desc = "nullified";
			}

			this.DisplayName.SetDefault( "Respirited" );
			this.Description.SetDefault(
				"You feel one with the universe"
				+"\nAll anima drain effects "+desc
			);

			Main.debuff[this.Type] = false;
			Main.buffNoTimeDisplay[this.Type] = false;
			Main.buffNoSave[this.Type] = false;
		}
	}
}
