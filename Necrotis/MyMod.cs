using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


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

		public override void Unload() {
			NecrotisMod.Instance = null;
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