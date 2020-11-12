using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		private void UpdateNecrotisForCurrentContext() {
			var config = NecrotisConfig.Instance;
			Player plr = this.player;
			int tileX = (int)plr.position.X / 16;
			int tileY = (int)plr.position.Y / 16;
			bool isTown = plr.townNPCs > 1f && !Main.bloodMoon && !Main.eclipse;

			//

			this.CurrentAnimaPercentChangeRate = 0f;

			//

			void reduceAnima( float amt, string ctx ) {
				if( amt > 0f && isTown ) {
					return;
				}
				this.AfflictAnimaPercentLoss( amt );

				if( config.DebugModeInfo ) {
					DebugHelpers.Print( ctx, amt.ToString("F6") );
				}
			}

			//

			// Underworld
			if( tileY > WorldHelpers.UnderworldLayerTopTileY ) {
				float hellAfflict = config.Get<float>( nameof(NecrotisConfig.HellAnimaPercentLossPerTick) );
				reduceAnima( hellAfflict, "NecrotisCtx_Hell" );
			}

			// Dirt layer
			else if( tileY > WorldHelpers.DirtLayerTopTileY ) {
				bool isOther = false;
				// Dungeon
				if( plr.ZoneDungeon ) {
					float dungAfflict = config.Get<float>( nameof(NecrotisConfig.DungeonAnimaPercentLossPerTick) );
					reduceAnima( dungAfflict, "NecrotisCtx_Dungeon" );
					isOther = true;
				}
				// Corruption/crimson
				if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
					float corrAfflict = config.Get<float>( nameof(NecrotisConfig.CorruptionAnimaPercentLossPerTick) );
					reduceAnima( corrAfflict, "NecrotisCtx_UndCorr" );
					isOther = true;
				}
				// Hallow
				if( plr.ZoneHoly ) {
					float holyAfflict = config.Get<float>( nameof(NecrotisConfig.HallowAnimaPercentLossPerTick) );
					reduceAnima( holyAfflict, "NecrotisCtx_UndHallow" );
					isOther = true;
				}
				// Desert
				if( plr.ZoneUndergroundDesert ) {	//plr.ZoneDesert ) {
					float desAfflict = config.Get<float>( nameof(NecrotisConfig.DesertAnimaPercentLossPerTick) );
					reduceAnima( desAfflict, "NecrotisCtx_UndDesert" );
					isOther = true;
				}
				// Ice
				if( plr.ZoneSnow ) {
					float snowAfflict = config.Get<float>( nameof(NecrotisConfig.SnowAnimaPercentLossPerTick) );
					reduceAnima( snowAfflict, "NecrotisCtx_UndIce" );
					isOther = true;
				}
				// Jungle
				if( plr.ZoneJungle ) {
					float jungAfflict = config.Get<float>( nameof(NecrotisConfig.JungleAnimaPercentLossPerTick) );
					reduceAnima( jungAfflict, "NecrotisCtx_UndJung" );
					isOther = true;
				}
				// Empty
				if( !isOther ) {
					float undAfflict = config.Get<float>( nameof(NecrotisConfig.UndergroundAnimaPercentLossPerTick) );
					reduceAnima( undAfflict, "NecrotisCtx_Und" );
				}
			}
			//else if( yPos > (WorldHelpers.DirtLayerTopTileY << 4) ) {

			// Surface
			else if( tileY > WorldHelpers.SurfaceLayerTopTileY ) {
				bool isBeach = tileX <= WorldHelpers.BeachWestTileX || tileX >= WorldHelpers.BeachEastTileX;

				// Beach
				if( isBeach ) {
					float beaAfflict = config.Get<float>( nameof(NecrotisConfig.BeachAnimaPercentLossPerTick) );
					reduceAnima( beaAfflict, "NecrotisCtx_Beach" );
				}
				// Corruption/crimson
				if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
					float corrAfflict = config.Get<float>( nameof(NecrotisConfig.CorruptionAnimaPercentLossPerTick) );
					reduceAnima( corrAfflict, "NecrotisCtx_Corr" );
				}
				// Hallow
				if( plr.ZoneHoly ) {
					float hallAfflict = config.Get<float>( nameof(NecrotisConfig.HallowAnimaPercentLossPerTick) );
					reduceAnima( hallAfflict, "NecrotisCtx_Hallow" );
				}
				// Desert
				if( !isBeach && plr.ZoneDesert ) {
					float desAfflict = config.Get<float>( nameof(NecrotisConfig.DesertAnimaPercentLossPerTick) );
					reduceAnima( desAfflict, "NecrotisCtx_Desert" );
				}
				// Ice
				if( plr.ZoneSnow ) {
					float snowAfflict = config.Get<float>( nameof(NecrotisConfig.SnowAnimaPercentLossPerTick) );
					reduceAnima( snowAfflict, "NecrotisCtx_Snow" );
				}
				// Jungle
				if( plr.ZoneJungle ) {
					float jungAfflict = config.Get<float>( nameof(NecrotisConfig.JungleAnimaPercentLossPerTick) );
					reduceAnima( jungAfflict, "NecrotisCtx_Jungle" );
				}

				// Night or Eclipse
				if( !isTown && (!Main.dayTime || Main.eclipse) ) {
					float nightAfflict = config.Get<float>( nameof(NecrotisConfig.NightOrEclipseAnimaPercentLossPerTick) );
					reduceAnima( nightAfflict, "NecrotisCtx_Night" );
				}
			}

			// Sky
			else if( tileY > WorldHelpers.SkyLayerTopTileY ) {
				float skyAfflict = config.Get<float>( nameof(NecrotisConfig.SkyAnimaPercentLossPerTick) );
				reduceAnima( skyAfflict, "NecrotisCtx_Sky" );
			}
		}
	}
}
