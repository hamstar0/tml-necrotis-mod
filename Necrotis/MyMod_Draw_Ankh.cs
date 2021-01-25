using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Libraries.Services.FX;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		private bool WasInventory = false;



		////////////////

		private void DrawHUDAnkh( SpriteBatch sb, float animaPercent, float animaChangeRate ) {
			var config = NecrotisConfig.Instance;
			Vector2 pos;

			if( Main.playerInventory ) {
				pos = new Vector2(
					config.Get<int>( nameof( config.AnkhInvScreenPositionX ) ),
					config.Get<int>( nameof( config.AnkhInvScreenPositionY ) )
				);
			} else {
				pos = new Vector2(
					config.Get<int>( nameof( config.AnkhScreenPositionX ) ),
					config.Get<int>( nameof( config.AnkhScreenPositionY ) )
				);
			}

			if( this.WasInventory != Main.playerInventory ) {
				this.WasInventory = Main.playerInventory;
				CustomParticle.ClearAll();
			}

			if( pos.X < 0 ) {
				pos.X = Main.screenWidth + pos.X;
			}
			if( pos.Y < 0 ) {
				pos.Y = Main.screenHeight + pos.Y;
			}

			int necScroll = (int)( animaPercent * (float)this.AnkhFgTex.Height );
			var statSrcRect = new Rectangle(
				x: 0,
				y: this.AnkhFgTex.Height - necScroll,
				width: this.AnkhFgTex.Width,
				height: necScroll
			);

			this.DrawHUDAnkhMain( sb, pos, statSrcRect, animaPercent, animaChangeRate );
			this.DrawHUDAnkhFX( sb, pos, statSrcRect, animaChangeRate );
		}

		/*private void DrawAnkhHoverTooltipLayer() {
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