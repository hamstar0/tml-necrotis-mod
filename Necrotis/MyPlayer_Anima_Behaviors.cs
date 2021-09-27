using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Necrotis.Buffs;
using Necrotis.Net;
using Necrotis.NecrotisBehaviors;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		private void UpdateAnimaBehaviors() {
			bool isNew = false;

			if( EnlivenedBuff.CanBuff(this.player, this.AnimaPercent) ) {
				isNew = this.ApplyEnlivened();
			} else if( NecrotisNatusDeBuff.CanBuff(this.player, this.AnimaPercent) ) {
				isNew = this.ApplyNecrotisNatus();
			} else if( NecrotisOmnisDeBuff.CanBuff(this.player, this.AnimaPercent) ) {
				isNew = this.ApplyNecrotisOmnis();
			}

			if( isNew ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					PlayerAnimaSyncProtocol.BroadcastFromClientToAll( this, "Sync Only" );
				}
			}
		}


		////

		private bool ApplyEnlivened() {
			int buffType = ModContent.BuffType<EnlivenedBuff>();
			bool isNew = false;

			if( Main.netMode != NetmodeID.Server ) {
				isNew = !this.player.HasBuff( buffType );
			}

			this.player.AddBuff( buffType, 2 );

			return isNew;
		}

		
		private bool ApplyNecrotisNatus() {
			int debuffType = ModContent.BuffType<NecrotisNatusDeBuff>();
			bool isNew = false;

			if( Main.netMode != NetmodeID.Server ) {
				isNew = !this.player.HasBuff( debuffType );

				if( isNew ) {
					Main.NewText( "Your limbs begin feeling stiff.", Color.OrangeRed );
				}
			}

			this.player.AddBuff( debuffType, 2 );

			return isNew;
		}


		private bool ApplyNecrotisOmnis() {
			int debuffType = ModContent.BuffType<NecrotisOmnisDeBuff>();
			bool isNew = false;

			if( Main.netMode != NetmodeID.Server ) {
				isNew = !this.player.HasBuff( debuffType );

				if( isNew ) {
					Main.NewText( "Necrotis sets in. Darkness approaches...", Color.OrangeRed );
				}
			}

			this.player.AddBuff( debuffType, 2 );

			return isNew;
		}
	}
}
