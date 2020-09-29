using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Necrotis.Buffs {
	partial class EnlivenedBuff : ModBuff {
		public static bool CanBuff( Player player, float necrotisResistPercent ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			return necrotisResistPercent >= config.EnlivenedNecrotisResistPercentMinimum;
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Enlivened" );
			this.Description.SetDefault(
				"You feel strangely lively"
				+ "\n" + "Max speed increased 15%"
			);

			Main.debuff[this.Type] = false;
			Main.buffNoTimeDisplay[this.Type] = true;
			Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			float moveEffectPercent = config.EnlivenedMovementPercent;

			player.maxRunSpeed *= moveEffectPercent;
			player.accRunSpeed = player.maxRunSpeed;
			player.moveSpeed *= moveEffectPercent;
		}
	}
}
