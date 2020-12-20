using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Helpers.Players;


namespace Necrotis.Items {
	public partial class DillutedEctoplasmItem : ModItem {
		public override bool ItemSpace( Player player ) {
			return true;
		}

		public override void GrabRange( Player player, ref int grabRange ) {
			grabRange = 16;
		}

		////////////////

		public override bool CanPickup( Player player ) {
			//return !player.HasBuff( BuffID.PotionSickness );
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			//bool hasRoom = myplayer.AnimaPercent < 1f;
			bool isEmptyHanded = player.HeldItem?.active != true;
			bool isHoldingJar = player.HeldItem?.type == ModContent.ItemType<EmptyCanopicJarItem>();

			if( (this.item.Center - player.Center).LengthSquared() < 256f ) {	// 16 units from player center
				if( !isEmptyHanded && !isHoldingJar ) {
					if( Timers.GetTimerTickDuration("NecrotisPickupAlert") <= 0 ) {
						Main.NewText( "Only bare hands or (empty) canopic jars can interact with this.", Color.Yellow );
					}
					Timers.SetTimer( "NecrotisPickupAlert", 60, false, () => false );
				}
			}

			return isEmptyHanded || isHoldingJar;
		}

		public override bool OnPickup( Player player ) {
			if( !this.PickupIntoJarIf( player ) ) {
				this.PickupWithHands( player );
			}

			return false;
		}


		////////////////

		public bool PickupIntoJarIf( Player player ) {
			int emptyJarType = ModContent.ItemType<EmptyCanopicJarItem>();

			if( player.HeldItem?.active != true || player.HeldItem?.type != emptyJarType ) {
				return false;
			}

			PlayerItemHelpers.RemoveInventoryItemQuantity( player, 1, emptyJarType );

			int itemWho = Item.NewItem( player.position, ModContent.ItemType<FilledCanopicJarItem>(), 1, false, 0, true );
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho, 1f, 0f, 0f, 0, 0, 0 );
			}

			Main.PlaySound( SoundID.Drip, this.item.Center, 2 );

			return true;
		}

		public void PickupWithHands( Player player ) {
			var config = NecrotisConfig.Instance;
			float percHeal = config.Get<float>( nameof(config.DillutedEctoplasmAnimaPercentHeal) );

			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.SubtractAnimaPercent( -percHeal, false, false );

			Main.PlaySound( SoundID.Drip, this.item.Center, 2 );
		}
	}
}