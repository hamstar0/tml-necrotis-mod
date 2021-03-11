using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Libraries.Services.FX;


namespace Necrotis.UI {
	partial class AnkhHUD {
		private bool WasInventory = false;



		////////////////

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			var plr = Main.LocalPlayer;
			var myplayer = plr.GetModPlayer<NecrotisPlayer>();
			this.DrawAnkh( sb, myplayer.AnimaPercent, myplayer.CurrentAnimaPercentChangeRate );
		}


		private void DrawAnkh( SpriteBatch sb, float animaPercent, float animaChangeRate ) {
			Vector2 pos = this.GetHudComputedPosition( true );

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

			this.DrawAnkhMain( sb, pos, statSrcRect, animaPercent, animaChangeRate );
			this.DrawAnkhFx( sb, pos, statSrcRect, animaChangeRate );
		}

		/*private void DrawHoverTooltipLayer() {
			var config = NecrotisConfig.Instance;
			Texture2D bgTex = this.GetTexture( "UI/AnkhBG" );

			var pos = new Vector2(
				config.Get<int>( nameof( NecrotisConfig.AnkhScreenPositionX ) ),
				config.Get<int>( nameof( NecrotisConfig.AnkhScreenPositionY ) )
			);

			var area = new Rectangle( (int)pos.X, (int)pos.Y, bgTex.Width, bgTex.Height );
			if( !area.Contains( Main.mouseX, Main.mouseY ) ) {
				return;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			float animaPercent = myplayer.CurrentAnimaPercentChangeRate;

			this.DrawAnkhHoverTooltip( animaPercent );
		}*/
	}
}