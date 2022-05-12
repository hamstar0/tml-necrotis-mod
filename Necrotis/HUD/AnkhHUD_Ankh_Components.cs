using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Services.AnimatedColor;


namespace Necrotis.HUD {
	partial class AnkhHUD {
		public static string GetGeneralHoverText(
					float animaPercent,
					float animaPercentChangeRate,
					float shieldPercent ) {
			int percent = (int)( animaPercent * 100f );
			if( percent < 0 ) { percent = 0; }

			string text = percent + "% Anima (Necrotis Resist %)";

			if( animaPercentChangeRate != 0f ) {
				int percentChangePerSec = (int)(animaPercentChangeRate * 100f * 60f * 60f);
				text += "\n";
				if( percentChangePerSec >= 0 ) {
					text += "+";
				}
				text += percentChangePerSec + "% per minute";
			}

			if( shieldPercent > 0f ) {
				string shield = ((int)(shieldPercent * 100f)).ToString();
				text += "\n"+shield+"% Barrier";
			}

			return text;
		}



		////////////////

		private void DrawAnkhMain(
					SpriteBatch sb,
					Vector2 pos,
					Rectangle srcRect,
					float animaPercent,
					float animaPercentChangeRate,
					float shieldPercent ) {
			float tint = Main.playerInventory ? 0.5f : 1f;

			//

			if( animaPercent >= 0.9f ) {
				var config = NecrotisConfig.Instance;
				float minBuffPerc = config.Get<float>( nameof(config.EnlivenedAnimaPercentMinimum) );

				if( animaPercent >= minBuffPerc ) {
					sb.Draw(
						texture: this.AnkhGlowTex,
						position: pos + new Vector2( -5f, -5f ),
						sourceRectangle: null,
						color: Color.White * tint
					);
				}
			} else if( animaPercent < 0.5f && !Main.playerInventory ) {
				sb.Draw(
					texture: this.AnkhUnglowTex,
					position: pos + new Vector2( -5f, -5f ),
					sourceRectangle: null,
					color: Color.White * tint
				);
			}

			//
			
			if( shieldPercent > 0f ) {
				float flicker = Main.rand.NextFloat();
				flicker = flicker * flicker * flicker * flicker;
				flicker = shieldPercent + ((1f - shieldPercent) * flicker);

				sb.Draw(
					texture: this.AnkhShieldTex,
					position: pos + new Vector2(-4f, -4f),
					sourceRectangle: null,
					color: Color.White * tint * flicker
				);
			}

			//

			sb.Draw(
				texture: this.AnkhBgTex,
				position: pos,
				sourceRectangle: null,
				color: Color.White * tint
			);

			if( srcRect.Height > 0 ) {
				sb.Draw(
					texture: this.AnkhFgTex,
					position: pos + new Vector2( 0, srcRect.Y ),
					sourceRectangle: srcRect,
					color: Color.White * tint
				);
			}

			//

			if( animaPercent == 0f ) {
				float ohmGlow = (float)AnimatedColors.Strobe.CurrentColor.R / 255f;

				sb.Draw(
					texture: this.AnkhOhmTex,
					position: pos,
					sourceRectangle: null,
					color: Color.White * ohmGlow * tint
				);
			}

			//

			var area = new Rectangle( (int)pos.X, (int)pos.Y, this.AnkhBgTex.Width, this.AnkhBgTex.Height );
			if( area.Contains( Main.mouseX, Main.mouseY ) ) {
				this.DrawHoverTooltipIf( sb, animaPercent, animaPercentChangeRate, shieldPercent );
			}
		}


		////////////////

		private void DrawHoverTooltipIf(
					SpriteBatch sb,
					float animaPercent,
					float animaPercentChangeRate,
					float shieldPercent ) {
			if( this.IsShowingDefaultHoverText ) {
				return;
			}

			string text = AnkhHUD.GetGeneralHoverText( animaPercent, animaPercentChangeRate, shieldPercent );

			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: text,
				x: Main.MouseScreen.X,
				y: Main.MouseScreen.Y + 24f,
				textColor: animaPercent > 0.5f
					? new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor )
					: new Color( Main.mouseTextColor, 0, 0 ),
				borderColor: Color.Black,
				origin: default( Vector2 )
			);
		}
	}
}