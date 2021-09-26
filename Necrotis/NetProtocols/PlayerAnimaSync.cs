using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Network.SimplePacket;
using Necrotis.NecrotisBehaviors;


namespace Necrotis.Net {
	class PlayerAnimaSyncProtocol : SimplePacketPayload {
		public static void BroadcastFromClientToAll( NecrotisPlayer myplayer, AnimaSource source ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) { throw new ModLibsException( "Not a client." ); }

			var payload = new PlayerAnimaSyncProtocol( myplayer.player, myplayer.AnimaPercent, source );

			SimplePacket.SendToServer( payload );
		}

		public static void SendToAllClients( NecrotisPlayer myplayer, AnimaSource source ) {
			if( Main.netMode != NetmodeID.Server ) { throw new ModLibsException( "Not a server." ); }

			var protocol = new PlayerAnimaSyncProtocol( myplayer.player, myplayer.AnimaPercent, source );

			SimplePacket.SendToClient( protocol, -1, myplayer.player.whoAmI );
		}



		////////////////

		public int PlayerWho;
		public float AnimaPercent;

		public byte Source;



		////////////////

		private PlayerAnimaSyncProtocol() { }

		private PlayerAnimaSyncProtocol( Player player, float animaPercent, AnimaSource source ) {
			this.PlayerWho = player.whoAmI;
			this.AnimaPercent = animaPercent;
			this.Source = (byte)source;
		}

		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			Player plr = Main.player[this.PlayerWho];
			var otherplr = plr.GetModPlayer<NecrotisPlayer>();

			otherplr.SyncAnima( this.AnimaPercent, (AnimaSource)this.Source );
		}

		public override void ReceiveOnClient() {
			Player plr = Main.player[this.PlayerWho];
			var otherplr = plr.GetModPlayer<NecrotisPlayer>();

			otherplr.SyncAnima( this.AnimaPercent, (AnimaSource)this.Source );
		}
	}
}
