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
			int xPos = (int)this.player.position.X;
			int yPos = (int)this.player.position.Y;

			// Underworld
			if( yPos > (WorldHelpers.UnderworldLayerTopTileY << 4) ) {
				this.AddNecrotis( NecrotisConfig.Instance.UnderworldAfflicationIncreasePerTick );
			}
			// Rock layer
			else if( yPos > (WorldHelpers.RockLayerTopTileY << 4) ) {
				// Dungeon
				if( this.player.ZoneDungeon ) {
					this.AddNecrotis( NecrotisConfig.Instance.DungeonAfflicationIncreasePerTick );
				}
				// Corruption/crimson
				if( this.player.ZoneCorrupt || this.player.ZoneCrimson ) {
					this.AddNecrotis( NecrotisConfig.Instance.CorruptionAfflicationIncreasePerTick );
				}
				// Hallow
				else if( this.player.ZoneHoly ) {
					this.AddNecrotis( NecrotisConfig.Instance.HallowAfflicationIncreasePerTick );
				}
				// Desert
				else if( this.player.ZoneDesert ) {
					this.AddNecrotis( NecrotisConfig.Instance.DesertAfflicationIncreasePerTick );
				}
				// Ice
				else if( this.player.ZoneSnow ) {
					this.AddNecrotis( NecrotisConfig.Instance.SnowAfflicationIncreasePerTick );
				}
				// Jungle
				else if( this.player.ZoneJungle ) {
					this.AddNecrotis( NecrotisConfig.Instance.JungleAfflicationIncreasePerTick );
				}
			}
			//else if( yPos > (WorldHelpers.DirtLayerTopTileY << 4) ) {
			// Surface
			else if( yPos > (WorldHelpers.SurfaceLayerTopTileY << 4) ) {
				// Beach
				if( xPos >= WorldHelpers.BeachEastTileX || xPos <= WorldHelpers.BeachEastTileX ) {
					this.AddNecrotis( NecrotisConfig.Instance.BeachAfflicationIncreasePerTick );
				}
				// Corruption/crimson
				else if( this.player.ZoneCorrupt || this.player.ZoneCrimson ) {
					this.AddNecrotis( NecrotisConfig.Instance.CorruptionAfflicationIncreasePerTick );
				}
				// Hallow
				else if( this.player.ZoneHoly ) {
					this.AddNecrotis( NecrotisConfig.Instance.HallowAfflicationIncreasePerTick );
				}
				// Desert
				else if( this.player.ZoneDesert ) {
					this.AddNecrotis( NecrotisConfig.Instance.DesertAfflicationIncreasePerTick );
				}
				// Ice
				else if( this.player.ZoneSnow ) {
					this.AddNecrotis( NecrotisConfig.Instance.SnowAfflicationIncreasePerTick );
				}
				// Jungle
				else if( this.player.ZoneJungle ) {
					this.AddNecrotis( NecrotisConfig.Instance.JungleAfflicationIncreasePerTick );
				}
			}
			// Sky
			else if( yPos > (WorldHelpers.SkyLayerTopTileY << 4) ) {
				this.AddNecrotis( NecrotisConfig.Instance.SkyAfflicationIncreasePerTick );
			}
		}


		////

		public void AddNecrotis( float amt, bool quiet=false ) {
			float old = this.NecrotisResistPercent;
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
