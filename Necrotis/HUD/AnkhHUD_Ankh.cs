using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Necrotis.Libraries.Services.FX;


namespace Necrotis.HUD {
	partial class AnkhHUD {
		public static float GetAnkhShield_SoulBarriers_WeakRef( Player player ) {
			SoulBarriers.Barriers.BarrierTypes.Barrier barrier
					= SoulBarriers.SoulBarriersAPI.GetPlayerBarrier( player );
			if( barrier == null ) {
				return 0f;
			}

			return (float)barrier.GetStrengthPercent( player.statManaMax2 );
		}



		////////////////

		private bool WasInventory = false;



		////////////////
		
		protected override void DrawSelf( SpriteBatch sb ) {
			base.DrawSelf( sb );

			Player player = Main.LocalPlayer;
			var myplayer = player.GetModPlayer<NecrotisPlayer>();

			//

			float shieldPercent = 0;
			if( NecrotisMod.Instance.SoulBarriersMod != null ) {
				shieldPercent = AnkhHUD.GetAnkhShield_SoulBarriers_WeakRef( player );
			}

			//

			this.DrawAnkh(
				sb: sb,
				animaPercent: myplayer.AnimaPercent,
				animaChangeRate: myplayer.CurrentAnimaPercentChangeRate,
				shieldPrecent: shieldPercent
			);
		}


		private void DrawAnkh( SpriteBatch sb, float animaPercent, float animaChangeRate, float shieldPrecent ) {
			Vector2 pos = this.GetHUDComputedPosition( true );

			int necScroll = (int)( animaPercent * (float)this.AnkhFgTex.Height );
			var statSrcRect = new Rectangle(
				x: 0,
				y: this.AnkhFgTex.Height - necScroll,
				width: this.AnkhFgTex.Width,
				height: necScroll
			);

			if( this.WasInventory != Main.playerInventory ) {
				this.WasInventory = Main.playerInventory;
				CustomParticle.ClearAll();
			}

			this.DrawAnkhMain( sb, pos, statSrcRect, animaPercent, animaChangeRate, shieldPrecent );
			this.DrawAnkhFx( sb, pos, statSrcRect, animaChangeRate );
		}
	}
}