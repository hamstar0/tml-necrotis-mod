using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.Items;


namespace Necrotis.Recipes {
	class EmptyCanopicJarItemRecipe : ModRecipe {
		public EmptyCanopicJarItemRecipe( EmptyCanopicJarItem myitem ) : base( myitem.mod ) {
			this.AddTile( TileID.DemonAltar );

			this.AddRecipeGroup( "Necrotis:Vases", 1 );
			this.AddRecipeGroup( "Necrotis:Tombstones", 1 );
			this.AddRecipeGroup( "Necrotis:EvilBiomeLightPets", 1 );
			this.AddIngredient( ItemID.Bone, 10 );
			this.AddIngredient( ItemID.WaterCandle, 1 );

			this.SetResult( myitem );
		}

		public override bool RecipeAvailable() {
			var config = NecrotisConfig.Instance;
			return config.Get<bool>( nameof(config.CanopicJarRecipeEnabled) );
		}
	}
}
