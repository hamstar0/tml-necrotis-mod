using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Items;
using ModLibsGeneral.Libraries.World;
using PotLuck;
using CursedBrambles;
using Necrotis.Items;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		private void LoadPotLuckMod() {
			/*potluckConfig.SetOverride(
				nameof(PotLuckConfig.PotEntries),
				new List<PotEntry> {
					new PotEntry {
						PercentChance = ectoDropPerc,
						HardModeOnly = false,
						IsSurface = true,
						IsCaves = true,
						IsUnderworld = false,
						ItemDefs = new List<PotItemEntry> {
							new PotItemEntry {
								MinStack = 1,
								MaxStack = 1,
								ItemDef = new ItemDefinition( ModContent.ItemType<DillutedEctoplasmItem>() )
							}
						}
					}
				}
			);*/

			//

			bool OnPotBreak( int tileX, int tileY, out IList<int> droppedItemIndexes ) {
				droppedItemIndexes = new List<int>();

				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					return false;
				}

				//

				var myConfig = NecrotisConfig.Instance;
				var potluckConfig = PotLuckConfig.Instance;

				float ectoDropPerc = myConfig.Get<float>( nameof( myConfig.DillutedEctoplasmPotDropChance ) );

				if( Main.rand.NextFloat() > ectoDropPerc ) {
					return false;
				}

				if( tileY > WorldLocationLibraries.UnderworldLayerTopTileY ) {
					return false;
				}

				//

				int itemIdx = ItemLibraries.CreateItem(
					pos: new Vector2( tileX*16, tileY*16 ),
					type: ModContent.ItemType<DillutedEctoplasmItem>(),
					stack: 1,
					width: 12,
					height: 12
				);

				droppedItemIndexes.Add( itemIdx );

				return true;
			}

			//

			PotLuckAPI.AddPotBreakAction( OnPotBreak );
		}


		private void LoadCursedBramblesMod() {
			var cbConfig = CursedBramblesConfig.Instance;

			cbConfig.SetOverride( nameof(cbConfig.BossesCreateBrambleTrail), false );
			cbConfig.SetOverride( nameof(cbConfig.PlayersCreateDefaultBrambleTrail), false );
		}
	}
}