using System;
using Terraria;


namespace Necrotis.NecrotisBehaviors {
	partial class NecrotisBehavior {
		public static void ApplyBehaviors(
					Player player,
					float necrotisPercent,
					out int lastMaxHpLost,
					out float movePercent ) {
			NecrotisBehavior.ApplyPlayerMovementBehaviors( player, necrotisPercent, out movePercent );
			NecrotisBehavior.ApplyPlayerJumpingBehaviors( player, necrotisPercent );
			NecrotisBehavior.ApplyPlayerHealthBehaviors( player, necrotisPercent, out lastMaxHpLost );
			NecrotisBehavior.ApplyPlayerDebuffBehaviors( player, necrotisPercent );

			if( necrotisPercent >= 1f ) {
				NecrotisBehavior.ApplyWorldBehaviors( player );
			}
		}


		////////////////

		public static void ApplyAnimaDefenses_PBG_WeakRef() {
			SoulBarriers.SoulBarriersAPI.AddBarrierCreateHook( ( barrier ) => {
				if( !(barrier is SoulBarriers.Barriers.BarrierTypes.Spherical.Personal.PersonalBarrier) ) {
					return;
				}

				void OnAnimaChange( Player player, float oldPercent, ref float percentLost, ref bool quiet ) {
					barrier.ApplyMetaphysicalHit( null, percentLost, true );

					float amtChanged = oldPercent - (float)barrier.Strength;
					percentLost -= amtChanged;
				}

				NecrotisAPI.AddAnimaChangeHook( OnAnimaChange );
			} );
		}
	}
}
