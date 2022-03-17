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
		private static void CanPickupAlerts( Player player, bool isEmptyHanded, bool isHoldingJar ) {
			if( !isEmptyHanded && !isHoldingJar && player.whoAmI == Main.myPlayer ) {
				if( Timers.GetTimerTickDuration("NecrotisPickupAlert") <= 0 ) {
					Main.NewText( "Only bare hands or (empty) canopic jars can interact with this.", Color.Yellow );
				}
				Timers.SetTimer( "NecrotisPickupAlert", 60, false, () => false );
			}

			//

			if( ModLoader.GetMod( "PKEMeter" ) == null ) {
				DillutedEctoplasmItem.ProduceMessage();
			}
		}



		////////////////

		public const int DefaultGrabRange = 24;



		////////////////
		
		internal static bool CanPickupAny( NecrotisPlayer myplayer ) {
			int ectoType = ModContent.ItemType<DillutedEctoplasmItem>();
			
			for( int i=0; i<Main.item.Length; i++ ) {
				Item item = Main.item[i];
				if( item?.IsAir != false ) { continue; }
				if( item.type != ectoType ) { continue; }

				var mymoditem = item.modItem as DillutedEctoplasmItem;
				if( mymoditem.CanPickup(myplayer.player) ) {
					return true;
				}
			}

			return false;
		}



		////////////////

		public override bool ItemSpace( Player player ) {
			return true;
		}

		////////////////
		
		public override bool CanPickup( Player player ) {
//return !player.HasBuff( BuffID.PotionSickness );
			//var myplayer = player.GetModPlayer<NecrotisPlayer>();
			//bool hasRoom = myplayer.AnimaPercent < 1f;
			bool isEmptyHanded = player.HeldItem?.IsAir ?? true;
			bool isHoldingJar = player.HeldItem?.type == ModContent.ItemType<EmptyCanopicJarItem>();

			float distSqr = (this.item.Center - player.MountedCenter).LengthSquared();
			bool isWithinRange = distSqr < (DillutedEctoplasmItem.DefaultGrabRange * DillutedEctoplasmItem.DefaultGrabRange);

//DebugLibraries.Print(
//	"pickup_"+this.item.whoAmI,
//	"isEmptyHanded:"+isEmptyHanded+", isHoldingJar:"+isHoldingJar+", distSqr:"+(int)distSqr
//);
			if( isWithinRange ) {   // 24 units from player center
				if( Main.netMode != NetmodeID.Server ) {
					DillutedEctoplasmItem.CanPickupAlerts( player, isEmptyHanded, isHoldingJar );
				}
			}
			
/*if( (isEmptyHanded || isHoldingJar) && isWithinRange ) {
LogLibraries.LogOnce( ""//(((isEmptyHanded || isHoldingJar) && isWithinRange) ? "TRUE" : "FALSE")
//	+" - isEmptyHanded:"+isEmptyHanded+", isHoldingJar:"+isHoldingJar+", isWithinRange:"+isWithinRange
	+" - rects:"+player.getRect().Intersects(this.item.getRect())
	+" - inv/anim:"+(player.inventory[player.selectedItem].type != 0)+","+(player.itemAnimation <= 0)
//	+" - plr:"+(player.whoAmI == Main.myPlayer)+" ("+player.whoAmI+", "+Main.myPlayer+")"
	+" - owner:"+this.item.owner
);
}*/
			return (isEmptyHanded || isHoldingJar) && isWithinRange;
		}

		public override bool OnPickup( Player player ) {
			//string timerName = "NecrotisPickupRepeatStopper_" + player.whoAmI + "_" + this.item.whoAmI;
			//bool hasCooldown = Timers.GetTimerTickDuration( timerName ) > 0;

			//Timers.SetTimer( timerName, 30, false, () => false );

			//if( !hasCooldown ) {
			// If picked up into jar, don't apply dose
			if( !this.PickupIntoJarIf( player, out bool isError ) && !isError ) {
				DillutedEctoplasmItem.ApplyEctoplasmDose( player );
			}

			return false;
		}


		////////////////

		public bool PickupIntoJarIf( Player player, out bool isError ) {
			if( player.HeldItem?.IsAir != false ) {
				isError = false;
				return false;
			}

			int emptyJarType = ModContent.ItemType<EmptyCanopicJarItem>();
			if( player.HeldItem?.type != emptyJarType ) {
				isError = false;
				return false;
			}

			if( PlayerItemLibraries.RemoveInventoryItemQuantity(player, emptyJarType, 1) == 0 ) {
				Main.NewText( "Could not fill jar.", Color.Yellow );
				isError = true;
				return false;
			}

			int itemWho = Item.NewItem(
				position: player.position,
				Type: ModContent.ItemType<FilledCanopicJarItem>(),
				Stack: 1,
				noBroadcast: false,
				prefixGiven: 0,
				noGrabDelay: true
			);
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho, 1f, 0f, 0f, 0, 0, 0 );
			}

			Main.PlaySound( SoundID.Drip, this.item.Center, 2 );

			isError = false;
			return true;
		}
	}
}