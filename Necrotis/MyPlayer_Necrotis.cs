using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		private void UpdateNecrotisForCurrentContext() {
			void addNecrotis( float amt, string ctx ) {
				this.AddNecrotis( amt );

				if( NecrotisConfig.Instance.DebugModeInfo ) {
					DebugHelpers.Print( ctx, amt.ToString("F6") );
				}
			}

			//

			int xPos = (int)this.player.position.X;
			int yPos = (int)this.player.position.Y;
			
			// Underworld
			if( yPos > (WorldHelpers.UnderworldLayerTopTileY << 4) ) {
				addNecrotis( NecrotisConfig.Instance.UnderworldAfflicationIncreasePerTick, "NecrotisCtx_Hell" );
			}

			// Rock layer
			else if( yPos > (WorldHelpers.RockLayerTopTileY << 4) ) {
				if( !this.player.ZoneDungeon ) {
					// Corruption/crimson
					if( this.player.ZoneCorrupt || this.player.ZoneCrimson ) {
						addNecrotis( NecrotisConfig.Instance.CorruptionAfflicationIncreasePerTick, "NecrotisCtx_UndCorr" );
					}
					// Hallow
					else if( this.player.ZoneHoly ) {
						addNecrotis( NecrotisConfig.Instance.HallowAfflicationIncreasePerTick, "NecrotisCtx_UndHallow" );
					}
					// Desert
					else if( this.player.ZoneDesert ) {
						addNecrotis( NecrotisConfig.Instance.DesertAfflicationIncreasePerTick, "NecrotisCtx_UndDesert" );
					}
					// Ice
					else if( this.player.ZoneSnow ) {
						addNecrotis( NecrotisConfig.Instance.SnowAfflicationIncreasePerTick, "NecrotisCtx_UndIce" );
					}
					// Jungle
					else if( this.player.ZoneJungle ) {
						addNecrotis( NecrotisConfig.Instance.JungleAfflicationIncreasePerTick, "NecrotisCtx_UndJung" );
					}
				}
			}

			// Dirt layer
			if( yPos > (WorldHelpers.DirtLayerTopTileY << 4) ) {
				// Dungeon
				if( this.player.ZoneDungeon ) {
					addNecrotis( NecrotisConfig.Instance.DungeonAfflicationIncreasePerTick, "NecrotisCtx_Dungeon" );
				}
			}
			//else if( yPos > (WorldHelpers.DirtLayerTopTileY << 4) ) {

			// Surface
			else if( yPos > (WorldHelpers.SurfaceLayerTopTileY << 4) ) {
				// Beach
				if( xPos >= WorldHelpers.BeachEastTileX || xPos <= WorldHelpers.BeachEastTileX ) {
					addNecrotis( NecrotisConfig.Instance.BeachAfflicationIncreasePerTick, "NecrotisCtx_Beach" );
				}
				// Corruption/crimson
				else if( this.player.ZoneCorrupt || this.player.ZoneCrimson ) {
					addNecrotis( NecrotisConfig.Instance.CorruptionAfflicationIncreasePerTick, "NecrotisCtx_Corr" );
				}
				// Hallow
				else if( this.player.ZoneHoly ) {
					addNecrotis( NecrotisConfig.Instance.HallowAfflicationIncreasePerTick, "NecrotisCtx_Hallow" );
				}
				// Desert
				else if( this.player.ZoneDesert ) {
					addNecrotis( NecrotisConfig.Instance.DesertAfflicationIncreasePerTick, "NecrotisCtx_Desert" );
				}
				// Ice
				else if( this.player.ZoneSnow ) {
					addNecrotis( NecrotisConfig.Instance.SnowAfflicationIncreasePerTick, "NecrotisCtx_Snow" );
				}
				// Jungle
				else if( this.player.ZoneJungle ) {
					addNecrotis( NecrotisConfig.Instance.JungleAfflicationIncreasePerTick, "NecrotisCtx_Jungle" );
				}
			}

			// Sky
			else if( yPos > (WorldHelpers.SkyLayerTopTileY << 4) ) {
				addNecrotis( NecrotisConfig.Instance.SkyAfflicationIncreasePerTick, "NecrotisCtx_Sky" );
			}
		}


		////

		public void AddNecrotis( float amt, bool quiet=false ) {
			float old = this.NecrotisResistPercent;

			if( this.NecrotisResistPercent < 0f ) {	// If afflicted
				if( amt > 0f ) {
					amt *= 0.5f;	// Slower decrease
				} else {
					amt *= 2f;	// Faster recovery
				}
			}

			this.NecrotisResistPercent -= amt;

			if( this.NecrotisResistPercent < -1f ) {
				this.NecrotisResistPercent = -1;
			} else if( this.NecrotisResistPercent > 1f ) {
				this.NecrotisResistPercent = 1f;
			}

			float realAmt = this.NecrotisResistPercent - old;

			if( !quiet && Math.Abs(amt) >= 0.1f ) {
				string fmtAmt = (realAmt * 100f).ToString("N0") + "%";
				if( realAmt > 0f ) {
					fmtAmt = "+" + fmtAmt;
				}

				CombatText.NewText( this.player.getRect(), Color.Gold, fmtAmt );
			}
		}
	}
}
