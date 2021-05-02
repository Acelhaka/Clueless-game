	wsUri = "ws://127.0.0.1:32123/ws";
	websocket = new WebSocket(wsUri);

	websocket.onopen = function (e) {
		return "CONNECTED";
	};

	websocket.onclose = function (e) {
		return "DISCONNECTED";
	};

	websocket.onmessage = function (e) {
		console.log(e.data);
		//document.getElementById("chatLogs").innerHTML += "<p>" + e.data + "</p>";
		//document.getElementById("msg").value = e.data; //"3.14159";
		//get(".msger-inputarea").submit();
		//document.getElementById("msg").value = "";
		//showMessage(e.data);
		var data = JSON.parse(e.data);
		appendMessage(data["UpdateObject"]["SenderName"], "img/327779.svg", "left", data["UpdateObject"]["Content"]);
		return e.data;
	};

	websocket.onerror = function (e) {
		return e.data;
	};

	function doSend(message) {
		//writeToScreen("SENT: " + message);
		websocket.send(message);
	}

	this.connectToServer = function(host, name) {
		//console.log(JSON.stringify({ 'IsHost': host, 'Name': name }));
		doSend(JSON.stringify({ 'IsHost': host, 'Name': name }));
	};

	this.startGame = function() {
		//websocket.send();
		doSend(JSON.stringify({ "UpdateType": 7, "UpdateObjectType": null, "UpdateObject": null }));

};


this.sendMessage = function () {
	console.log("inside send message, m = ", document.getElementById("msg").value);
	doSend(JSON.stringify({ "UpdateType": 8, "UpdateObjectType": "CluelessNetwork.TransmittedTypes.ChatMessage", "UpdateObject": { "Content": document.getElementById("msg").value, "SenderName": null, "Scope": 0 } }));
	//get(".msger-inputarea").submit();
	//document.getElementById("msg").value = m; //"3.14159";
	//get(".msger-inputarea").submit();
	//console.log("msgr-input = ", get(".msger-inputarea"));

}

	







/*
var cards = [];


function getPossibleMoves() {
	
	
}


function makeMove(room) {
	console.log('moving to: ', room);
}


function makeAccusation() {
	
}

function endTurn() {
	
	
}


function makeSuggestion() {
	
	
}


function disproveSuggestion() {

	
}


function init() {
	
	// this should give the player their cards
	
	// assign the weapons to the room
	cards = ['', '', ''];
	
}
*/



