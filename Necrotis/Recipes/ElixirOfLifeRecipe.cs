using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.Items;


namespace Necrotis.Recipes {
	class ElixirOfLifeItemRecipe : ModRecipe {
		public ElixirOfLifeItemRecipe( ElixirOfLifeItem myitem, bool adventureModeVersion ) : base( myitem.mod ) {
			//this.AddIngredient( ItemID.LifeCrystal, 1 );
			//this.AddIngredient( ItemID.PeaceCandle, 1 );
			//this.AddIngredient( ItemID.Bottle, 1 );
			//this.AddIngredient( ItemID.ShinePotion, 1 );
			if( adventureModeVersion ) {
				this.AddIngredient( ItemID.FallenStar, 1 );
				this.AddIngredient( ItemID.GoldCoin, 1 );
				this.AddRecipeGroup( "Necrotis:Critters", 3 );
				this.AddIngredient( ItemID.LesserManaPotion, 1 );
				//this.AddIngredient( ItemID.BottledHoney, 1 );
				this.needHoney = true;
			} else {
				this.AddTile( TileID.DemonAltar );
				this.AddIngredient( ItemID.GoldCoin, 1 );
				this.AddRecipeGroup( "Necrotis:Critters", 3 );
				this.AddIngredient( ItemID.BottledHoney, 1 );
			}

			this.SetResult( myitem );
		}

		public override bool RecipeAvailable() {
			var config = NecrotisConfig.Instance;
			return config.Get<bool>( nameof(config.ElixirVanillaRecipeEnabled) );
		}
	}
}
