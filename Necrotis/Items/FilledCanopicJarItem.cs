using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Necrotis.Items {
	public class FilledCanopicJarItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Canopic Jar (Filled)" );
			this.Tooltip.SetDefault(
				"Enchanted vessel for safely containing spiritual energies."
				+"\nUse to replenish anima."
			);
		}

		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 16;
			this.item.maxStack = 1;
			this.item.useTurn = true;
			//this.item.autoReuse = true;
			this.item.useAnimation = 15;
			this.item.useTime = 10;
			this.item.useStyle = ItemUseStyleID.SwingThrow;
			this.item.consumable = true;
			this.item.value = Item.buyPrice( 0, 30, 0, 0 );
			//this.item.UseSound = SoundID.Item108;
			this.item.rare = ItemRarityID.Orange;
		}


		////

		public override bool UseItem( Player player ) {
			bool canUse = base.UseItem( player );

			if( !canUse || player.HeldItem != this.item ) {
				return false;
			}

			var config = NecrotisConfig.Instance;
			float ectoHealPerc = config.Get<float>( nameof(config.DillutedEctoplasmAnimaPercentHeal) );
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.SubtractAnimaPercent( -ectoHealPerc, false, false );

			var emptyJar = new Item();
			emptyJar.SetDefaults( ModContent.ItemType<FilledCanopicJarItem>() );

			player.inventory[player.selectedItem] = emptyJar;

			Main.PlaySound( SoundID.Drip, player.Center, 2 );

			return canUse;
		}
	}
}
