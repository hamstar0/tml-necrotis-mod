using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Libraries.Services.FX;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		private void DrawHUDAnkhFX( SpriteBatch sb, Vector2 pos, Rectangle innerSrcRect, float animaPercentChangeRate ) {
			if( Main.gamePaused ) {
				return;
			}

			if( animaPercentChangeRate < 0f ) {
				this.DrawHUDAnkhDrainFX( sb,  pos, innerSrcRect, animaPercentChangeRate );
			} else if( animaPercentChangeRate > 0f ) {
				this.DrawHUDAnkhGainFX( sb, pos, innerSrcRect, animaPercentChangeRate );
			}
		}

		
		private void DrawHUDAnkhDrainFX( SpriteBatch sb, Vector2 pos, Rectangle innerSrcRect, float animaPercentChangeRate ) {
//DebugHelpers.Print( "drain", "drain:" + (-animaPercentChangeRate * 1024f) );
			if( Main.rand.NextFloat() >= (-animaPercentChangeRate * 1024f) ) {
				return;
			}

			int duration = Main.rand.Next( 15, 60 );
			var newPos = pos + new Vector2(
				(float)innerSrcRect.Width * Main.rand.NextFloat(),
				innerSrcRect.Y
			);

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

		private void DrawHUDAnkhGainFX( SpriteBatch sb, Vector2 pos, Rectangle innerSrcRect, float animaPercentChangeRate ) {
			var srcPos = pos;
			srcPos.X += innerSrcRect.Width / 2;
			var origin = new Vector2( this.AnkhDripSource.Width / 2, this.AnkhDripSource.Height / 2 );

			float flicker = 1f - (float)Math.Pow( Main.rand.NextFloat(), animaPercentChangeRate * 8192 );

			sb.Draw(
				texture: this.AnkhDripSource,
				position: srcPos,
				sourceRectangle: null,
				color: Color.White,
				rotation: 0f,
				origin: origin,
				scale: 0.5f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
			sb.Draw(
				texture: this.AnkhDripSource,
				position: srcPos,
				sourceRectangle: null,
				color: Color.White * (0.25f + (0.5f * flicker)),
				rotation: 0f,
				origin: origin,
				scale: 1f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
			sb.Draw(
				texture: this.AnkhDripSource,
				position: srcPos,
				sourceRectangle: null,
				color: Color.White * flicker,
				rotation: 0f,
				origin: origin,
				scale: 2f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

//DebugHelpers.Print( "gain", "gain:" + (-animaPercentChangeRate * 1024f) );
			if( Main.rand.NextFloat() >= (animaPercentChangeRate * 1024f) ) {
				return;
			}

			var dripPos = srcPos;
			dripPos.X += Main.rand.Next( 8 ) - 4;

			CustomParticle.Create(
				isInWorld: false,
				pos: dripPos,
				tickDuration: 60 * 4,
				color: Color.Gold,
				scale: 2f,
				sprayAmt: 0f,
				hasGravity: true
			);
		}


		////////////////

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