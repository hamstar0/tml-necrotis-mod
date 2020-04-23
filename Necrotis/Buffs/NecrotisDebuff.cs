using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using HamstarHelpers.Helpers.World;


namespace Necrotis.Buffs {
	partial class NecrotisDeBuff : ModBuff {
		public static bool IsBeingAfflicted( Player player ) {
			return player.ZoneDungeon && player.position.Y > ( WorldHelpers.RockLayerTopTileY << 4 );
		}

		/*public static float GetPercentAfflicted( Player player ) {
			int buffIdx = player.FindBuffIndex( ModContent.BuffType<NecrotisDebuff>() );
			if( buffIdx == -1 ) {
				return 0f;
			}

			int currDuration = player.buffTime[ buffIdx ];
			return (float)currDuration / (float)NecrotisConfig.Instance.NecrotisMaxAfflictionTickDuration;
		}*/


		////////////////

		public static void ApplyEffect( Player player, float percent ) {
			NecrotisDeBuff.ApplyMovementEffects( player, percent );
			NecrotisDeBuff.ApplyHealthEffects( player, percent );
			NecrotisDeBuff.ApplyDebuffEffects( player, percent );
		}


		private static void ApplyMovementEffects( Player player, float afflictPerc ) {
			NecrotisConfig config = NecrotisConfig.Instance;

			float effectPerc = 1f - afflictPerc;

			// Percent of affliction until max effect
			float afflictPercUntilMaxMoveEffect = config.DebuffPercentUntilLowestMovement;
			float moveEffectPercRange = (effectPerc * (1f - afflictPercUntilMaxMoveEffect)) + afflictPercUntilMaxMoveEffect;

			// Max effect amount
			float lowestMoveEffectPercent = config.LowestPercentOfMovementProducedByDebuff;
			float moveEffectPercent = ((1f - lowestMoveEffectPercent) * moveEffectPercRange) + lowestMoveEffectPercent;

			if( config.DebuffReducesRunWalkSpeed ) {
				player.maxRunSpeed *= moveEffectPercent;
				player.accRunSpeed = player.maxRunSpeed;
				player.moveSpeed *= moveEffectPercent;
			}

			if( config.DebuffReducesJumpHeight ) {
				int maxJump = (int)( (float)Player.jumpHeight * moveEffectPercent );
				if( player.jump > maxJump ) {
					player.jump = maxJump;
				}
			}
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

			if( afflictPerc >= (config.DebuffPercentBeforeBleeding?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.Bleeding, 3 );
			}
			if( afflictPerc >= (config.DebuffPercentBeforePoisoned?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.Poisoned, 3 );
			}
			if( afflictPerc >= (config.DebuffPercentBeforeCursedInferno?.Percent ?? 1.0001f) ) {
				player.AddBuff( BuffID.CursedInferno, 3 );
			}
		}



		////////////////
		
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Necrotis" );
			this.Description.SetDefault(
				"You feel your life energy draining"
				+ "\n" + "Worsens as it increases"
			);

			Main.debuff[this.Type] = true;
			//Main.buffNoTimeDisplay[this.Type] = true;
			//Main.buffNoSave[this.Type] = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			var myplayer = player.GetModPlayer<NecrotisPlayer>();

			float percent = -myplayer.NecrotisResistPercent;
			if( percent <= 0f ) {
				return;
			}

			NecrotisDeBuff.ApplyEffect( player, percent );
		}
	}
}
