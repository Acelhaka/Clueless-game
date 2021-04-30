using System.Collections.Generic;
using WebSocket4Net;

namespace CluelessNetwork.Websockets
{
	internal class ClientWebsocketWrapper : WebsocketWrapperBase
	{
		private readonly WebSocket _websocket;
		private readonly Queue<string> _receivedMessages;

		internal ClientWebsocketWrapper(string uri)
		{
			_websocket = new WebSocket(uri);
			_websocket.Open();
			_receivedMessages = new Queue<string>();
			_websocket.MessageReceived += WebsocketOnMessageReceived;
		}

		private void WebsocketOnMessageReceived(object? sender, MessageReceivedEventArgs e)
		{
			_receivedMessages.Enqueue(e.Message);
		}

		protected override void SendString(string json)
		{
			_websocket.Send(json);
		}

		protected override string RecvString()
		{
			while (true)
			{
				if (_receivedMessages.TryDequeue(out var message))
					return message;
			}
		}

		public override void Dispose()
		{
			_websocket.MessageReceived -= WebsocketOnMessageReceived;
			_websocket.Dispose();
		}
	}
}