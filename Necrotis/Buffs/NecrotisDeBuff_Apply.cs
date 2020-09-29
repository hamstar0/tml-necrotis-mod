using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		public static void ApplyEffect( Player player, float percent ) {
			NecrotisDeBuff.ApplyMovementEffects( player, percent );
			NecrotisDeBuff.ApplyJumpingEffects( player, percent );
			NecrotisDeBuff.ApplyHealthEffects( player, percent );
			NecrotisDeBuff.ApplyDebuffEffects( player, percent );
		}


		////

		private static void ApplyMovementEffects( Player player, float afflictPerc ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			float effectPerc = 1f - afflictPerc;

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

		private static void ApplyJumpingEffects( Player player, float afflictPerc ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			float effectPerc = 1f - afflictPerc;

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


		public static void ApplyLifeRegenEffect( Player player, float afflictPerc ) {
			player.lifeRegen = (int)((float)player.lifeRegen * (1f - afflictPerc));
		}


		private static void ApplyHealthEffects( Player player, float afflictPerc ) {
			NecrotisConfig config = NecrotisConfig.Instance;

			float effectPerc = 1f - afflictPerc;

			float reducedMaxHp = (float)(player.statLifeMax2 - 100) * effectPerc;
			player.statLifeMax2 = 100 + (int)reducedMaxHp;

			player.lifeRegen = (int)((float)player.lifeRegen * effectPerc);
		}


		private static void ApplyDebuffEffects( Player player, float afflictPerc ) {
			NecrotisConfig config = NecrotisConfig.Instance;
			var percBleed = config.Get<NullablePercent>( nameof(NecrotisConfig.DebuffPercentBeforeBleeding) );
			var percPoison = config.Get<NullablePercent>( nameof(NecrotisConfig.DebuffPercentBeforePoisoned) );
			var percCurInf = config.Get<NullablePercent>( nameof(NecrotisConfig.DebuffPercentBeforeCursedInferno) );

			if( afflictPerc >= (percBleed?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.Bleeding, 3 );
			}
			if( afflictPerc >= (percPoison?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.Poisoned, 3 );
			}
			if( afflictPerc >= (percCurInf?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.CursedInferno, 3 );
			}
		}


		////////////////

		public static void ApplyVisualFX( Player player, ref float r, ref float g, ref float b ) {
			r *= 0.7f;
			//g *= 0.85f;
			b *= 0.7f;
		}
	}
}
