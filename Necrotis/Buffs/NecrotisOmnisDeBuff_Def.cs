using System;
using Terraria;
using Terraria.ModLoader;
using Necrotis.NecrotisBehaviors;


namespace Necrotis.Buffs {
	public partial class NecrotisOmnisDeBuff : ModBuff {
		private static string BaseDescription =
				"You feel horriby drained"
				+ "\n" + "You are now being stalked by a shadow when in deep places";



		////////////////

		public static bool CanBuff( Player player, float animaPercent ) {
			return animaPercent <= 0f;
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis Omnis" );
			this.Description.SetDefault( NecrotisOmnisDeBuff.BaseDescription );

			Main.debuff[ this.Type ] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			this.UpdateBehaviors( player );
		}

		private void UpdateBehaviors( Player player ) {
			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, 1f, out float movePercent );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, 1f );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, 1f, out int maxHpLost );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, 1f );
			NecrotisBehavior.ApplyWorldBehaviors( player );

			int movePercOf100 = (int)( movePercent * 100f );

			this.Description.SetDefault( NecrotisOmnisDeBuff.BaseDescription
				+ "\n" + "Max health reduced to " + player.statLifeMax2 + " (of " + (player.statLifeMax2 + maxHpLost) + ")"
				+ "\n" + "Health regeneration reduced to 0%"
				+ "\n" + "Movement speed reduced to " + movePercOf100 + "%"
			);
		}
	}
}
