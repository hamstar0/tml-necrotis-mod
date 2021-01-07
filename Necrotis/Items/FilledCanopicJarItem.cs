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
			this.item.useStyle = ItemUseStyleID.EatingUsing;
			this.item.useAnimation = 15;
			this.item.useTime = 15;
			this.item.consumable = true;
			this.item.value = Item.buyPrice( 0, 10, 0, 0 );
			this.item.healLife = 10;
			this.item.healMana = 10;
			//this.item.UseSound = SoundID.Item108;
			this.item.rare = ItemRarityID.Orange;
		}


		////

		//public override void OnConsumeItem( Player player ) {
		public override bool UseItem( Player player ) {
			var config = NecrotisConfig.Instance;
			float ectoHealPerc = config.Get<float>( nameof(config.DillutedEctoplasmAnimaPercentHeal) );
			var myplayer = player.GetModPlayer<NecrotisPlayer>();

			myplayer.SubtractAnimaPercent( -ectoHealPerc, false, false );

			int itemWho = Item.NewItem( player.position, ModContent.ItemType<EmptyCanopicJarItem>(), 1, false, 0, true );
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho, 1f, 0f, 0f, 0, 0, 0 );
			}

			Main.PlaySound( SoundID.Drip, player.Center, 2 );

			return base.UseItem( player );
		}
	}
}
