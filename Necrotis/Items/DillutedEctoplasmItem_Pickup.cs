using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


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
			return myplayer.NecrotisResistPercent < 0.95f;
		}

		public override bool OnPickup( Player player ) {
			/*if( player.HasBuff( BuffID.PotionSickness ) ) {
				return false;
			}
			player.AddBuff( BuffID.PotionSickness, 60 * 60 );*/

			var myplayer = player.GetModPlayer<NecrotisPlayer>();

			// Recover necrotis back to 0%, if needed
			if( myplayer.NecrotisResistPercent < 0f ) {
				myplayer.AddNecrotis( -myplayer.NecrotisResistPercent, false );
			}

			myplayer.AddNecrotis( -NecrotisConfig.Instance.DillutedEctoplasmFortifyPercent, true );

			return false;
		}
	}
}