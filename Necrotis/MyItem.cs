using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Necrotis {
	class NecrotisGlobalItem : GlobalItem {
		public override void SetDefaults( Item item ) {
			if( item.type == ItemID.Ectoplasm ) {
				item.consumable = true;
			}
		}


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


		public override bool ConsumeItem( Item item, Player player ) {
			if( item.type == ItemID.Ectoplasm ) {
				return true;
			}
			return base.ConsumeItem( item, player );
		}

		public override void OnConsumeItem( Item item, Player player ) {
			if( item.type == ItemID.Ectoplasm ) {
				var myplayer = player.GetModPlayer<NecrotisPlayer>();
				myplayer.SubtractAnimaPercent( -1f, false, false );
			}
		}
	}
}
