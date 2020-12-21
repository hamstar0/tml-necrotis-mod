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

		private int LastRegenEffectPercOf100;
		private int LastMovePercOf100;
		private int LastMaxHpLost;



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis Natus" );
			this.Description.SetDefault( NecrotisNatusDeBuff.BaseDescription );

			Main.debuff[this.Type] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}

		public override void ModifyBuffTip( ref string tip, ref int rare ) {
			Player plr = Main.LocalPlayer;
			tip =NecrotisNatusDeBuff.BaseDescription
				+ "\n" + "Max health reduced to "+plr.statLifeMax2+" (of "+(plr.statLifeMax2 + this.LastMaxHpLost)+")"
				+ "\n" + "Health regeneration reduced to "+this.LastRegenEffectPercOf100+"%"
				+ "\n" + "Movement speed reduced to "+this.LastMovePercOf100+"%";
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			if( player.dead ) { return; }

			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			float necrotisPercent = myplayer.NecrotisPercent;

			this.UpdateNecrotisBehaviors( player, necrotisPercent );
		}

		private void UpdateNecrotisBehaviors( Player player, float necrotisPercent ) {
			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, necrotisPercent, out float movePercent );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, necrotisPercent, out this.LastMaxHpLost );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, necrotisPercent );

			this.LastRegenEffectPercOf100 = (int)((1f - necrotisPercent) * 100f);
			this.LastMovePercOf100 = (int)(movePercent * 100f);
		}
	}
}
