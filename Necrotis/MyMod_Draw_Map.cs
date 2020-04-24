using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.HUD;
using Necrotis.Items;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		public override void PostDrawFullscreenMap( ref string mouseText ) {
			Texture2D tex = ModContent.GetTexture( "Necrotis/Items/DillutedEctoplasmItem" );
			Vector2 origin = new Vector2( tex.Width/2, tex.Height/2 );
			int ectoType = ModContent.ItemType<DillutedEctoplasmItem>();
			int len = Main.item.Length;

			for( int i=0; i<len; i++ ) {
				Item item = Main.item[i];
				if( item?.active != true || item.type != ectoType ) {
					continue;
				}

				var mapPos = HUDMapHelpers.GetFullMapPositionAsScreenPosition( item.position );
				if( !mapPos.IsOnScreen ) {
					continue;
				}

				Main.spriteBatch.Draw(
					texture: tex,
					position: mapPos.ScreenPosition,
					sourceRectangle: null,
					color: Color.White,
					rotation: 0f,
					origin: origin,
					scale: 0.25f,
					effects: SpriteEffects.None,
					layerDepth: 1f
				);
			}
		}
	}
}