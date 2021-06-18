using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;
using Necrotis.Libraries.Services.FX;
using Necrotis.NecrotisBehaviors;
using Necrotis.Items;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		public float AnimaPercent { get; private set; } = 0.51f;
		public float CurrentAnimaPercentChangeRate { get; private set; } = 0f;


		////

		public float NecrotisPercent => Math.Max( 1f - (this.AnimaPercent * 2f), 0f );  // below 50% anima

		public Vector2 AnkhHUDDisplayOffset { get; internal set; } = default;


		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Initialize() {
			this.AnimaPercent = 0.51f;
			this.CurrentAnimaPercentChangeRate = 0f;
		}

		////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey( "anima_percent" ) ) {
				this.AnimaPercent = MathHelper.Clamp( tag.GetFloat("anima_percent"), 0f, 1f );
			}

			if( tag.ContainsKey( "ankh_offset_x" ) ) {
				this.AnkhHUDDisplayOffset = new Vector2(
					tag.GetInt( "ankh_offset_x" ),
					tag.GetInt( "ankh_offset_y" )
				);
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "anima_percent", this.AnimaPercent },
				{ "ankh_offset_x", (int)this.AnkhHUDDisplayOffset.X },
				{ "ankh_offset_y", (int)this.AnkhHUDDisplayOffset.Y }
			};
		}


		////////////////

		public override void OnRespawn( Player player ) {
			var config = NecrotisConfig.Instance;
			float minPerc = config.Get<float>( nameof(config.RespawnMinimumAnima) );

			if( this.AnimaPercent < minPerc ) {
				this.AnimaPercent = minPerc;
			}
		}


		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				if( Main.netMode != NetmodeID.Server ) {
					if( Main.netMode == NetmodeID.MultiplayerClient ) {
						DillutedEctoplasmItem.CanPickupAny( this );
					}
					CustomParticle.UpdateParticles();
				}
			}

			if( !this.player.dead ) {
				this.UpdateAnimaState();
				this.UpdateAnimaBehaviors();
//DebugLibraries.Print( "necrotis", "necrotis%: "+this.AnimaPercent.ToString("N2") );
			}
		}


		////////////////

		public override void UpdateLifeRegen() {
			float necPerc = this.NecrotisPercent;
			if( necPerc > 0f ) {
				NecrotisBehavior.ApplyPlayerLifeRegenBehaviors( this.player, necPerc );
			}
		}


		////////////////

		public override void DrawEffects( PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright ) {
			float necPerc = this.NecrotisPercent;
			if( necPerc > 0f ) {
				NecrotisBehavior.ApplyVisualFX( this.player, ref r, ref g, ref b );
			}
		}
	}
}
