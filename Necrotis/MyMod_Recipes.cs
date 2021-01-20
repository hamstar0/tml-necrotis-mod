using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
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