﻿using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		private void UpdateNecrotisForCurrentContext() {
			Player plr = this.player;
			int tileX = (int)plr.position.X / 16;
			int tileY = (int)plr.position.Y / 16;
			bool isTown = plr.townNPCs > 1f && !Main.bloodMoon && !Main.eclipse;

			//

			void addNecrotis( float amt, string ctx ) {
				if( amt > 0f && isTown ) {
					return;
				}
				this.AfflictNecrotis( amt );

				if( NecrotisConfig.Instance.DebugModeInfo ) {
					DebugHelpers.Print( ctx, amt.ToString("F6") );
				}
			}

			//

			this.CurrentNecrotisAfflictPercentRate = 0f;

			//

			// Underworld
			if( tileY > (WorldHelpers.UnderworldLayerTopTileY << 4) ) {
				addNecrotis( NecrotisConfig.Instance.HellAfflicationIncreasePerTick, "NecrotisCtx_Hell" );
			}

			// Rock layer
			//else if( yPos > (WorldHelpers.RockLayerTopTileY << 4) ) {

			// Dirt layer
			else if( tileY > (WorldHelpers.DirtLayerTopTileY << 4) ) {
				bool isOther = false;
				// Dungeon
				if( plr.ZoneDungeon ) {
					addNecrotis( NecrotisConfig.Instance.DungeonAfflicationIncreasePerTick, "NecrotisCtx_Dungeon" );
					isOther = true;
				}
				// Corruption/crimson
				if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
					addNecrotis( NecrotisConfig.Instance.CorruptionAfflicationIncreasePerTick, "NecrotisCtx_UndCorr" );
					isOther = true;
				}
				// Hallow
				if( plr.ZoneHoly ) {
					addNecrotis( NecrotisConfig.Instance.HallowAfflicationIncreasePerTick, "NecrotisCtx_UndHallow" );
					isOther = true;
				}
				// Desert
				if( plr.ZoneUndergroundDesert ) {	//plr.ZoneDesert ) {
					addNecrotis( NecrotisConfig.Instance.DesertAfflicationIncreasePerTick, "NecrotisCtx_UndDesert" );
					isOther = true;
				}
				// Ice
				if( plr.ZoneSnow ) {
					addNecrotis( NecrotisConfig.Instance.SnowAfflicationIncreasePerTick, "NecrotisCtx_UndIce" );
					isOther = true;
				}
				// Jungle
				if( plr.ZoneJungle ) {
					addNecrotis( NecrotisConfig.Instance.JungleAfflicationIncreasePerTick, "NecrotisCtx_UndJung" );
					isOther = true;
				}
				// Empty
				if( !isOther ) {
					addNecrotis( NecrotisConfig.Instance.UndergroundAfflicationIncreasePerTick, "NecrotisCtx_Und" );
				}
			}
			//else if( yPos > (WorldHelpers.DirtLayerTopTileY << 4) ) {

			// Surface
			else if( tileY > (WorldHelpers.SurfaceLayerTopTileY << 4) ) {
				bool isBeach = tileX <= WorldHelpers.BeachWestTileX || tileX >= WorldHelpers.BeachEastTileX;
				// Beach
				if( isBeach ) {
					addNecrotis( NecrotisConfig.Instance.BeachAfflicationIncreasePerTick, "NecrotisCtx_Beach" );
				}
				// Corruption/crimson
				if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
					addNecrotis( NecrotisConfig.Instance.CorruptionAfflicationIncreasePerTick, "NecrotisCtx_Corr" );
				}
				// Hallow
				if( plr.ZoneHoly ) {
					addNecrotis( NecrotisConfig.Instance.HallowAfflicationIncreasePerTick, "NecrotisCtx_Hallow" );
				}
				// Desert
				if( !isBeach && plr.ZoneDesert ) {
					addNecrotis( NecrotisConfig.Instance.DesertAfflicationIncreasePerTick, "NecrotisCtx_Desert" );
				}
				// Ice
				if( plr.ZoneSnow ) {
					addNecrotis( NecrotisConfig.Instance.SnowAfflicationIncreasePerTick, "NecrotisCtx_Snow" );
				}
				// Jungle
				if( plr.ZoneJungle ) {
					addNecrotis( NecrotisConfig.Instance.JungleAfflicationIncreasePerTick, "NecrotisCtx_Jungle" );
				}

				// Night or Eclipse
				if( !isTown && (!Main.dayTime || Main.eclipse) ) {
					addNecrotis( NecrotisConfig.Instance.NightOrEclipseAfflicationIncreasePerTick, "NecrotisCtx_Night" );
				}
			}

			// Sky
			else if( tileY > (WorldHelpers.SkyLayerTopTileY << 4) ) {
				addNecrotis( NecrotisConfig.Instance.SkyAfflicationIncreasePerTick, "NecrotisCtx_Sky" );
			}
		}


		////

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

			this.CurrentNecrotisAfflictPercentRate += percChangeAmt;

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
