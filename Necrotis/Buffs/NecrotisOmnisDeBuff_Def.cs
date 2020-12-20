using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;
using Necrotis.NecrotisBehaviors;


namespace Necrotis.Buffs {
	public partial class NecrotisOmnisDeBuff : ModBuff {
		public static bool CanBuff( Player player, float animaPercent ) {
			return animaPercent <= 0f;
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis Omnis" );
			this.Description.SetDefault(
				"You feel horriby drained"
				+ "\n" + "Reduces max life, speed, life regen"
				+ "\n" + "You are now being stalked by a shadow when in deep places"
			);

			Main.debuff[ this.Type ] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			this.UpdateBehaviors( player );
		}

		private void UpdateBehaviors( Player player ) {
			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, 1f );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, 1f );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, 1f );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, 1f );
			NecrotisBehavior.ApplyWorldBehaviors( player );
		}
	}
}
