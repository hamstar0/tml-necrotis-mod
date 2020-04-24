using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;
using Necrotis.Items;
using PotLuck;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-necrotis-mod";


		////////////////

		public static NecrotisMod Instance { get; private set; }



		////////////////

		public NecrotisMod() {
			NecrotisMod.Instance = this;
		}

		////////////////

		public override void Load() {
		}

		public override void Unload() {
			NecrotisMod.Instance = null;
		}


		public override void PostSetupContent() {
			var potluckConfig = ModConfigStack.GetConfigAt<PotLuckConfig>( 100 );
			if( potluckConfig == null ) {
				potluckConfig = new PotLuckConfig();
			}

			potluckConfig.PotEntries.Clear();
			potluckConfig.PotEntries.Add(
				new PotEntry {
					PercentChance = NecrotisConfig.Instance.DillutedEctoplasmPotDropChance,
					HardModeOnly = false,
					IsSurface = true,
					IsCaves = true,
					IsUnderworld = true,
					ItemDefs = new List<PotItemEntry> {
						new PotItemEntry {
							MinStack = 1,
							MaxStack = 1,
							ItemDef = new ItemDefinition( ModContent.ItemType<DillutedEctoplasmItem>() )
						}
					}
				}
			);

			PotLuckConfig.Instance.OverlayChanges( potluckConfig );
		}


		////////////////

		public override void AddRecipeGroups() {
			var group1 = new RecipeGroup(
				getName: () => Language.GetTextValue("LegacyMisc.37")+" "+Lang.GetItemNameValue(ItemID.StrangePlant1),
				validItems: new[] {
					(int)ItemID.StrangePlant1,
					(int)ItemID.StrangePlant2,
					(int)ItemID.StrangePlant3,
					(int)ItemID.StrangePlant4
				}
			);
			var group2 = new RecipeGroup(
				getName: () => Language.GetTextValue("LegacyMisc.37")+" "+Lang.GetItemNameValue(ItemID.Tombstone),
				validItems: new[] {
					(int)ItemID.Tombstone,
					(int)ItemID.GraveMarker,
					(int)ItemID.Gravestone,
					(int)ItemID.CrossGraveMarker,
					(int)ItemID.RichGravestone1,
					(int)ItemID.RichGravestone2,
					(int)ItemID.RichGravestone3,
					(int)ItemID.RichGravestone4,
					(int)ItemID.RichGravestone5,
					(int)ItemID.Headstone,
					(int)ItemID.Obelisk,
				}
			);

			RecipeGroup.RegisterGroup( "Necrotis:StrangePlants", group1 );
			RecipeGroup.RegisterGroup( "Necrotis:Tombstones", group2 );
		}
	}
}