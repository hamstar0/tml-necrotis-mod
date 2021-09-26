using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using Necrotis.NecrotisBehaviors;


namespace Necrotis {
	public partial class NecrotisAPI : ILoadable {
		public static float GetAnimaPercentOfPlayer( Player player ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			return myplayer.AnimaPercent;
		}


		public static void SubtractAnimaPercentFromPlayer(
					Player player,
					float percent,
					bool noPopupNumbers,
					AnimaSource source = AnimaSource.Custom,
					bool sync = true ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();
			myplayer.SubtractAnimaPercent( percent, noPopupNumbers, source, sync );
		}



		////////////////

		private IList<AnimaChangeHook> AnimaChangeHooks = new List<AnimaChangeHook>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }
	}
}
