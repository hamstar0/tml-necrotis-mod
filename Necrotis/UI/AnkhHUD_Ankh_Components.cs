using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.AnimatedColor;


namespace Necrotis.UI {
	partial class AnkhHUD {
		private void DrawMain(
					SpriteBatch sb,
					Vector2 pos,
					Rectangle srcRect,
					float animaPercent,
					float animaPercentChangeRate ) {
			if( animaPercent >= 0.9f ) {
				var config = NecrotisConfig.Instance;
				float minBuffPerc = config.Get<float>( nameof(config.EnlivenedAnimaPercentMinimum) );

				if( animaPercent >= minBuffPerc ) {
					sb.Draw(
						texture: this.AnkhGlowTex,
						position: pos + new Vector2( -5f, -5f ),
						sourceRectangle: null,
						color: Color.White
					);
				}
			} else if( animaPercent < 0.5f ) {
				sb.Draw(
					texture: this.AnkhUnglowTex,
					position: pos + new Vector2( -5f, -5f ),
					sourceRectangle: null,
					color: Color.White
				);
			}

			sb.Draw(
				texture: this.AnkhBgTex,
				position: pos,
				sourceRectangle: null,
				color: Color.White
			);

			if( srcRect.Height > 0 ) {
				sb.Draw(
					texture: this.AnkhFgTex,
					position: pos + new Vector2( 0, srcRect.Y ),
					sourceRectangle: srcRect,
					color: Color.White
				);
			}

			if( animaPercent == 0f ) {
				float glow = (float)AnimatedColors.Strobe.CurrentColor.R / 255f;

				sb.Draw(
					texture: this.AnkhOhmTex,
					position: pos,
					sourceRectangle: null,
					color: Color.White * glow
				);
			}

			var area = new Rectangle( (int)pos.X, (int)pos.Y, this.AnkhBgTex.Width, this.AnkhBgTex.Height );
			if( area.Contains( Main.mouseX, Main.mouseY ) ) {
				float percent = animaPercent * 100f;
				if( percent < 0f ) { percent = 0f; }

				Utils.DrawBorderStringFourWay(
					sb: sb,
					font: Main.fontMouseText,
					text: percent.ToString("N0") + "% Anima (Necrotis Resist %)",
					x: Main.MouseScreen.X,
					y: Main.MouseScreen.Y + 24f,
					textColor: animaPercent > 0.5f
						? new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor )
						: new Color( Main.mouseTextColor, 0, 0 ),
					borderColor: Color.Black,
					origin: default(Vector2)
				);
			}
		}


		////////////////

		/*private void DrawHoverTooltip( float animaPercent ) {
			float percent = animaPercent * 100f;
			if( percent < 0f ) { percent = 0f; }

			Main.spriteBatch.DrawString(
				spriteFont: Main.fontMouseText,
				text: percent.ToString( "N0" ) + "% Anima (Necrotis Resist %)",
				position: Main.MouseScreen + new Vector2( 0f, 24f ),
				color: animaPercent > 0f
					? Color.White
					: Color.Red
			);
		}*/
	}
}