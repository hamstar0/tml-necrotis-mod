using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Necrotis.Items {
	public class EmptyCanopicJarItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Canopic Jar (Empty)" );
			this.Tooltip.SetDefault(
				"Enchanted vessel for safely containing spiritual energies."
				+"\nHold jar near wisps of soft ectoplasm to store for later use."
			);
		}

		public override void SetDefaults() {
			this.item.useStyle = ItemUseStyleID.EatingUsing;
			this.item.useTurn = true;
			this.item.useAnimation = 17;
			this.item.useTime = 17;
			this.item.width = 12;
			this.item.height = 16;
			this.item.maxStack = 1;
			this.item.consumable = true;
			this.item.value = Item.buyPrice( 0, 25, 0, 0 );
			this.item.rare = ItemRarityID.Orange;
		}

		////

		public override void AddRecipes() {
			var recipe = new EmptyCanopicJarItemRecipe( this );
			recipe.AddRecipe();
		}
	}




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
			return config.Get<bool>(nameof(config.CanopicJarRecipeEnabled));
		}
	}
}
