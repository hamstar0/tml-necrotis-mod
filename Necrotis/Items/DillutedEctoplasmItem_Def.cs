using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace Necrotis.Items {
	public partial class DillutedEctoplasmItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Dilluted Ectoplasm" );
			this.Tooltip.SetDefault( "Psycho-kinetic residue of spiritual entities. Dilluted." );

			int mytype = ItemType<DillutedEctoplasmItem>();
			ItemID.Sets.ItemNoGravity[ mytype ] = true;
		}

		public override void SetDefaults() {
			this.item.width = 16;
			this.item.height = 16;
			this.item.value = 0;
			this.item.rare = 2;
		}


		////////////////

		public override void AddRecipes() {
			if( !NecrotisConfig.Instance.Get<bool>( nameof(NecrotisConfig.DillutedEctoplasmRecipeEnabled) ) ) {
				return;
			}

			ModRecipe recipe1 = new ModRecipe( this.mod );
			recipe1.AddIngredient( ItemID.Gel, 10 );
			recipe1.AddRecipeGroup( "Necrotis:StrangePlants", 1 );
			recipe1.AddRecipeGroup( "Necrotis:Tombstone", 1 );
			recipe1.AddTile( TileID.Bottles );
			recipe1.SetResult( this );
			recipe1.AddRecipe();

			ModRecipe recipe2 = new ModRecipe( this.mod );
			recipe2.AddIngredient( ItemID.Gel, 10 );
			recipe2.AddRecipeGroup( "Necrotis:StrangePlants", 1 );
			recipe2.AddIngredient( ItemID.Bone, 1 );
			recipe2.AddTile( TileID.Bottles );
			recipe2.SetResult( this );
			recipe2.AddRecipe();
		}
	}
}