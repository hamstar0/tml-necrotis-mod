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
			//bool hasRoom = myplayer.AnimaPercent < 0.95f;
			bool isEmptyHanded = player.HeldItem?.active != true;

			if( this.item.active && (this.item.Center - player.Center).LengthSquared() < 256f ) {
				bool isHoldingJar = player.HeldItem?.active != true || player.HeldItem.type != ModContent.ItemType<EmptyCanopicJarItem>();

				if( isHoldingJar || !isEmptyHanded ) {
					if( Timers.GetTimerTickDuration( "NecrotisPickupAlert" ) <= 0 ) {
						Main.NewText( "Only bare hands or canopic jars can interact with this.", Color.Yellow );
					}
					Timers.SetTimer( "NecrotisPickupAlert", 60, false, () => false );
				}
			}

			return /*hasRoom &&*/ isEmptyHanded;
		}

		public override bool OnPickup( Player player ) {
			var config = NecrotisConfig.Instance;
			float percHeal = config.Get<float>( nameof(config.DillutedEctoplasmAnimaPercentHeal) );

			if( player.HeldItem?.active != true || player.HeldItem.type != ModContent.ItemType<EmptyCanopicJarItem>() ) {
				var filledJar = new Item();
				filledJar.SetDefaults( ModContent.ItemType<FilledCanopicJarItem>() );

				player.inventory[ player.selectedItem ] = filledJar;
			} else {
				var myplayer = player.GetModPlayer<NecrotisPlayer>();
				myplayer.SubtractAnimaPercent( -percHeal, false, false );
			}

			Main.PlaySound( SoundID.Drip, this.item.Center, 2 );

			return false;
		}
	}
}