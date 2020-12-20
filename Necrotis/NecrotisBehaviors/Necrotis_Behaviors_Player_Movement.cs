using System;
using Terraria;


namespace Necrotis.NecrotisBehaviors {
	partial class NecrotisBehavior {
		internal static void ApplyPlayerMovementBehaviors( Player player, float necrotisPercent, out float effectPercent ) {
			var config = NecrotisConfig.Instance;
			if( !config.Get<bool>( nameof(config.DebuffReducesRunWalkSpeed) ) ) {
				effectPercent = 0f;
				return;
			}

			effectPercent = 1f - necrotisPercent;

			NecrotisBehavior.ApplyPlayerMovementReductionBehaviors( player, effectPercent );
		}

		internal static void ApplyPlayerMovementReductionBehaviors( Player player, float necrotisPercent ) {
			var config = NecrotisConfig.Instance;
			var afflictPercUntilLowMoveIf = config.Get<NullablePercent>( nameof(config.DebuffPercentUntilLowestMovement) );
			var lowestMoveEffectPercentIf = config.Get<NullablePercent>( nameof(config.LowestPercentOfMovementProducedByDebuff) );

			if( afflictPercUntilLowMoveIf == null || lowestMoveEffectPercentIf == null ) {
				return;
			}

			// Percent of affliction until max effect
			float afflictPercUntilLowMove = afflictPercUntilLowMoveIf.Percent;
			float lowestMoveEffectPercent = lowestMoveEffectPercentIf.Percent;

			float moveEffectPercRange = ( necrotisPercent * (1f - afflictPercUntilLowMove) ) + afflictPercUntilLowMove;

			// Max effect amount
			float moveEffectPercent = ( (1f - lowestMoveEffectPercent) * moveEffectPercRange ) + lowestMoveEffectPercent;

			if( config.Get<bool>( nameof(config.DebuffReducesRunWalkSpeed) ) ) {
				player.maxRunSpeed *= moveEffectPercent;
				player.accRunSpeed = player.maxRunSpeed;
				player.moveSpeed *= moveEffectPercent;
			}
		}


		////////////////

		internal static void ApplyPlayerJumpingBehaviors( Player player, float necrotisPercent ) {
			var config = NecrotisConfig.Instance;

			if( config.Get<bool>( nameof( config.DebuffReducesJumpHeight ) ) ) {
				float effectPerc = 1f - necrotisPercent;

				NecrotisBehavior.ApplyPlayerJumpingReductionBehaviors( player, effectPerc );
			}
		}

		internal static void ApplyPlayerJumpingReductionBehaviors( Player player, float effectPercent ) {
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
	}
}
