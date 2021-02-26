using System;
using Terraria;
using Terraria.ModLoader;


namespace Necrotis.Buffs {
	public partial class EnlivenedBuff : ModBuff {
		public static bool CanBuff( Player player, float animaPercent ) {
			var config = NecrotisConfig.Instance;
			float minBuffPerc = config.Get<float>( nameof(config.EnlivenedAnimaPercentMinimum) );

			return animaPercent >= minBuffPerc;
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Enlivened" );
			this.Description.SetDefault(
				"You feel strangely lively"
				+ "\nMax speed increased 15%"
			);

			Main.debuff[this.Type] = false;
			Main.buffNoTimeDisplay[this.Type] = true;
			Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			float moveEffectPercent = config.Get<float>( nameof(config.EnlivenedMovementPercent) );

			player.maxRunSpeed *= moveEffectPercent;
			player.accRunSpeed = player.maxRunSpeed;
			player.moveSpeed *= moveEffectPercent;
		}
	}
}
