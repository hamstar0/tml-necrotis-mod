using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.Buffs;
using Necrotis.Recipes;


namespace Necrotis.Items {
	public class ElixirOfLifeItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Elixir of Life" );
			this.Tooltip.SetDefault( "Removes all common afflictions, restores all health, and protects against anima loss for a while" );
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
			this.item.buffType = ModContent.BuffType<RespiritedBuff>();
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
			var config = NecrotisConfig.Instance;

			player.ClearBuff( BuffID.Bleeding );
			player.ClearBuff( BuffID.BrokenArmor );
			player.ClearBuff( BuffID.Confused );
			player.ClearBuff( BuffID.Cursed );
			player.ClearBuff( BuffID.Darkness );
			player.ClearBuff( BuffID.Poisoned );
			player.ClearBuff( BuffID.Silenced );
			player.ClearBuff( BuffID.Slow );
			player.ClearBuff( BuffID.Weak );
			player.ClearBuff( BuffID.Chilled );
			player.ClearBuff( BuffID.Frozen );
			player.ClearBuff( BuffID.Blackout );
			player.ClearBuff( BuffID.Venom );

			int healAmt = player.statLifeMax2 - player.statLife;

			player.statLife = healAmt;
			player.HealEffect( healAmt, true );

			int seconds = config.Get<int>( nameof(config.ElixirDurationInSeconds) );
			int ticks = seconds * 60;   //60 * 60 * 3

			player.AddBuff( ModContent.BuffType<RespiritedBuff>(), ticks );
		}
	}
}
