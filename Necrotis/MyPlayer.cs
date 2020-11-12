using System;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Buffs;
using Necrotis.Libraries.Services.FX;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		public float AnimaPercent { get; private set; } = 0.51f;
		public float CurrentAnimaPercentChangeRate { get; private set; } = 0f;


		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey( "anima_percent" ) ) {
				return;
			}

			this.AnimaPercent = tag.GetFloat( "anima_percent" );
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "anima_percent", this.AnimaPercent },
			};
		}


		////////////////

		public override void PreUpdate() {
			CustomParticle.UpdateParticles();

			this.UpdateNecrotisForCurrentContext();

			if( NecrotisDeBuff.CanDeBuff(player, this.AnimaPercent) ) {
				this.player.AddBuff( BuffType<NecrotisDeBuff>(), 2 );
			} else if( EnlivenedBuff.CanBuff(player, this.AnimaPercent) ) {
				this.player.AddBuff( BuffType<EnlivenedBuff>(), 2 );
			}
//DebugHelpers.Print( "necrotis", "necrotis%: "+this.AnimaPercent.ToString("N2") );
		}


		////////////////

		public override void UpdateLifeRegen() {
			if( this.AnimaPercent < 0f ) {
				NecrotisDeBuff.ApplyLifeRegenEffect( this.player, -this.AnimaPercent );
			}
		}


		////////////////

		public override void DrawEffects( PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright ) {
			if( this.AnimaPercent < 0f ) {
				NecrotisDeBuff.ApplyVisualFX( this.player, ref r, ref g, ref b );
			}
		}
	}
}
