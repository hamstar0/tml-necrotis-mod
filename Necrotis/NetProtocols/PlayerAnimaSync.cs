using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;


namespace Necrotis.Net {
	[Serializable]
	class PlayerAnimaSyncProtocol : NetIOBroadcastPayload {
		public static void Broadcast( NecrotisPlayer myplayer ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) { throw new ModHelpersException( "Not a client." ); }

			var protocol = new PlayerAnimaSyncProtocol( myplayer.player, myplayer.AnimaPercent );

			NetIO.Broadcast( protocol );
		}

		public static void SendToAllClients( NecrotisPlayer myplayer ) {
			if( Main.netMode != NetmodeID.Server ) { throw new ModHelpersException( "Not a server." ); }

			var protocol = new PlayerAnimaSyncProtocol( myplayer.player, myplayer.AnimaPercent );

			NetIO.SendToClients( protocol, myplayer.player.whoAmI );
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

		public override bool ReceiveOnServerBeforeRebroadcast( int fromWho ) {
			Player plr = Main.player[this.PlayerWho];
			var otherplr = plr.GetModPlayer<NecrotisPlayer>();

			otherplr.SyncAnima( this.AnimaPercent );

			return true;
		}

		public override void ReceiveBroadcastOnClient() {
			Player plr = Main.player[this.PlayerWho];
			var otherplr = plr.GetModPlayer<NecrotisPlayer>();

			otherplr.SyncAnima( this.AnimaPercent );
		}
	}
}
