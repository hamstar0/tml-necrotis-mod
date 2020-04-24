using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( Main.playerInventory ) {
				return;
			}

			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Resource Bars" ) );
			if( idx == -1 ) {
				return;
			}

			Texture2D bgTex = this.GetTexture( "UI/AnkhBG" );
			Texture2D fgTex = this.GetTexture( "UI/AnkhFG" );
			var pos = new Vector2(
				NecrotisConfig.Instance.AnkhScreenPositionX,
				NecrotisConfig.Instance.AnkhScreenPositionY
			);
			if( pos.X < 0 ) {
				pos.X = Main.screenWidth - pos.X;
			}
			if( pos.Y < 0 ) {
				pos.Y = Main.screenHeight - pos.Y;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			int necScroll = (int)((float)myplayer.NecrotisResistPercent * (float)fgTex.Height);
			var statSrcRect = new Rectangle(
				x: 0,
				y: fgTex.Height - necScroll,
				width: fgTex.Width,
				height: necScroll
			);

			GameInterfaceDrawMethod draw = () => {
				Main.spriteBatch.Draw(
					texture: bgTex,
					position: pos,
					sourceRectangle: null,
					color: Color.White
				);
				if( statSrcRect.Height > 0 ) {
					Main.spriteBatch.Draw(
						texture: fgTex,
						position: pos + new Vector2(0, fgTex.Height - necScroll),
						sourceRectangle: statSrcRect,
						color: Color.White
					);
				}

				var area = new Rectangle( (int)pos.X, (int)pos.Y, bgTex.Width, bgTex.Height );
				if( area.Contains(Main.mouseX, Main.mouseY) ) {
					Main.spriteBatch.DrawString(
						spriteFont: Main.fontMouseText,
						text: (myplayer.NecrotisResistPercent*100f).ToString("N0")+"% Necrotis Resist",
						position: Main.MouseScreen + new Vector2(0f, 24f),
						color: myplayer.NecrotisResistPercent > 0f
							? Color.White
							: Color.Red
					);
				}

				return true;
			};
			var interfaceLayer = new LegacyGameInterfaceLayer( "Necrotis: Status Display", draw, InterfaceScaleType.UI );

			//layers.RemoveAt( idx );
			layers.Insert( idx, interfaceLayer );
		}
	}
}