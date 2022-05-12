using System;
using Terraria;
using Terraria.ID;


namespace Necrotis.NecrotisBehaviors {
	partial class NecrotisBehavior {
		public static float CalculateViewVisibilityScale( float necrotisPercent ) {
			float lightPerc = Math.Max( 1f - necrotisPercent, 0f );
			var config = NecrotisConfig.Instance;
			float minLight = config.Get<float>( nameof(config.LowestPercentViewVisibilityFromDebuff) );

			return minLight + ((1f - minLight) * lightPerc);
		}


		////////////////

		public static void UpdateViewVisibilityScale( ref float visibility, float necrotisPercent ) {
			visibility *= NecrotisBehavior.CalculateViewVisibilityScale( necrotisPercent );
		}


		////////////////

		public static void ApplyPlayerLifeRegenBehaviors( Player player, float necrotisPercent ) {
			float effectPerc = 1f - necrotisPercent;
			player.lifeRegen = (int)((float)player.lifeRegen * effectPerc);
		}


		internal static void ApplyPlayerHealthBehaviors( Player player, float necrotisPercent, out int maxHpLost ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			if( !config.Get<bool>( nameof(config.DebuffReducesMaxLifeWhenActive) ) ) {
				maxHpLost = 0;
				return;
			}

			float effectPerc = 1f - necrotisPercent;

			maxHpLost = player.statLifeMax2;

			float reducedMaxHp = (float)(player.statLifeMax2 - 100) * effectPerc;
			player.statLifeMax2 = Math.Max( 100 + (int)reducedMaxHp, 100 );

			player.lifeRegen = (int)( (float)player.lifeRegen * effectPerc );

			maxHpLost -= player.statLifeMax2;
		}


		////
		
		internal static void ApplyPlayerDebuffBehaviors( Player player, float necrotisPercent ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			var percBleed = config.Get<NullablePercent>( nameof(config.DebuffPercentBeforeBleeding) );
			var percPoison = config.Get<NullablePercent>( nameof(config.DebuffPercentBeforePoisoned) );
			var percCurInf = config.Get<NullablePercent>( nameof(config.DebuffPercentBeforeCursedInferno) );

			if( necrotisPercent >= (percBleed?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.Bleeding, 3 );
			}
			if( necrotisPercent >= (percPoison?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.Poisoned, 3 );
			}
			if( necrotisPercent >= (percCurInf?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.CursedInferno, 3 );
			}
		}
	}
}
