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

		private int LastMovePercOf100;
		private int LastMaxHpLost;



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis Omnis" );
			this.Description.SetDefault( NecrotisOmnisDeBuff.BaseDescription );

			Main.debuff[ this.Type ] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}

		public override void ModifyBuffTip( ref string tip, ref int rare ) {
			Player plr = Main.LocalPlayer;

			tip = NecrotisOmnisDeBuff.BaseDescription;
			if( plr.statLifeMax > 100 ) {
				int realMaxHp = plr.statLifeMax2 + this.LastMaxHpLost;
				tip += "\n"+"Max health reduced to "+plr.statLifeMax2+" (of "+realMaxHp+")";
			}
			tip += "\n"+"Health regeneration reduced to 0%";
			tip += "\n"+"Movement speed reduced to "+this.LastMovePercOf100+"%";
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			this.UpdateBehaviors( player );
		}

		private void UpdateBehaviors( Player player ) {
			if( player.dead ) { return; }

			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, 1f, out float movePercent );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, 1f );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, 1f, out this.LastMaxHpLost );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, 1f );
			NecrotisBehavior.ApplyWorldBehaviors( player );

			this.LastMovePercOf100 = (int)( movePercent * 100f );
		}
	}
}
