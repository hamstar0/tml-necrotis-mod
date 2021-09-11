using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Dusts;
using Necrotis.Net;


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

			DustLibraries.CreateMany( dustType: DustLibraries.GoldGlitterTypeID, position: player.Center, quantity: 8 );
			Main.PlaySound( SoundID.Item4 );

			return true;
		}



		////////////////
		
		public void SubtractAnimaPercent( float percentLost, bool quiet, bool sync ) {
			if( percentLost == 0f ) {
				return;
			}

			float old = this.AnimaPercent;

			//

			NecrotisAPI.RunAnimaChangeHooks( this.player, old, ref percentLost, ref quiet );

			//
			
			/*// If afflicted
			if( this.AnimaPercent < 0.5f ) {
				// Reduce amount of added affliction
				if( percentAmt > 0f ) {
					percentAmt *= 0.25f;
				}
			}*/

			this.AnimaPercent -= percentLost;
			if( this.AnimaPercent < 0f ) {
				this.AnimaPercent = 0f;
			} else if( this.AnimaPercent > 1f ) {
				this.AnimaPercent = 1f;
			}

			// Amount of change
			float percChangeAmt = this.AnimaPercent - old;

			this.CurrentAnimaPercentChangeRate += percChangeAmt;

			if( !quiet ) {
				// Display afflict amount
				if( Math.Abs(percentLost) >= 0.1f ) {
					string fmtAmt = (int)(percentLost * 100f)+"%";
					Color color;

					if( percentLost > 0f ) {
						fmtAmt = "+" + fmtAmt;
						color = Color.Lerp( Color.Gold, Color.White, 0.25f );
					} else {
						color = Color.Lerp( Color.Gold, Color.Black, 0.25f );
					}

					CombatText.NewText( this.player.getRect(), color, fmtAmt );
				}
			}

			if( sync ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					PlayerAnimaSyncProtocol.Broadcast( this );
				} else if( Main.netMode == NetmodeID.Server ) {
					PlayerAnimaSyncProtocol.SendToAllClients( this );
				}
			}
		}


		////////////////

		internal void SyncAnima( float animaPercent ) {
			this.AnimaPercent = animaPercent;
		}
	}
}
