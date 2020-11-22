using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Buffs;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		private void UpdateAnimaBehaviors() {
			if( this.NecrotisPercent > 0f ) {
				if( this.NecrotisPercent < 1f ) {
					this.player.AddBuff( ModContent.BuffType<NecrotisNatusDeBuff>(), 2 );
				} else {
					this.player.AddBuff( ModContent.BuffType<NecrotisOmnisDeBuff>(), 2 );
				}
			} else if( EnlivenedBuff.CanBuff( player, this.AnimaPercent ) ) {
				this.player.AddBuff( ModContent.BuffType<EnlivenedBuff>(), 2 );
			}
		}
	}
}
