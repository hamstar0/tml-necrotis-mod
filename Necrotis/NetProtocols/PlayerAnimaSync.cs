using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Network.SimplePacket;


namespace Necrotis.Net {
	class PlayerAnimaSyncProtocol : SimplePacketPayload {
		public static void BroadcastFromClientToAll( NecrotisPlayer myplayer, string context ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) { throw new ModLibsException( "Not a client." ); }

			var payload = new PlayerAnimaSyncProtocol( myplayer.player, myplayer.AnimaPercent, context );

			SimplePacket.SendToServer( payload );
		}

		public static void SendToAllClients( NecrotisPlayer myplayer, string context ) {
			if( Main.netMode != NetmodeID.Server ) { throw new ModLibsException( "Not a server." ); }

			var protocol = new PlayerAnimaSyncProtocol( myplayer.player, myplayer.AnimaPercent, context );

			SimplePacket.SendToClient( protocol, -1, myplayer.player.whoAmI );
		}



		////////////////

		public int PlayerWho;
		public float AnimaPercent;

		public string Context;



		////////////////

		private PlayerAnimaSyncProtocol() { }

		private PlayerAnimaSyncProtocol( Player player, float animaPercent, string context ) {
			this.PlayerWho = player.whoAmI;
			this.AnimaPercent = animaPercent;
			this.Context = context;
		}

		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			Player plr = Main.player[this.PlayerWho];
			var otherplr = plr.GetModPlayer<NecrotisPlayer>();

			otherplr.SyncAnima( this.AnimaPercent, this.Context );
		}

		public override void ReceiveOnClient() {
			Player plr = Main.player[this.PlayerWho];
			var otherplr = plr.GetModPlayer<NecrotisPlayer>();

			otherplr.SyncAnima( this.AnimaPercent, this.Context );
		}
	}
}
