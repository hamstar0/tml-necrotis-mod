using System;
using Terraria;
using Terraria.ModLoader;
using Necrotis.NecrotisBehaviors;


namespace Necrotis.Buffs {
	public partial class NecrotisNatusDeBuff : ModBuff {
		public static bool CanBuff( Player player, float animaPercent ) {
			return animaPercent > 0f && animaPercent <= 0.5f;
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis Natus" );
			this.Description.SetDefault(
				"You begin feeling horriby drained"
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

			this.UpdateNecrotisBehaviors( player, necrotisPercent );
		}

		private void UpdateNecrotisBehaviors( Player player, float necrotisPercent ) {
			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, necrotisPercent );
		}
	}
}
