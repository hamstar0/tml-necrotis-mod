using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Necrotis.Buffs;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		private void UpdateAnimaState() {
			Player plr = this.player;
			int tileY = (int)plr.position.Y / 16;

			//

			this.CurrentAnimaPercentChangeRate = 0f;

			//

			// Underworld
			if( tileY > WorldHelpers.UnderworldLayerTopTileY ) {
				this.UpdateAnimaStateForUnderworld();
			}
			// Dirt layer
			else if( tileY > WorldHelpers.DirtLayerTopTileY ) {
				this.UpdateAnimaStateForDirtLayer();
			}
			// Surface
			else if( tileY > WorldHelpers.SurfaceLayerTopTileY ) {
				this.UpdateAnimaStateForSurface();
			}
			// Sky
			else if( tileY <= WorldHelpers.SkyLayerBottomTileY ) {
				this.UpdateAnimaStateForSky();
			}
			this.UpdateAnimaStateForAll();
		}


		////

		private void UpdateAnimaStateForAll() {
			var config = NecrotisConfig.Instance;
			Player plr = this.player;

			bool isTown = plr.townNPCs > 1f;
			bool isUnsafe = Main.bloodMoon || Main.eclipse;

			// Town (safe)
			if( isTown && !isUnsafe ) {
				float townAfflict = config.Get<float>( nameof( NecrotisConfig.TownAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( townAfflict, "NecrotisCtx_Town" );
			}
		}


		private void UpdateAnimaStateForUnderworld() {
			var config = NecrotisConfig.Instance;

			float hellAfflict = config.Get<float>( nameof( NecrotisConfig.HellAnimaPercentLossPerTick ) );
			this.ReduceAnimaPerContext( hellAfflict, "NecrotisCtx_Hell" );
		}


		private void UpdateAnimaStateForDirtLayer() {
			var config = NecrotisConfig.Instance;
			bool isOther = false;
			Player plr = this.player;

			// Dungeon
			if( plr.ZoneDungeon ) {
				float dungAfflict = config.Get<float>( nameof( NecrotisConfig.DungeonAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( dungAfflict, "NecrotisCtx_Dungeon" );
				isOther = true;
			}
			// Corruption/crimson
			if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
				float corrAfflict = config.Get<float>( nameof( NecrotisConfig.CorruptionAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( corrAfflict, "NecrotisCtx_UndCorr" );
				isOther = true;
			}
			// Hallow
			if( plr.ZoneHoly ) {
				float holyAfflict = config.Get<float>( nameof( NecrotisConfig.HallowAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( holyAfflict, "NecrotisCtx_UndHallow" );
				isOther = true;
			}
			// Desert
			if( plr.ZoneUndergroundDesert ) {   //plr.ZoneDesert ) {
				float desAfflict = config.Get<float>( nameof( NecrotisConfig.DesertAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( desAfflict, "NecrotisCtx_UndDesert" );
				isOther = true;
			}
			// Ice
			if( plr.ZoneSnow ) {
				float snowAfflict = config.Get<float>( nameof( NecrotisConfig.SnowAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( snowAfflict, "NecrotisCtx_UndIce" );
				isOther = true;
			}
			// Jungle
			if( plr.ZoneJungle ) {
				float jungAfflict = config.Get<float>( nameof( NecrotisConfig.JungleAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( jungAfflict, "NecrotisCtx_UndJung" );
				isOther = true;
			}

			// Caves (default)
			if( !isOther ) {
				float caveAfflict = config.Get<float>( nameof( NecrotisConfig.PlainCavesAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( caveAfflict, "NecrotisCtx_Und" );
			}
		}


		private void UpdateAnimaStateForSurface() {
			var config = NecrotisConfig.Instance;
			Player plr = this.player;
			int tileX = (int)plr.position.X / 16;
			int tileY = (int)plr.position.Y / 16;

			bool isTown = plr.townNPCs > 1f;
			bool isUnsafe = Main.bloodMoon || Main.eclipse;
			bool isBeach = tileX <= WorldHelpers.BeachWestTileX || tileX >= WorldHelpers.BeachEastTileX;
			bool isOther = false;

			// Beach
			if( isBeach && tileY > WorldHelpers.SkyLayerBottomTileY ) {
				float beaAfflict = config.Get<float>( nameof( NecrotisConfig.BeachAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( beaAfflict, "NecrotisCtx_Beach" );
				isOther = true;
			}
			// Desert
			if( !isBeach && plr.ZoneDesert ) {
				float desAfflict = config.Get<float>( nameof( NecrotisConfig.DesertAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( desAfflict, "NecrotisCtx_Desert" );
				isOther = true;
			}
			// Ice
			if( plr.ZoneSnow ) {
				float snowAfflict = config.Get<float>( nameof( NecrotisConfig.SnowAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( snowAfflict, "NecrotisCtx_Snow" );
				isOther = true;
			}
			// Jungle
			if( plr.ZoneJungle ) {
				float jungAfflict = config.Get<float>( nameof( NecrotisConfig.JungleAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( jungAfflict, "NecrotisCtx_Jungle" );
				isOther = true;
			}

			// Hallow
			if( plr.ZoneHoly ) {
				float hallAfflict = config.Get<float>( nameof( NecrotisConfig.HallowAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( hallAfflict, "NecrotisCtx_Hallow" );
				//isOther = true;
			}
			// Corruption/crimson
			else if( plr.ZoneCorrupt || plr.ZoneCrimson ) {
				float corrAfflict = config.Get<float>( nameof( NecrotisConfig.CorruptionAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( corrAfflict, "NecrotisCtx_Corr" );
				isOther = true;
			}

			// Forest (default)
			if( !isOther ) {
				float forAfflict = config.Get<float>( nameof( NecrotisConfig.ForestAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( forAfflict, "NecrotisCtx_Forest" );
			}

			// Night or Eclipse
			if( (!isTown || isUnsafe) && ( !Main.dayTime || Main.eclipse ) ) {
				float nightAfflict = config.Get<float>( nameof( NecrotisConfig.NightOrEclipseAnimaPercentLossPerTick ) );
				this.ReduceAnimaPerContext( nightAfflict, "NecrotisCtx_NightOrEclipse" );
			}
		}

		private void UpdateAnimaStateForSky() {
			var config = NecrotisConfig.Instance;

			float skyAfflict = config.Get<float>( nameof( NecrotisConfig.SkyAnimaPercentLossPerTick ) );
			this.ReduceAnimaPerContext( skyAfflict, "NecrotisCtx_Sky" );
		}


		////////////////

		private void ReduceAnimaPerContext( float percent, string context ) {
			var config = NecrotisConfig.Instance;
			Player plr = this.player;

			bool isTown = plr.townNPCs > 1f;
			bool isUnsafe = Main.bloodMoon || Main.eclipse;
			bool isElixired = player.HasBuff( ModContent.BuffType<ElixirBuff>() );

			// Town
			if( percent > 0f && isTown && !isUnsafe ) {
				percent = 0f;
			}

			// Elixer
			if( percent > 0f && isElixired ) {
				percent *= config.Get<float>( nameof(config.ElixirAnimaDrainMultiplier) );
			}

			this.SubtractAnimaPercent( percent, false, false );

			if( config.DebugModeInfo ) {
				if( isTown ) {
					context += "_Town";
					if( isUnsafe ) {
						context += "Unsafe";
					}
				}
				string amtStr = percent.ToString( "F6" );
				if( isElixired ) {
					amtStr += "e";
				}

				DebugHelpers.Print( context, amtStr );
			}
		}
	}
}
