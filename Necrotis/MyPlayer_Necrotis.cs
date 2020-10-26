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

			if( myplayer.NecrotisResistPercent >= healPercMax ) {
				return 0f;
			}

			return healPercMax - myplayer.NecrotisResistPercent;
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
			
			myplayer.NecrotisResistPercent += healAmount;

			DustHelpers.CreateMany( dustType: DustHelpers.GoldGlitterTypeID, position: player.Center, quantity: 8 );
			Main.PlaySound( SoundID.Item4 );

			return true;
		}



		////////////////

		public void AfflictNecrotis( float percentAmt, bool quiet=false ) {
			float old = this.NecrotisResistPercent;

			// If afflicted
			if( this.NecrotisResistPercent < 0f ) {
				// Reduce amount of added affliction
				if( percentAmt > 0f ) {
					percentAmt *= 0.25f;
				}
				// Otherwise increase amount of recovery
				else {
					//amt *= 4f;  // Faster recovery
					this.NecrotisResistPercent = 0;
				}
			}

			this.NecrotisResistPercent -= percentAmt;

			// Clamp (-1 to 0 = affliction buffer)
			if( this.NecrotisResistPercent < -1f ) {
				this.NecrotisResistPercent = -1;
			} else if( this.NecrotisResistPercent > 1f ) {
				this.NecrotisResistPercent = 1f;
			}

			// Amount of change
			float percChangeAmt = this.NecrotisResistPercent - old;

			this.CurrentNecrotisResistPercentChangeRate += percChangeAmt;

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
