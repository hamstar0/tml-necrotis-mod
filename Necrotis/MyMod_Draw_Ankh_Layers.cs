using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		private void DrawAnkhLayer() {
			var config = NecrotisConfig.Instance;
			Texture2D bgTex = this.GetTexture( "UI/AnkhBG" );
			Texture2D fgTex = this.GetTexture( "UI/AnkhFG" );

			var pos = new Vector2(
				config.Get<int>( nameof( NecrotisConfig.AnkhScreenPositionX ) ),
				config.Get<int>( nameof( NecrotisConfig.AnkhScreenPositionY ) )
			);
			if( pos.X < 0 ) {
				pos.X = Main.screenWidth + pos.X;
			}
			if( pos.Y < 0 ) {
				pos.Y = Main.screenHeight + pos.Y;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			int necScroll = (int)( (float)myplayer.NecrotisResistPercent * (float)fgTex.Height );
			var statSrcRect = new Rectangle(
				x: 0,
				y: fgTex.Height - necScroll,
				width: fgTex.Width,
				height: necScroll
			);

			this.DrawUIAnkhChangeFX( pos, statSrcRect, myplayer.CurrentNecrotisResistPercentChangeRate );
			this.DrawUIAnkh( bgTex, fgTex, pos, statSrcRect, myplayer.NecrotisResistPercent );
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
			float necrotisResistPercent = myplayer.CurrentNecrotisResistPercentChangeRate;

			this.DrawAnkhHoverTooltip( necrotisResistPercent );
		}*/
	}
}