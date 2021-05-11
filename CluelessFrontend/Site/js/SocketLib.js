	wsUri = "ws://127.0.0.1:80/ws";
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

		// this means ChatMessage and thus, append the message to the chat log in the interface
		if (data["UpdateType"] == 8) {
			appendMessage(data["UpdateObject"]["SenderName"], "img/327779.svg", "left", data["UpdateObject"]["Content"]);

		} else if (data["UpdateType"] == 7) {
			console.log("Game Start!....need to update the player cards....and set the weapon to room mapping: ", data);
			generateCards(data["UpdateObject"]["Cards"]);
			generateWeaponTokens();
			generateMovePad("nextMove");

		} else if (data["UpdateType"] == 10) {
			console.log("turn information sent!");
			console.log("data = ", data);
			// TODO make this function 
			updatePlayerTurn(data["UpdateObject"]["NewTurnPlayer"], data["UpdateObject"]["IsMyTurn"])

		}else if (data["UpdateType"] < 7) {
			console.log("..... response ", data);
		}
		
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
		sendSuspectSelection(name);
		
	};

	this.startGame = function() {
		//websocket.send();
		doSend(JSON.stringify({ "UpdateType": 7, "UpdateObjectType": null, "UpdateObject": null }));
		//generateCards(["SCARLET", "KNIFE", "STUDY", "BILLARD", "WHITE", "PLUM"]);
		//generateCards()
		// TOOD add this from the server similar to the cards provided
		generateWeaponTokens("");
		// erase the start game button since it was already pressed
		document.getElementById('startGameButtonId').innerHTML = "";
		generateMovePad("nextMove");

	};

	this.endTurn = function () {
		console.log("sending endTurn request to the server....");
		//generateWeaponTokens();
		doSend(JSON.stringify({ "UpdateType": 11, "UpdateObjectType": null, "UpdateObject": null }));
	};

	this.sendSuspectSelection = function (name) {
		//val = document.getElementById('playerSuspectValue').value;
		val = getSuspectValueName();
		suspect_value = "";
		radiosPlayerSelect = document.getElementsByName('playerSelect');
		if (radiosPlayerSelect[0].checked) {
			val = 0
			suspect_value = 1;
		}
		else if (radiosPlayerSelect[1].checked) {
			val = 1;
			suspect_value = 0;
		}
		else if (radiosPlayerSelect[2].checked) {
			val = 2;
			suspect_value = 4;
		}
		else if (radiosPlayerSelect[3].checked) {
			val = 3;
			suspect_value = 5;
		}
		else if (radiosPlayerSelect[4].checked) {
			val = 4;
			suspect_value = 3;
		}
		else if (radiosPlayerSelect[5].checked) {
			val = 5;
			suspect_value = 2;
        }
			
		doSend(JSON.stringify({ "UpdateType": 6, "UpdateObjectType": "CluelessNetwork.TransmittedTypes.SuspectSelectionUpdate", "UpdateObject": { "SuspectSelected": val, "PlayerName": null } }));

		// so the client knows it's location
		updatePlayerLocation(val);
		setSuspectVal(suspect_value);
	}

	

this.moveRoom = function (room, currentRoom, suspectVal) {
	var idMap = {
		'Study': 0,
		'Hallway-r1': 9,
		'Hall': 1,
		'Hallway-r3': 10,
		'Lounge': 2,
		'Hallway-r5': 11,
		'Hallway-r6': 12,
		'Hallway-r7': 13,
		'Library': 3,
		'Hallway-r8': 14,
		'Billiards': 4,
		'Hallway-r9': 15,
		'Dinning': 5,
		'Hallway-r10': 16,
		'Hallway-r11': 17,
		'Hallway-r12': 18,
		'Conservatory': 6,
		'Hallway-r14': 19,
		'Ballroom': 7,
		'Hallway-r19': 20,
		'Kitchen': 8

	}
	if (room in idMap) {
		console.log("moveRoom, ", room);
		//doSend(JSON.stringify({ "UpdateType": 1, "UpdateObjectType": null, "UpdateObject": { "room": idMap[room] } }));
		//TODO figure out the hallway-r3 and other hallway id values dont seem to have an ENUM type...
		movePlayer(suspectVal, idMap[room], currentRoom);
		generateMovePad(null);
	} else {
		console.log("room " + room + ", not found in isMap....");
    }
		
	};


this.sendMessage = function () {
	console.log("inside send message, m = ", document.getElementById("msg").value);
	doSend(JSON.stringify({ "UpdateType": 8, "UpdateObjectType": "CluelessNetwork.TransmittedTypes.ChatMessage", "UpdateObject": { "Content": document.getElementById("msg").value, "SenderName": null, "Scope": 0 } }));
}