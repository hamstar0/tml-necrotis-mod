using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.Recipes;


namespace Necrotis.Items {
	public class EmptyCanopicJarItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Canopic Jar (Empty)" );
			this.Tooltip.SetDefault( "Enchanted vessel for preserving mummified viscera of the dead"
				+"\nAble to safely containing spiritual energies"
				+"\nHold jar near wisps of soft ectoplasm to store for later use"
			);
		}

		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 16;
			this.item.maxStack = 15;
			this.item.useStyle = ItemUseStyleID.EatingUsing;
			this.item.useTurn = true;
			this.item.useAnimation = 17;
			this.item.useTime = 17;
			this.item.consumable = true;
			this.item.value = Item.buyPrice( 0, 10, 0, 0 );
			this.item.rare = ItemRarityID.Orange;
		}

		////

		public override void AddRecipes() {
			var recipe = new EmptyCanopicJarItemRecipe( this );
			recipe.AddRecipe();
		}
	}
}
