using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using HamstarHelpers.Helpers.Debug;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
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
