using System;
using Terraria;
using Terraria.ModLoader;
using Necrotis.NecrotisBehaviors;


namespace Necrotis.Buffs {
	public partial class NecrotisNatusDeBuff : ModBuff {
		private static string BaseDescription = "You begin feeling horriby drained";
				//+ "\n" + "Reduces max life, speed, life regen";



		////////////////

		public static bool CanBuff( Player player, float animaPercent ) {
			return animaPercent > 0f && animaPercent <= 0.5f;
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis Natus" );
			this.Description.SetDefault( NecrotisNatusDeBuff.BaseDescription );

			Main.debuff[this.Type] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			float necrotisPercent = myplayer.NecrotisPercent;

			this.UpdateNecrotisBehaviors( player, necrotisPercent );
		}

		private void UpdateNecrotisBehaviors( Player player, float necrotisPercent ) {
			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, necrotisPercent, out float movePercent );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, necrotisPercent, out int maxHpLost );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, necrotisPercent );

			int regenEffectPercOf100 = (int)((1f - necrotisPercent) * 100f);
			int movePercOf100 = (int)(movePercent * 100f);

			this.Description.SetDefault( NecrotisNatusDeBuff.BaseDescription
				+ "\n" + "Max health reduced to "+player.statLifeMax2+" (of "+(player.statLifeMax2 + maxHpLost)+")"
				+ "\n" + "Health regeneration reduced to "+regenEffectPercOf100+"%"
				+ "\n" + "Movement speed reduced to "+movePercOf100+"%"
			);
		}
	}
}
