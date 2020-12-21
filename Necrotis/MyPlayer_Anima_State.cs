using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Necrotis.Buffs;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		private void UpdateAnimaStateForCurrentContext() {
			var config = NecrotisConfig.Instance;
			Player plr = this.player;
			int tileX = (int)plr.position.X / 16;
			int tileY = (int)plr.position.Y / 16;

			bool isTown = plr.townNPCs > 1f;
			bool isUnsafe = !Main.bloodMoon && !Main.eclipse;
			bool isBeach = tileX <= WorldHelpers.BeachWestTileX || tileX >= WorldHelpers.BeachEastTileX;
			bool isElixired = player.HasBuff( ModContent.BuffType<ElixirBuff>() );

			//

			this.CurrentAnimaPercentChangeRate = 0f;

			//

			void reduceAnima( float amt, string ctx ) {
				// Town
				if( amt > 0f && isTown && !isUnsafe ) {
					amt = 0f;
				}

				// Elixer
				if( amt > 0f && isElixired ) {
					amt *= config.Get<float>( nameof(config.ElixirAnimaDrainMultiplier) );
				}

				this.SubtractAnimaPercent( amt, false, false );

				if( amt != 0 && config.DebugModeInfo ) {
					if( isTown ) {
						ctx += "_Town";
						if( isUnsafe ) {
							ctx += "Unsafe";
						}
					}
					string amtStr = amt.ToString( "F6" );
					if( isElixired ) {
						amtStr += "e";
					}

					DebugHelpers.Print( ctx, amtStr );
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
				// Caves (default)
				if( !isOther ) {
					float caveAfflict = config.Get<float>( nameof(NecrotisConfig.PlainCavesAnimaPercentLossPerTick) );
					reduceAnima( caveAfflict, "NecrotisCtx_Und" );
				}
			}
			//else if( yPos > (WorldHelpers.DirtLayerTopTileY << 4) ) {

			// Surface
			else if( tileY > WorldHelpers.SurfaceLayerTopTileY ) {
				bool isOther = false;
				// Beach
				if( isBeach && tileY > WorldHelpers.SkyLayerBottomTileY ) {
					float beaAfflict = config.Get<float>( nameof(NecrotisConfig.BeachAnimaPercentLossPerTick) );
					reduceAnima( beaAfflict, "NecrotisCtx_Beach" );
					isOther = true;
				}
				// Corruption/crimson
				if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
					float corrAfflict = config.Get<float>( nameof(NecrotisConfig.CorruptionAnimaPercentLossPerTick) );
					reduceAnima( corrAfflict, "NecrotisCtx_Corr" );
					isOther = true;
				}
				// Hallow
				if( plr.ZoneHoly ) {
					float hallAfflict = config.Get<float>( nameof(NecrotisConfig.HallowAnimaPercentLossPerTick) );
					reduceAnima( hallAfflict, "NecrotisCtx_Hallow" );
					isOther = true;
				}
				// Desert
				if( !isBeach && plr.ZoneDesert ) {
					float desAfflict = config.Get<float>( nameof(NecrotisConfig.DesertAnimaPercentLossPerTick) );
					reduceAnima( desAfflict, "NecrotisCtx_Desert" );
					isOther = true;
				}
				// Ice
				if( plr.ZoneSnow ) {
					float snowAfflict = config.Get<float>( nameof(NecrotisConfig.SnowAnimaPercentLossPerTick) );
					reduceAnima( snowAfflict, "NecrotisCtx_Snow" );
					isOther = true;
				}
				// Jungle
				if( plr.ZoneJungle ) {
					float jungAfflict = config.Get<float>( nameof(NecrotisConfig.JungleAnimaPercentLossPerTick) );
					reduceAnima( jungAfflict, "NecrotisCtx_Jungle" );
					isOther = true;
				}
				// Forest (default)
				if( !isOther ) {
					float forAfflict = config.Get<float>( nameof( NecrotisConfig.ForestAnimaPercentLossPerTick ) );
					reduceAnima( forAfflict, "NecrotisCtx_Forest" );
				}

				// Night or Eclipse
				if( (!isTown || isUnsafe) && (!Main.dayTime || Main.eclipse) ) {
					float nightAfflict = config.Get<float>( nameof(NecrotisConfig.NightOrEclipseAnimaPercentLossPerTick) );
					reduceAnima( nightAfflict, "NecrotisCtx_Night" );
				}
			}

			// Town (safe)
			if( isTown && !isUnsafe ) {
				float townAfflict = config.Get<float>( nameof(NecrotisConfig.TownAnimaPercentLossPerTick) );
				reduceAnima( townAfflict, "NecrotisCtx_Town" );
			}

			// Sky
			else if( tileY <= WorldHelpers.SkyLayerBottomTileY ) {
				float skyAfflict = config.Get<float>( nameof(NecrotisConfig.SkyAnimaPercentLossPerTick) );
				reduceAnima( skyAfflict, "NecrotisCtx_Sky" );
			}
		}
	}
}
