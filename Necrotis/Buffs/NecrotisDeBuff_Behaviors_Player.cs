using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		private static void ApplyPlayerMovementBehaviors( Player player, float necrotisPercent ) {
			var config = NecrotisConfig.Instance;

			if( config.Get<bool>( nameof(config.DebuffReducesRunWalkSpeed) ) ) {
				float effectPercent = 1f - necrotisPercent;

				NecrotisDeBuff.ApplyPlayerMovementReductionBehaviors( player, effectPercent );
			}
		}

		private static void ApplyPlayerMovementReductionBehaviors( Player player, float effectPercent ) {
			var config = NecrotisConfig.Instance;
			var afflictPercUntilLowMoveIf = config.Get<NullablePercent>( nameof(config.DebuffPercentUntilLowestMovement) );
			var lowestMoveEffectPercentIf = config.Get<NullablePercent>( nameof(config.LowestPercentOfMovementProducedByDebuff) );

			if( afflictPercUntilLowMoveIf == null || lowestMoveEffectPercentIf == null ) {
				return;
			}

			// Percent of affliction until max effect
			float afflictPercUntilLowMove = afflictPercUntilLowMoveIf.Percent;
			float lowestMoveEffectPercent = lowestMoveEffectPercentIf.Percent;

			float moveEffectPercRange = ( effectPercent * (1f - afflictPercUntilLowMove) ) + afflictPercUntilLowMove;

			// Max effect amount
			float moveEffectPercent = ( (1f - lowestMoveEffectPercent) * moveEffectPercRange ) + lowestMoveEffectPercent;

			if( config.Get<bool>( nameof(config.DebuffReducesRunWalkSpeed) ) ) {
				player.maxRunSpeed *= moveEffectPercent;
				player.accRunSpeed = player.maxRunSpeed;
				player.moveSpeed *= moveEffectPercent;
			}
		}


		////

		private static void ApplyPlayerJumpingBehaviors( Player player, float necrotisPercent ) {
			var config = NecrotisConfig.Instance;
			
			if( config.Get<bool>( nameof(config.DebuffReducesJumpHeight) ) ) {
				float effectPerc = 1f - necrotisPercent;

				NecrotisDeBuff.ApplyPlayerJumpingReductionBehaviors( player, effectPerc );
			}
		}

		private static void ApplyPlayerJumpingReductionBehaviors( Player player, float effectPercent ) {
			var config = NecrotisConfig.Instance;
			var necPercUntilLowJumpIf = config.Get<NullablePercent>( nameof(config.DebuffPercentUntilLowestJumping) );
			var lowestMoveEffectPercentIf = config.Get<NullablePercent>( nameof(config.LowestPercentOfJumpingProducedByDebuff) );

			if( necPercUntilLowJumpIf == null || lowestMoveEffectPercentIf == null ) {
				return;
			}

			// Percent of affliction until max effect
			float necPercUntilLowJump = necPercUntilLowJumpIf.Percent;
			float moveEffectPercRange = (effectPercent * (1f - necPercUntilLowJump)) + necPercUntilLowJump;

			// Max effect amount
			float lowestMoveEffectPercent = lowestMoveEffectPercentIf.Percent;
			float moveEffectPercent = ( (1f - lowestMoveEffectPercent) * moveEffectPercRange ) + lowestMoveEffectPercent;

			int maxJump = (int)( (float)Player.jumpHeight * moveEffectPercent );
			if( player.jump > maxJump ) {
				player.jump = maxJump;
			}
		}


		////

		public static void ApplyPlayerLifeRegenBehaviors( Player player, float necrotisPercent ) {
			player.lifeRegen = (int)((float)player.lifeRegen * (1f - necrotisPercent));
		}


		private static void ApplyPlayerHealthBehaviors( Player player, float afflictPerc ) {
			NecrotisConfig config = NecrotisConfig.Instance;

			if( config.Get<bool>( nameof(config.DebuffReducesMaxLifeWhenActive) ) ) {
				float effectPerc = 1f - afflictPerc;

				float reducedMaxHp = (float)(player.statLifeMax2 - 100) * effectPerc;
				player.statLifeMax2 = 100 + (int)reducedMaxHp;

				player.lifeRegen = (int)( (float)player.lifeRegen * effectPerc );
			}
		}


		////

		private static void ApplyPlayerDebuffBehaviors( Player player, float necrotisPercent ) {
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
