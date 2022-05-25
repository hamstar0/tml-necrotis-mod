using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using CursedBrambles;
using CursedBrambles.Tiles;
using Necrotis.Buffs;


namespace Necrotis.NecrotisBehaviors {
	partial class NecrotisBehavior {
		internal static void ApplyWorldBehaviors( Player player, float necrotisPercent ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			if( necrotisPercent >= 1f && !player.dead && NecrotisOmnisDeBuff.CanBuff(player, 1f - necrotisPercent) ) {
				int radius = 64;
				int tickRate = 15;

				bool hasBrambleCurse = CursedBramblesAPI.GetPlayerBrambleWakeStatus(
					player: player,
					manuallyActivated: out _,
					isElevationConsidered: out _,
					radius: out int currRadius,
					tickRate: out int currTickRate,
					validateAt: out _
				);

				if( hasBrambleCurse ) {
					if( currTickRate > tickRate || currRadius > radius ) {
						hasBrambleCurse = false;

						CursedBramblesAPI.UnsetPlayerBrambleWakeCreating( player );
					}
				}

				if( !hasBrambleCurse ) {
					NecrotisBehavior.ApplyWorldBehaviors_BeginWake( player, radius, tickRate );
				}
			} else {
				CursedBramblesAPI.UnsetPlayerBrambleWakeCreating( player );
			}
		}


		private static void ApplyWorldBehaviors_BeginWake( Player player, int radius, int tickRate ) {
			CursedBramblesAPI.ValidateBrambleCreateAt validateAt = CursedBramblesAPI.CreatePlayerAvoidingBrambleValidator( 4 );
			int brambTile = ModContent.TileType<CursedBrambleTile>();

			bool ValidateAt( int tileX, int tileY ) {
				int minX = Math.Max( tileX - 8, 1 );
				int minY = Math.Max( tileY - 8, 1 );
				int maxX = Math.Max( tileX + 8, Main.maxTilesX - 1 );
				int maxY = Math.Max( tileY + 8, Main.maxTilesY - 1 );
				int adjBrambleCount = 0;

				for( int i=minX; i<maxX; i++ ) {
					for( int j=minY; j<maxY; j++ ) {
						Tile tile = Main.tile[i, j];
						if( tile?.active() == true && tile.type == brambTile ) {
							adjBrambleCount++;
						}
					}
				}

				return adjBrambleCount <= 12 
					? validateAt( tileX, tileY )
					: false;
			}

			//

			CursedBramblesAPI.SetPlayerToCreateBrambleWake(
				player: player,
				isElevationChecked: true,
				tileRadius: radius,
				tickRate: tickRate,
				validateAt: ValidateAt
			);
		}
	}
}
