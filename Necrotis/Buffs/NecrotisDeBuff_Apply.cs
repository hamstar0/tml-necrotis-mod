using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		public static void ApplyBehaviors( Player player, float necrotisPercent ) {
			NecrotisDeBuff.ApplyPlayerMovementBehaviors( player, necrotisPercent );
			NecrotisDeBuff.ApplyPlayerJumpingBehaviors( player, necrotisPercent );
			NecrotisDeBuff.ApplyPlayerHealthBehaviors( player, necrotisPercent );
			NecrotisDeBuff.ApplyPlayerDebuffBehaviors( player, necrotisPercent );
		}


		////

		private static void ApplyPlayerMovementBehaviors( Player player, float necrotisPercent ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			float effectPerc = 1f - necrotisPercent;

			// Percent of affliction until max effect
			float afflictPercUntilLowMove = config.Get<float>( nameof(config.DebuffPercentUntilLowestMovement) );
			float moveEffectPercRange = (effectPerc * (1f - afflictPercUntilLowMove)) + afflictPercUntilLowMove;

			// Max effect amount
			float lowestMoveEffectPercent = config.Get<float>( nameof(config.LowestPercentOfMovementProducedByDebuff) );
			float moveEffectPercent = ((1f - lowestMoveEffectPercent) * moveEffectPercRange) + lowestMoveEffectPercent;

			if( config.Get<bool>( nameof(config.DebuffReducesRunWalkSpeed) ) ) {
				player.maxRunSpeed *= moveEffectPercent;
				player.accRunSpeed = player.maxRunSpeed;
				player.moveSpeed *= moveEffectPercent;
			}
		}

		private static void ApplyPlayerJumpingBehaviors( Player player, float necrotisPercent ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			float effectPerc = 1f - necrotisPercent;

			// Percent of affliction until max effect
			float afflictPercUntilLowJump = config.Get<float>( nameof(config.DebuffPercentUntilLowestJumping) );
			float moveEffectPercRange = (effectPerc * (1f - afflictPercUntilLowJump)) + afflictPercUntilLowJump;

			// Max effect amount
			float lowestMoveEffectPercent = config.Get<float>( nameof(config.LowestPercentOfJumpingProducedByDebuff ) );
			float moveEffectPercent = ((1f - lowestMoveEffectPercent) * moveEffectPercRange) + lowestMoveEffectPercent;

			if( config.Get<bool>( nameof(config.DebuffReducesJumpHeight) ) ) {
				int maxJump = (int)( (float)Player.jumpHeight * moveEffectPercent );
				if( player.jump > maxJump ) {
					player.jump = maxJump;
				}
			}
		}


		public static void ApplyPlayerLifeRegenBehaviors( Player player, float necrotisPercent ) {
			player.lifeRegen = (int)((float)player.lifeRegen * (1f - necrotisPercent));
		}


		private static void ApplyPlayerHealthBehaviors( Player player, float afflictPerc ) {
			NecrotisConfig config = NecrotisConfig.Instance;

			float effectPerc = 1f - afflictPerc;

			float reducedMaxHp = (float)(player.statLifeMax2 - 100) * effectPerc;
			player.statLifeMax2 = 100 + (int)reducedMaxHp;

			player.lifeRegen = (int)((float)player.lifeRegen * effectPerc);
		}


		////

		private static void ApplyPlayerDebuffBehaviors( Player player, float necrotisPercent ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			var percBleed = config.Get<NullablePercent>( nameof(NecrotisConfig.DebuffPercentBeforeBleeding) );
			var percPoison = config.Get<NullablePercent>( nameof(NecrotisConfig.DebuffPercentBeforePoisoned) );
			var percCurInf = config.Get<NullablePercent>( nameof(NecrotisConfig.DebuffPercentBeforeCursedInferno) );

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
