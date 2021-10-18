using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.Items;


namespace Necrotis.Recipes {
	class ElixirOfLifeItemRecipe : ModRecipe {
		public ElixirOfLifeItemRecipe( ElixirOfLifeItem myitem ) : base( myitem.mod ) {
			this.AddTile( TileID.DemonAltar );

			//this.AddIngredient( ItemID.LifeCrystal, 1 );
			//this.AddIngredient( ItemID.PeaceCandle, 1 );
			//this.AddIngredient( ItemID.Bottle, 1 );
			//this.AddIngredient( ItemID.ShinePotion, 1 );
			this.AddIngredient( ItemID.FallenStar, 1 );
			this.AddIngredient( ItemID.GoldCoin, 1 );
			this.AddRecipeGroup( "Necrotis:Critters", 3 );
			//this.AddIngredient( ItemID.BottledHoney, 1 );
			this.needHoney = true;
			
			this.SetResult( myitem );
		}

		public override bool RecipeAvailable() {
			var config = NecrotisConfig.Instance;
			return config.Get<bool>( nameof(config.ElixirRecipeEnabled) );
		}
	}
}
