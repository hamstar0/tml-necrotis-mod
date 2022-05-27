using System;
using Terraria;
using Terraria.ModLoader;


namespace Necrotis.Buffs {
	public partial class RespiritedBuff : ModBuff {
		public override void SetDefaults() {
			var config = NecrotisConfig.Instance;
			float drainMul = config.Get<float>( nameof(config.ElixirAnimaDrainMultiplier) );

			string desc;
			if( drainMul > 0f && drainMul < 1f ) {
				desc = "reduced";
			} else if( drainMul > 1f ) {
				desc = "increased";
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
