using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Necrotis.UI {
	partial class AnkhHUD {
		private Vector2? BaseDragOffset = null;
		private Vector2 PreviousDragMousePos = default;



		////////////////

		private bool RunHUDEditor( out bool isHovering ) {
			Vector2 pos = AnkhHUD.GetHUDPosition();
			var area = new Rectangle(
				(int)pos.X,
				(int)pos.Y,
				this.AnkhBgTex.Width,
				this.AnkhBgTex.Height
			);

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );

			if( Main.mouseLeft ) {
				if( this.BaseDragOffset.HasValue || isHovering ) {
					this.RunHUDEditor_Drag( pos );
				}
			} else {
				this.BaseDragOffset = null;
			}

			return this.BaseDragOffset.HasValue;
		}


		private void RunHUDEditor_Drag( Vector2 basePos ) {
			if( !this.BaseDragOffset.HasValue ) {
				this.BaseDragOffset = basePos - Main.MouseScreen;
				this.PreviousDragMousePos = Main.MouseScreen;

				return;
			}
			
			Vector2 movedSince = Main.MouseScreen - this.PreviousDragMousePos;
			this.PreviousDragMousePos = Main.MouseScreen;

			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			myplayer.AnkhHUDDisplayOffset += movedSince;
		}
	}
}
