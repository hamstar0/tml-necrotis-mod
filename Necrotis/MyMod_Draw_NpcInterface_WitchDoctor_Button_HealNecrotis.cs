using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Items;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		private GameInterfaceLayer GetInterfaceLayer_NpcChatButton_WitchDoctor_HealNecrotis() {
			bool addedChatButtonUI() {
				int talkNpcWho = Main.LocalPlayer.talkNPC;
				NPC talkNpc = talkNpcWho == -1 ? null : Main.npc[talkNpcWho];

				if( Main.npcChatText != "" && talkNpc?.active == true && talkNpc.netID == NPCID.WitchDoctor ) {
					this.Draw_NpcChatButton_WitchDoctor_HealNecrotis();
				}
				return true;
			};

			return new LegacyGameInterfaceLayer(
				"Necrotis: Witch Doctor Heal Button",
				addedChatButtonUI,
				InterfaceScaleType.UI
			);
		}


		////////////////

		private void Draw_NpcChatButton_WitchDoctor_HealNecrotis() {
			string label = "Heal Necrotis";

			float healAmount = NecrotisPlayer.CalculateHealAmountFromWitchDoctor( Main.LocalPlayer );
			int cost = NecrotisPlayer.CalculateHealCostFromWitchDoctor( Main.LocalPlayer, healAmount );
			Color color;

			if( cost == 0 ) {
				color = Color.Gray;
			} else {
				label += " ("+ItemMoneyHelpers.RenderMoney(cost)+")";

				color = new Color(
					Main.mouseTextColor,
					(byte)( (double)Main.mouseTextColor / 1.1d ),
					Main.mouseTextColor / 2,
					Main.mouseTextColor
				);
			}

			List<List<TextSnippet>> snippets = Utils.WordwrapStringSmart(
				text: Main.npcChatText,
				c: Color.White,
				font: Main.fontMouseText,
				maxWidth: 460,
				maxLines: 10
			);
			int lineCount = snippets.Count;

			Vector2 scale = new Vector2( 0.9f );

			DynamicSpriteFont font = Main.fontMouseText;
			Vector2 stringSize = ChatManager.GetStringSize( font, label, scale, -1f );

			Vector2 pos = new Vector2(
				stringSize.X + 30f + 96f/*todo?*/,
				100 + ( lineCount + 1 ) * 30
			);
			pos.X += (Main.screenWidth / 2) - (Main.chatBackTexture.Width / 2);

			if( cost > 0) {
				this.Hover_NpcChatButton_WitchDoctor_HealNecrotis( pos, stringSize, ref scale );
			}

			ChatManager.DrawColorCodedStringWithShadow(
				spriteBatch: Main.spriteBatch,
				font: font,
				text: label,
				position: pos + stringSize * 0.5f,
				baseColor: color,
				rotation: 0f,
				origin: stringSize * 0.5f,
				baseScale: scale,
				maxWidth: -1f,
				spread: 2f
			);

			// TODO: Figure out how to use this properly?
			//UILinkPointNavigator.SetPosition( 2502, pos + stringSize * 0.5f );
			//UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight = true;

			if( cost > 0 ) {
				if( Main.npcChatFocus3 && Main.mouseLeft && Main.mouseLeftRelease ) {
					this.Click_NpcChatButton_WitchDoctorHeal_Necrotis( healAmount );
				}
			}
		}


		////

		private void Hover_NpcChatButton_WitchDoctor_HealNecrotis( Vector2 buttonPos, Vector2 stringSize, ref Vector2 scale ) {
			Vector2 mousePos = new Vector2( (float)Main.mouseX, (float)Main.mouseY );
			bool isMouseHovering = mousePos.Between( buttonPos, buttonPos + stringSize * scale );

			if( isMouseHovering && !PlayerInput.IgnoreMouseInterface ) {
				Main.LocalPlayer.mouseInterface = true;
				Main.LocalPlayer.releaseUseItem = false;
				scale *= 1.1f;

				if( !Main.npcChatFocus3 ) {
					Main.PlaySound( SoundID.MenuTick, -1, -1, 1, 1f, 0f );
				}
				Main.npcChatFocus3 = true;
			} else {
				if( Main.npcChatFocus3 ) {
					Main.PlaySound( SoundID.MenuTick, -1, -1, 1, 1f, 0f );
				}
				Main.npcChatFocus3 = false;
			}
		}

		private void Click_NpcChatButton_WitchDoctorHeal_Necrotis( float healAmount ) {
			if( healAmount > 0 ) {
				NecrotisPlayer.HealAtCost( Main.LocalPlayer, healAmount );
			}
		}
	}
}