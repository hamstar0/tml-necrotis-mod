using Microsoft.Xna.Framework;
using Terraria;
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
			//bool hasRoom = myplayer.NecrotisResistPercent < 0.95f;
			bool isEmptyHanded = player.HeldItem?.active != true;

			if( /*!hasRoom ||*/ !isEmptyHanded ) {
				if( Timers.GetTimerTickDuration("NecrotisPickupAlert") <= 0 ) {
					Main.NewText( "Only bare hands can interact with this.", Color.Yellow );
				}
				Timers.SetTimer( "NecrotisPickupAlert", 60, false, () => {
					return false;
				} );
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
			if( myplayer.NecrotisResistPercent < 0f ) {
				myplayer.AfflictNecrotis( -myplayer.NecrotisResistPercent, false );
			}

			float ectoFortPerc = NecrotisConfig.Instance.Get<float>(
				nameof(NecrotisConfig.DillutedEctoplasmFortifyPercent)
			);
			myplayer.AfflictNecrotis( -ectoFortPerc, true );

			return false;
		}
	}
}