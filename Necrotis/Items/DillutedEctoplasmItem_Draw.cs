using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.UI;


namespace Necrotis.Items {
	public partial class DillutedEctoplasmItem : ModItem {
		 private bool _HasBegunHighlightingEcto_Singleton = false;
		 private bool _HasFinishedHighlightedEcto_Singleton = false;

		public override void PostDrawInWorld(
					SpriteBatch spriteBatch,
					Color lightColor,
					Color alphaColor,
					float rotation,
					float scale,
					int whoAmI ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			var myitemSingleton = ModContent.GetInstance<DillutedEctoplasmItem>();

			if( !myitemSingleton._HasFinishedHighlightedEcto_Singleton ) {
				if( myplayer.AnimaPercent < 0.35f ) {
					myitemSingleton._HasBegunHighlightingEcto_Singleton = true;

					this.DrawHighlightIcon( spriteBatch );
				} else if( myplayer.AnimaPercent > 0.36f ) {	//?
					if( myitemSingleton._HasBegunHighlightingEcto_Singleton ) {
						myitemSingleton._HasFinishedHighlightedEcto_Singleton = true;
					}
				}
			}
		}


		////////////////

		public void DrawHighlightIcon( SpriteBatch sb ) {
			float pulse = (float)Main.mouseTextColor / 255f;

			Vector2 pos = this.item.Center;
			pos.Y = this.item.position.Y;
			pos.Y -= 32f - (pulse * 16f);
			pos = UIZoomLibraries.ConvertToScreenPosition( pos, null, null );

			string text = "V";

			Vector2 textDim = Main.fontMouseText.MeasureString( text );

			//

			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: text,
				position: pos,
				color: Color.Lime * 0.65f,
				rotation: 0f,
				origin: textDim * 0.5f,
				scale: 1f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
		}
	}
}