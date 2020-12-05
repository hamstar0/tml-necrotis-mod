using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.Buffs;


namespace Necrotis.Items {
	public class ElixirOfLifeItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Elixir of Life" );
			this.Tooltip.SetDefault( "Removes all common afflictions, restores all health, and protects against anima loss for a while." );
		}

		public override void SetDefaults() {
			this.item.UseSound = SoundID.Item3;
			this.item.useStyle = ItemUseStyleID.EatingUsing;
			this.item.useTurn = true;
			this.item.useAnimation = 17;
			this.item.useTime = 17;
			this.item.maxStack = 30;
			this.item.consumable = true;
			this.item.width = 14;
			this.item.height = 24;
			//item.potion = true;
			this.item.buffType = this.mod.BuffType( "ElixirBuff" );
			this.item.buffTime = 30 * 60;
			this.item.value = 1000;
			this.item.rare = ItemRarityID.Orange;
		}

		public override void AddRecipes() {
			var recipe = new ElixirOfLifeItemRecipe( this );
			recipe.AddRecipe();
		}

		////

		public override void OnConsumeItem( Player player ) {
			player.ClearBuff( BuffID.Bleeding );
			player.ClearBuff( BuffID.BrokenArmor );
			player.ClearBuff( BuffID.Confused );
			player.ClearBuff( BuffID.Cursed );
			player.ClearBuff( BuffID.Darkness );
			player.ClearBuff( BuffID.Poisoned );
			player.ClearBuff( BuffID.Silenced );
			player.ClearBuff( BuffID.Slow );
			player.ClearBuff( BuffID.Weak );

			int healAmt = player.statLifeMax2 - player.statLife;

			player.statLife = healAmt;
			player.HealEffect( healAmt, true );

			player.AddBuff( ModContent.BuffType<ElixirBuff>(), 60 * 60 * 3 );
		}
	}




	class ElixirOfLifeItemRecipe : ModRecipe {
		public ElixirOfLifeItemRecipe( ElixirOfLifeItem myitem ) : base( myitem.mod ) {
			this.AddTile( TileID.DemonAltar );

			//this.AddIngredient( ItemID.LifeCrystal, 1 );
			//this.AddIngredient( ItemID.PeaceCandle, 1 );
			//this.AddIngredient( ItemID.Bottle, 1 );
			this.AddIngredient( ItemID.ShinePotion, 1 );
			this.AddIngredient( ItemID.GoldCoin, 1 );
			this.AddRecipeGroup( "Necrotis:Critters", 3 );
			
			this.SetResult( myitem );
		}

		public override bool RecipeAvailable() {
			var config = NecrotisConfig.Instance;
			return config.Get<bool>( nameof(config.ElixirRecipeEnabled) );
		}
	}
}
