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

			Player plr = this.player;
			int xPos = (int)plr.position.X;
			int yPos = (int)plr.position.Y;
			
			// Underworld
			if( yPos > (WorldHelpers.UnderworldLayerTopTileY << 4) ) {
				addNecrotis( NecrotisConfig.Instance.HellAfflicationIncreasePerTick, "NecrotisCtx_Hell" );
			}

			// Rock layer
			//else if( yPos > (WorldHelpers.RockLayerTopTileY << 4) ) {

			// Dirt layer
			else if( yPos > (WorldHelpers.DirtLayerTopTileY << 4) ) {
				// Dungeon
				if( plr.ZoneDungeon ) {
					addNecrotis( NecrotisConfig.Instance.DungeonAfflicationIncreasePerTick, "NecrotisCtx_Dungeon" );
				}
				// Corruption/crimson
				if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
					addNecrotis( NecrotisConfig.Instance.CorruptionAfflicationIncreasePerTick, "NecrotisCtx_UndCorr" );
				}
				// Hallow
				if( plr.ZoneHoly ) {
					addNecrotis( NecrotisConfig.Instance.HallowAfflicationIncreasePerTick, "NecrotisCtx_UndHallow" );
				}
				// Desert
				if( plr.ZoneUndergroundDesert ) {	//plr.ZoneDesert ) {
					addNecrotis( NecrotisConfig.Instance.DesertAfflicationIncreasePerTick, "NecrotisCtx_UndDesert" );
				}
				// Ice
				if( plr.ZoneSnow ) {
					addNecrotis( NecrotisConfig.Instance.SnowAfflicationIncreasePerTick, "NecrotisCtx_UndIce" );
				}
				// Jungle
				if( plr.ZoneJungle ) {
					addNecrotis( NecrotisConfig.Instance.JungleAfflicationIncreasePerTick, "NecrotisCtx_UndJung" );
				}
				// Empty
				else {
					addNecrotis( NecrotisConfig.Instance.UndergroundAfflicationIncreasePerTick, "NecrotisCtx_Und" );
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
				else if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
					addNecrotis( NecrotisConfig.Instance.CorruptionAfflicationIncreasePerTick, "NecrotisCtx_Corr" );
				}
				// Hallow
				else if( plr.ZoneHoly ) {
					addNecrotis( NecrotisConfig.Instance.HallowAfflicationIncreasePerTick, "NecrotisCtx_Hallow" );
				}
				// Desert
				else if( plr.ZoneDesert ) {
					addNecrotis( NecrotisConfig.Instance.DesertAfflicationIncreasePerTick, "NecrotisCtx_Desert" );
				}
				// Ice
				else if( plr.ZoneSnow ) {
					addNecrotis( NecrotisConfig.Instance.SnowAfflicationIncreasePerTick, "NecrotisCtx_Snow" );
				}
				// Jungle
				else if( plr.ZoneJungle ) {
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
