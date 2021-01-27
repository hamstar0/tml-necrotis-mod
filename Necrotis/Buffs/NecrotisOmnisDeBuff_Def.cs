using System;
using Terraria;
using Terraria.ModLoader;
using Necrotis.NecrotisBehaviors;


namespace Necrotis.Buffs {
	public partial class NecrotisOmnisDeBuff : ModBuff {
		private static string BaseDescription = "You feel horriby drained"
				+ "\n" + "Your anima is very low"
				+ "\n" + "Beware the shadows of the depths";



		////////////////

		public static bool CanBuff( Player player, float animaPercent ) {
			return animaPercent <= 0f;
		}



		////////////////

		private float LastMovePercent;
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
			var myplayer = plr.GetModPlayer<NecrotisPlayer>();

			float necrotisPercent = myplayer.NecrotisPercent;
			int lastMovePercentOf100 = (int)( this.LastMovePercent * 100f );
			float visibility = NecrotisBehavior.CalculateViewVisibilityScale( 1f );

			tip = NecrotisOmnisDeBuff.BaseDescription;
			if( plr.statLifeMax > 100 ) {
				int realMaxHp = plr.statLifeMax2 + this.LastMaxHpLost;
				tip += "\n"+"Max health reduced to "+plr.statLifeMax2+" (of "+realMaxHp+")";
			}
			tip += "\n"+"Health regeneration reduced to 0%";
			tip += "\n"+"Movement speed reduced to "+lastMovePercentOf100+"%";
			tip += "\n"+"Visibility reduced to "+(int)(visibility * 100f)+"%";
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			if( !player.dead ) {
				NecrotisBehavior.ApplyBehaviors( player, 1f, out this.LastMaxHpLost, out this.LastMovePercent );
			}
		}
	}
}
