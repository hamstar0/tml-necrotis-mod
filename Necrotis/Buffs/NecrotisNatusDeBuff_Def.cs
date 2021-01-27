using System;
using Terraria;
using Terraria.ModLoader;
using Necrotis.NecrotisBehaviors;


namespace Necrotis.Buffs {
	public partial class NecrotisNatusDeBuff : ModBuff {
		private static string BaseDescription = "You begin feeling horriby drained"
				+ "\n" + "Your anima is getting low";
				//+ "\n" + "Reduces max life, speed, life regen";



		////////////////

		public static bool CanBuff( Player player, float animaPercent ) {
			return animaPercent > 0f && animaPercent <= 0.5f;
		}



		////////////////

		private float LastMovePercent;
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
			var myplayer = plr.GetModPlayer<NecrotisPlayer>();

			float necrotisPercent = myplayer.NecrotisPercent;
			int lastRegenEffectPercOf100 = (int)( (1f - necrotisPercent) * 100f );
			int lastMovePercentOf100 = (int)( this.LastMovePercent * 100f );
			float visibility = NecrotisBehavior.CalculateViewVisibilityScale( necrotisPercent );

			tip = NecrotisNatusDeBuff.BaseDescription;
			if( plr.statLifeMax > 100 ) {
				int realMaxHp = plr.statLifeMax2 + this.LastMaxHpLost;
				tip += "\n"+"Max health reduced to "+plr.statLifeMax2+" (of "+realMaxHp+")";
			}
			tip += "\n"+"Health regeneration reduced to "+lastRegenEffectPercOf100+"%";
			tip += "\n"+"Movement speed reduced to "+lastMovePercentOf100+"%";
			tip += "\n"+"Visibility reduced to "+(int)(visibility * 100f)+"%";
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			if( player.dead ) { return; }

			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			float necrotisPercent = myplayer.NecrotisPercent;

			NecrotisBehavior.ApplyBehaviors( player, necrotisPercent, out this.LastMaxHpLost, out this.LastMovePercent );
		}
	}
}
