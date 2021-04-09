using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Items;
using PotLuck;
using CursedBrambles;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		private void LoadPotLuckMod() {
			var myConfig = NecrotisConfig.Instance;
			var potluckConfig = PotLuckConfig.Instance;

			float ectoDropPerc = myConfig.Get<float>( nameof(myConfig.DillutedEctoplasmPotDropChance) );

			potluckConfig.SetOverride(
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
			);
		}


		private void LoadCursedBramblesMod() {
			var cbConfig = CursedBramblesConfig.Instance;

			cbConfig.SetOverride( nameof(cbConfig.BossesCreateBrambleTrail), false );
			cbConfig.SetOverride( nameof(cbConfig.PlayersCreateDefaultBrambleTrail), false );
		}
	}
}