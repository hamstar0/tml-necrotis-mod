using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Buffs;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		private void UpdateAnimaBehaviors() {
			if( NecrotisDeBuff.CanDeBuff( player, this.AnimaPercent ) ) {
				this.player.AddBuff( ModContent.BuffType<NecrotisDeBuff>(), 2 );
			} else if( EnlivenedBuff.CanBuff( player, this.AnimaPercent ) ) {
				this.player.AddBuff( ModContent.BuffType<EnlivenedBuff>(), 2 );
			}
		}
	}
}
