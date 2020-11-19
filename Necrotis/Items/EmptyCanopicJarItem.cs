using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Necrotis.Items {
	class EmptyCanopicJarItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Canopic Jar (Empty)" );
			this.Tooltip.SetDefault(
				"Enchanted vessel for safely containing spiritual energies."
				+"\nHold jar near wisps of soft ectoplasm to store for later use."
			);
		}

		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 16;
			this.item.maxStack = 1;
			this.item.useTime = 10;
			this.item.useStyle = ItemUseStyleID.SwingThrow;
			this.item.consumable = true;
			this.item.value = Item.buyPrice( 0, 25, 0, 0 );
			this.item.rare = ItemRarityID.Orange;
		}

		////

		public override void AddRecipes() {
			var config = NecrotisConfig.Instance;
			if( !config.Get<bool>(nameof(config.CanopicJarRecipeEnabled)) ) {
				return;
			}

			var recipe = new ModRecipe( this.mod );
			recipe.AddRecipeGroup( "Necrotis:Vases", 1 );
			recipe.AddRecipeGroup( "Necrotis:Tombstones", 1 );
			recipe.AddRecipeGroup( "Necrotis:EvilBiomeLightPet", 1 );
			recipe.AddIngredient( ItemID.Bone, 10 );
			recipe.AddIngredient( ItemID.WaterCandle, 1 );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}
