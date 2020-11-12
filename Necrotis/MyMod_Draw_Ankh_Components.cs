using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.AnimatedColor;
using Necrotis.Libraries.Services.FX;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		private void DrawHUDAnkhMain( Vector2 pos, Rectangle srcRect, float animaPercent, float animaPercentChangeRate ) {
			SpriteBatch sb = Main.spriteBatch;

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


		private void DrawHUDAnkhFX( Vector2 pos, Rectangle innerSrcRect, float animaPercentChangeRate ) {
			if( animaPercentChangeRate < 0f ) {
//DebugHelpers.Print( "drain", "drain:" + (-animaPercentChangeRate * 1024f) );
				if( Main.rand.NextFloat() < (-animaPercentChangeRate * 1024f) ) {
					int duration = Main.rand.Next( 15, 60 );
					var newPos = pos + new Vector2(
						(float)innerSrcRect.Width * Main.rand.NextFloat(),
						innerSrcRect.Y
					);

					if( !Main.gamePaused ) {
						CustomParticle.Create(
							isInWorld: false,
							pos: newPos,
							tickDuration: duration,
							color: Color.Gold,
							scale: 2f,
							sprayAmt: 1f,
							hasGravity: true
						);
					}
				}
			} else if( animaPercentChangeRate > 0f ) {
//DebugHelpers.Print( "gain", "gain:" + (-animaPercentChangeRate * 1024f) );
				if( Main.rand.NextFloat() < (animaPercentChangeRate * 1024f) ) {
					var newPos = pos;
					newPos.X += (innerSrcRect.Width / 2) - 4;
					newPos.X += Main.rand.Next( 8 );

					if( !Main.gamePaused ) {
						CustomParticle.Create(
							isInWorld: false,
							pos: newPos,
							tickDuration: 30,
							color: Color.Gold,
							scale: 2f,
							sprayAmt: 0f,
							hasGravity: true
						);
					}
				}
			}
		}


		/*private void DrawAnkhHoverTooltip( float animaPercent ) {
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