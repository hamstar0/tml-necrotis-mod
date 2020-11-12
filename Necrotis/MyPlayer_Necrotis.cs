using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Dusts;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		public static float CalculateHealAmountFromWitchDoctor( Player player ) {
			var config = NecrotisConfig.Instance;
			var myplayer = player.GetModPlayer<NecrotisPlayer>();

			float healPercMax = config.Get<float>( nameof(config.WitchDoctorHealPercentMax) );

			if( myplayer.AnimaPercent >= healPercMax ) {
				return 0f;
			}

			return healPercMax - myplayer.AnimaPercent;
		}
		
		public static int CalculateHealCostFromWitchDoctor( Player player, float healAmount ) {
			var config = NecrotisConfig.Instance;
			float costPerPercent = config.Get<float>( nameof(config.WitchDoctorHealCostPerPercent) );

			return (int)(healAmount * costPerPercent);
		}

		////

		public static bool HealAtCost( Player player, float healAmount ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			int cost = NecrotisPlayer.CalculateHealCostFromWitchDoctor( player, healAmount );
			if( cost == 0 ) {
				return false;
			}

			if( !player.BuyItem(cost) ) {
				return false;
			}
			
			myplayer.AnimaPercent += healAmount;

			DustHelpers.CreateMany( dustType: DustHelpers.GoldGlitterTypeID, position: player.Center, quantity: 8 );
			Main.PlaySound( SoundID.Item4 );

			return true;
		}



		////////////////

		public void AfflictAnimaPercentLoss( float percentAmt, bool quiet=false ) {
			float old = this.AnimaPercent;

			/*// If afflicted
			if( this.AnimaPercent < 0.5f ) {
				// Reduce amount of added affliction
				if( percentAmt > 0f ) {
					percentAmt *= 0.25f;
				}
			}*/

			this.AnimaPercent -= percentAmt;

			if( this.AnimaPercent < 0f ) {
				this.AnimaPercent = 0f;
			} else if( this.AnimaPercent > 1f ) {
				this.AnimaPercent = 1f;
			}

			// Amount of change
			float percChangeAmt = this.AnimaPercent - old;

			this.CurrentAnimaPercentChangeRate += percChangeAmt;

			// Display afflict amount
			if( !quiet && Math.Abs(percentAmt) >= 0.1f ) {
				string fmtAmt = (percChangeAmt * 100f).ToString("N0") + "%";
				if( percChangeAmt > 0f ) {
					fmtAmt = "+" + fmtAmt;
				}

				CombatText.NewText( this.player.getRect(), Color.Gold, fmtAmt );
			}
		}
	}
}
