using System;
using Terraria;
using Terraria.ModLoader;
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

			this.CurrentNecrotisResistPercentChangeRate = 0f;

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

			// Underworld
			if( tileY > WorldHelpers.UnderworldLayerTopTileY ) {
				addNecrotis( NecrotisConfig.Instance.HellAfflicationIncreasePerTick, "NecrotisCtx_Hell" );
			}

			// Dirt layer
			else if( tileY > WorldHelpers.DirtLayerTopTileY ) {
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
			else if( tileY > WorldHelpers.SurfaceLayerTopTileY ) {
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
			else if( tileY > WorldHelpers.SkyLayerTopTileY ) {
				addNecrotis( NecrotisConfig.Instance.SkyAfflicationIncreasePerTick, "NecrotisCtx_Sky" );
			}
		}
	}
}
