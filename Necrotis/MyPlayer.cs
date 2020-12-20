using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Libraries.Services.FX;
using Necrotis.NecrotisBehaviors;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		public float AnimaPercent { get; private set; } = 0.51f;
		public float CurrentAnimaPercentChangeRate { get; private set; } = 0f;


		////

		public float NecrotisPercent => Math.Max( 1f - (this.AnimaPercent * 2f), 0f );


		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Initialize() {
			this.AnimaPercent = 0f;
			this.CurrentAnimaPercentChangeRate = 0f;
		}

		////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey( "anima_percent" ) ) {
				return;
			}

			this.AnimaPercent = MathHelper.Clamp( tag.GetFloat("anima_percent"), 0f, 1f );
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "anima_percent", this.AnimaPercent },
			};
		}


		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				CustomParticle.UpdateParticles();
			}

			if( !this.player.dead ) {
				this.UpdateAnimaStateForCurrentContext();
				this.UpdateAnimaBehaviors();
//DebugHelpers.Print( "necrotis", "necrotis%: "+this.AnimaPercent.ToString("N2") );
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
