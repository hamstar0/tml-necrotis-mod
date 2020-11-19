using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
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
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.InitializeUI();
			}
		}

		public override void Unload() {
			NecrotisMod.Instance = null;
		}


		public override void PostSetupContent() {
			var potluckConfig = ModContent.GetInstance<PotLuckConfig>();
			if( potluckConfig != null ) {
				float ectoDropPerc = NecrotisConfig.Instance.Get<float>(
					nameof(NecrotisConfig.DillutedEctoplasmPotDropChance)
				);

				potluckConfig.SetOverride(
					nameof(PotLuckConfig.PotEntries),
					new List<PotEntry> {
						new PotEntry {
							PercentChance = ectoDropPerc,
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
					}
				);
			}
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
			var group3 = new RecipeGroup(
				getName: () => Language.GetTextValue("LegacyMisc.37")+" Vase",
				validItems: new[] {
					(int)ItemID.BlueDungeonVase,
					(int)ItemID.GreenDungeonVase,
					(int)ItemID.PinkDungeonVase,
					(int)ItemID.ObsidianVase,
					//(int)ItemID.PinkVase,
				}
			);
			var group4 = new RecipeGroup(
				getName: () => Language.GetTextValue("LegacyMisc.37")+" Evil Biome Light Pet",
				validItems: new[] {
					(int)ItemID.ShadowOrb,
					(int)ItemID.CrimsonHeart,
				}
			);
			var group5 = new RecipeGroup(
				getName: () => Language.GetTextValue("LegacyMisc.37")+" Critter",
				validItems: new[] {
					(int)ItemID.Bird,
					(int)ItemID.BlueJay,
					(int)ItemID.Bunny,
					(int)ItemID.Cardinal,
					(int)ItemID.Duck,
					(int)ItemID.MallardDuck,
					(int)ItemID.Frog,
					(int)ItemID.Goldfish,
					(int)ItemID.Mouse,
					(int)ItemID.Penguin,
					(int)ItemID.Squirrel,
					(int)ItemID.GoldBird,
					(int)ItemID.GoldBunny,
					(int)ItemID.GoldFrog,
					(int)ItemID.GoldMouse,
					(int)ItemID.SquirrelGold,
				}
			);

			RecipeGroup.RegisterGroup( "Necrotis:StrangePlants", group1 );
			RecipeGroup.RegisterGroup( "Necrotis:Tombstones", group2 );
			RecipeGroup.RegisterGroup( "Necrotis:Vases", group3 );
			RecipeGroup.RegisterGroup( "Necrotis:EvilBiomeLightPets", group4 );
			RecipeGroup.RegisterGroup( "Necrotis:Critters", group5 );
		}
	}
}