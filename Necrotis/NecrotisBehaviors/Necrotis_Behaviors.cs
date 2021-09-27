using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


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
			SoulBarriers.SoulBarriersAPI.AddBarrierCreateHook( (barrier) => {
				if( !(barrier is SoulBarriers.Barriers.BarrierTypes.Spherical.Personal.PersonalBarrier) ) {
					return;
				}

				void OnAnimaChange(
							Player plr,
							float oldAnimaPercent,
							ref string context,
							ref float animaPercentLost,
							ref bool quiet ) {
					if( !barrier.IsActive ) {
						return;
					}
					if( animaPercentLost < 0f ) {
						return;
					}

//LogLibraries.Log( "CALLING OnAnimaChange "+barrier.ToString()+" - "+percentLost );
					double oldBarrierStr = barrier.Strength;
					barrier.ApplyMetaphysicalHit( null, animaPercentLost, false );

					double amtChanged = oldBarrierStr - barrier.Strength;
					animaPercentLost -= (float)amtChanged;

					quiet = Math.Abs(amtChanged) < 1d;

					context += " +PBG";
				}

				NecrotisAPI.AddAnimaChangeHook( OnAnimaChange );
			} );
		}
	}
}
