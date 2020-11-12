using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


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
				if( /*!hasRoom ||*/ !isEmptyHanded ) {
					if( Timers.GetTimerTickDuration( "NecrotisPickupAlert" ) <= 0 ) {
						Main.NewText( "Only bare hands can interact with this.", Color.Yellow );
					}
					Timers.SetTimer( "NecrotisPickupAlert", 60, false, () => false );
				}
			}

			return /*hasRoom &&*/ isEmptyHanded;
		}

		public override bool OnPickup( Player player ) {
			/*if( player.HasBuff( BuffID.PotionSickness ) ) {
				return false;
			}
			player.AddBuff( BuffID.PotionSickness, 60 * 60 );*/

			var myplayer = player.GetModPlayer<NecrotisPlayer>();

			// Recover necrotis back to 0%, if needed
			if( myplayer.AnimaPercent < 0f ) {
				myplayer.AfflictAnimaPercentLoss( -myplayer.AnimaPercent, false );
			}

			float ectoFortPerc = NecrotisConfig.Instance.Get<float>(
				nameof(NecrotisConfig.DillutedEctoplasmFortifyPercent)
			);
			myplayer.AfflictAnimaPercentLoss( -ectoFortPerc, true );

			Main.PlaySound( SoundID.Drip, this.item.Center, 2 );

			return false;
		}
	}
}