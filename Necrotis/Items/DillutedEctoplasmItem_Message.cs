using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using ModLibsGeneral.Libraries.Players;
using Messages;


namespace Necrotis.Items {
	public partial class DillutedEctoplasmItem : ModItem {
		private static string MessageId = "Necrotis_DillutedEctoplasm";
		private static string MessageTitle = "Dilluted Ectoplasm";
		private static string MessageBody =
			"Scattered around and within the island of Terraria are innumerable burial urns"
			+ " found simply lying about: Unguarded, yet undisturbed. An odd thing, seeing as their"
			+ " contents are often treasures for the taking. More notable than treasure, however,"
			+ " is the presence of corporeal spiritual essence in a form known as ectoplasm. Though"
			+ " dilluted due to age and material contamination, the substance itself is stable, but"
			+ " often typically difficult to obtain due to circumstances."
			+ "\n \nIt is theorized that someone with the means (or maybe a very particular nature) could"
			+ " harness ectoplasm for their own use. Ancient records suggest the roots of magic itself"
			+ " exist within the nature of this spiritual energy, but all knowledge of how to tap into"
			+ " this seems to have been lost to time..."
			+ "\n \nWhat is known about ectoplasm, however, is that its raw form has been reported"
			+ " to alleviate symptoms of the necrotis plague by way of simple contact. Ethics aside,"
			+ " if only the island wasn't so dangerous, we could at least begin using it for research"
			+ " into a possible cure for the plague...";

		

		////////////////
		
		private static void InitializeScannableMessage_WeakRef() {
			int timer = 0;
			bool prevFound = false;
			int ectoType = ModContent.ItemType<DillutedEctoplasmItem>();

			Rectangle getItemRect( Item item ) {
				return new Rectangle(
					(int)item.position.X - 8,
					(int)item.position.Y - 8,
					item.width + 16,
					item.height + 16
				);
			}

			PKEMeter.PKEMeterAPI.SetScannable(
				name: "DillutedEctoplasm",
				scannable: new PKEMeter.Logic.PKEScannable(
					canScan: (scrX, scrY) => {
						if( timer-- > 0 ) {
							return prevFound;
						}
						timer = 5;

						prevFound = Main.item.Any(
							i => i?.active == true
								&& i.type == ectoType
								&& getItemRect(i).Contains( (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y )
						);
						return prevFound;
					},
					onScanCompleteAction: DillutedEctoplasmItem.ProduceMessage
				),
				allowRepeat: false,
				runIfComplete: true
			);
		}


		////////////////

		private static void ProduceMessage() {
			MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
				MessagesAPI.AddMessage(
					title: DillutedEctoplasmItem.MessageTitle,
					description: DillutedEctoplasmItem.MessageBody,
					modOfOrigin: NecrotisMod.Instance,
					alertPlayer: MessagesAPI.IsUnread( DillutedEctoplasmItem.MessageId ),
					isImportant: false,
					parentMessage: MessagesAPI.StoryLoreCategoryMsg,
					id: DillutedEctoplasmItem.MessageId
				);
			} );
		}
	}
}