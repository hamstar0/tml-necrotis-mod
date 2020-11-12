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
			if( item.type != ItemID.Ectoplasm ) {
				return;
			}

			var tip = new TooltipLine( this.mod, "NecrotisEctoplasmTip", "Consume to restore full anima (necrotis resist %)" );
			tooltips.Add( tip );
		}


		public override bool ConsumeItem( Item item, Player player ) {
			if( item.type != ItemID.Ectoplasm ) {
				return base.ConsumeItem( item, player );
			}
			return true;
		}

		public override void OnConsumeItem( Item item, Player player ) {
			if( item.type != ItemID.Ectoplasm ) {
				return;
			}

			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.AfflictAnimaPercentLoss( -1f );
		}
	}
}
