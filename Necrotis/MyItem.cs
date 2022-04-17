using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace Necrotis {
	class NecrotisGlobalItem : GlobalItem {
		public override void SetDefaults( Item item ) {
			if( item.type == ItemID.Ectoplasm ) {
				item.useStyle = ItemUseStyleID.EatingUsing;
				item.useAnimation = 15;
				item.useTime = 15;
				item.useTurn = true;
				item.UseSound = SoundID.Item3;

				item.consumable = true;
				//item.potion = true;
				//item.healLife = 0;
				//item.healMana = 0;
				//item.healMana = 20;
			}
		}


		////////////////

		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			string modname = "[c/FFFF88:Necrotis] - ";
			TooltipLine tip;

			switch( item.type ) {
			case ItemID.Ectoplasm:
				tip = new TooltipLine(
					this.mod,
					"NecrotisEctoplasmTip",
					modname+"Consume to restore full anima (necrotis resist %)"
				);
				tooltips.Add( tip );
				break;
			case ItemID.ShinePotion:
			case ItemID.GoldCoin:
				tip = new TooltipLine(
					this.mod,
					"NecrotisEctoplasmTip",
					modname+"New recipe available!"
				);
				tooltips.Add( tip );
				break;
			}
		}


		////////////////

		public override bool UseItem( Item item, Player player ) {
			if( item.type == ItemID.Ectoplasm ) {
				if( player.itemAnimation > 0 && player.itemTime == 0 ) {
					player.itemTime = item.useTime;
					return true;
				}
			}
			return base.UseItem( item, player );
		}

		public override void OnConsumeItem( Item item, Player player ) {
			if( item.type == ItemID.Ectoplasm ) {
				var myplayer = player.GetModPlayer<NecrotisPlayer>();
				myplayer.SubtractAnimaPercent( -1f, false, "Canopic Jar", false );
			}
		}
	}
}
