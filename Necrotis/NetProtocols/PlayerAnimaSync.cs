using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Services.Network.SimplePacket;


namespace Necrotis.Net {
	[Serializable]
	class PlayerAnimaSyncProtocol : SimplePacketPayload {
		public static void Broadcast( NecrotisPlayer myplayer ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) { throw new ModLibsException( "Not a client." ); }

			var payload = new PlayerAnimaSyncProtocol( myplayer.player, myplayer.AnimaPercent );

			SimplePacket.SendToServer( payload );
		}

		public static void SendToAllClients( NecrotisPlayer myplayer ) {
			if( Main.netMode != NetmodeID.Server ) { throw new ModLibsException( "Not a server." ); }

			var protocol = new PlayerAnimaSyncProtocol( myplayer.player, myplayer.AnimaPercent );

			SimplePacket.SendToClient( protocol, -1, myplayer.player.whoAmI );
		}



		////////////////

		public int PlayerWho;
		public float AnimaPercent;



		////////////////

		private PlayerAnimaSyncProtocol() { }

		private PlayerAnimaSyncProtocol( Player player, float animaPercent ) {
			this.PlayerWho = player.whoAmI;
			this.AnimaPercent = animaPercent;
		}

		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			Player plr = Main.player[this.PlayerWho];
			var otherplr = plr.GetModPlayer<NecrotisPlayer>();

			otherplr.SyncAnima( this.AnimaPercent );
		}

		public override void ReceiveOnClient() {
			Player plr = Main.player[this.PlayerWho];
			var otherplr = plr.GetModPlayer<NecrotisPlayer>();

			otherplr.SyncAnima( this.AnimaPercent );
		}
	}
}
