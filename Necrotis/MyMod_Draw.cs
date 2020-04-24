using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Cursor" ) );
			if( idx == -1 ) { return; }

			Texture2D bgTex = this.GetTexture( "UI/AnkhBG" );
			Texture2D fgTex = this.GetTexture( "UI/AnkhFG" );
			var pos = new Vector2( Main.screenWidth - 336, 22 );

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
				return true;
			};
			var interfaceLayer = new LegacyGameInterfaceLayer( "Necrotis: Status Display", draw, InterfaceScaleType.UI );

			//layers.RemoveAt( idx );
			layers.Insert( idx, interfaceLayer );
		}
	}
}