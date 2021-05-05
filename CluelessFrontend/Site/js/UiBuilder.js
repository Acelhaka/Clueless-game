var location_to_ids = { "Study":"location_r0", 
						"Hallway-r1": "location_r1",
						"Hall": "location_r2",
						"Hallway-r3": "location_r3",
					    "Lounge": "location_r4",
						"Hallway-r5": "location_r5",
						"Hallway-r6": "location_r6",
						"Hallway-r7": "location_r7",
						"Library": "location_r8",
						"Hallway-r9": "location_r9",
						"Billards": "location_r10",
						"Hallway-11": "location_r11",
						"Dinning": "location_r12",
						"Hallway-13": "location_r13",
						"Hallway-14": "location_r14",
						"Hallway-15": "location_r15",
						"Conservatory": "location_r16",
						"Hallway-17": "location_r17",
						"Ballroom": "location_r18",
						"Hallway-19": "location_r19",
						"Kitchen": "location_r20"};
						

var weapons_token_to_links = {
		"CANDLESTICK": { "src": "img/weaponIcons/Candlestick.PNG", "alt": "Candlestick" },
		"KNIFE": { "src": "img/weaponIcons/Dagger.PNG", "alt": "Dagger" },
		"PIPE": { "src": "img/weaponIcons/LeadPipe.PNG", "alt": "Pipe" },
		"REVOLVER": { "src": "img/weaponIcons/Revolver.PNG", "alt": "Revolver" },
		"ROPE": { "src": "img/weaponIcons/Rope.PNG", "alt": "Rope" },
		"SPANNER": { "src": "img/weaponIcons/Spanner.PNG", "alt": "Spanner" }
};

var weapons_to_links = {
	"CANDLESTICK": { "src": "img/weapons/Candlestick.PNG", "alt": "Candlestick" },
	"KNIFE": { "src": "img/weapons/Dagger.PNG", "alt": "Dagger" },
	"PIPE": { "src": "img/weapons/LeadPipe.PNG", "alt": "Pipe" },
	"REVOLVER": { "src": "img/weapons/Revolver.PNG", "alt": "Revolver" },
	"ROPE": { "src": "img/weapons/Rope.PNG", "alt": "Rope" },
	"SPANNER": { "src": "img/weapons/Spanner.PNG", "alt": "Spanner" }
};

var suspects_to_links = {
	"SCARLET": { "src": "img/characters/MissScarlet.png", "alt": "Miss Scarlet" },
	"MUSTARD": { "src": "img/characters/ColnelMustard.png", "alt": "Colonel Mustard" },
	"PEACOCK": { "src": "img/characters/MrsPeakcock.png", "alt": "Mrs Peacock" },
	"WHITE": { "src": "img/characters/MrsWhite.png", "alt": "Mrs White" },
	"PLUM": { "src": "img/characters/ProfessorPlum.png", "alt": "Professor Plum" },
	"GREEN": { "src": "img/characters/ReverendGreen.png", "alt": "Mr Green" }
};

var suspectsTokens_to_links = {
	"SCARLET": { "src": "img/characterIcons/MissScarlet.png", "alt": "Miss Scarlet" },
	"MUSTARD": { "src": "img/characterIcons/ColonelMustard.png", "alt": "Colonel Mustard" },
	"PEACOCK": { "src": "img/characterIcons/MrsPeacock.png", "alt": "Mrs Peacock" },
	"WHITE": { "src": "img/characterIcons/MrsWhite.png", "alt": "Mrs White" },
	"PLUM": { "src": "img/characterIcons/ProfessorPlum.png", "alt": "Professor Plum" },
	"GREEN": { "src": "img/characterIcons/ReverendGreen.png", "alt": "Mr Green" }
};

var room_to_links = {
	"BALLROOM": { "src": "img/rooms/Ballroom.png", "alt": "Ballroom" },
	"BILLARD": { "src": "img/rooms/BillardRoom.png", "alt": "Billard Room" },
	"CONSERVATORY": { "src": "img/rooms/Conservatory.png", "alt": "Conservatory" },
	"DINNING": { "src": "img/rooms/DinningRoom.png", "alt": "Dinning Room" },
	"HALL": { "src": "img/rooms/Hall.png", "alt": "Hall" },
	"KITCHEN": { "src": "img/rooms/Kithcen.png", "alt": "Kitchen" },
	"LIBRARY": { "src": "img/rooms/Library.png", "alt": "Library" },
	"LOUNGE": { "src": "img/rooms/Lounge.png", "alt": "Lounge" },
	"STUDY": { "src": "img/rooms/Study.png", "alt": "Study" }
};

var cards_to_links = Object.assign({}, room_to_links, suspects_to_links, weapons_to_links);


function generateCards(cards) {
	var html = "<p>";
	console.log("cards_to_links = ", cards_to_links);
	console.log("cards = ", cards);
	for (var c in cards) {
		console.log("c = ", c);
		html += "<img class='zoom' src='" + cards_to_links[cards[c]].src + "' title='" + cards_to_links[cards[c]].alt + "' height=100px; width:100px; alt='" + cards_to_links[cards[c]].alt + "'/>";
	}
	html += "</p>";
	document.getElementById("playerCards").innerHTML = html;
}						

function generateBoard() {

	var html = '<div class="container" style="float:left;">' +
					// ROW 1 of the board
					' <div class="row"> ' +
						'<div id="location_r0" class="card room" title="Study" style="background-image:url(img/board-images/study.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title" style=""><center></center></h5>' +
									// TODO need to add a suspect and weapon section to each location so weapons and suspects can get assigned to the location (hallways dont need weapons though)
									'<center><p style="float:top;" id="r0_suspects"></p>' +
									'<p style="float:bottom;" id="r0_weapons"></p>'+
									'</center>' +
							'</div>'+
						'</div>' +
						'<div id="location_r1" class="card horizHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								' <h5 class="card-title"><center></center></h5> ' +
								' <center><span id="r1"></span></center> ' +
							'</div>' +
						'</div>' +
						'<div id="location_r2" class="card room" title="Hall" style="background-image:url(img/board-images/hall.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>'+
								'<center><p style="float:top;" id="r2_suspects"></p>' +
								'<p style="float:bottom;" id="r2_weapons"></p>' +
								'</center>' +
							'</div>' +
						'</div>' +
						'<div id="location_r3" class="card horizHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r3"><span title="Miss Scarlet"><img src="img/characterIcons/MissScarlet.PNG"></span></center>' +
							'</div>' +
						'</div>' +
						'<div id="location_r4" class="card room" title="Lounge" style="background-image:url(img/board-images/lounge.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><p style="float:top;" id="r4_suspects"></p>' +
								'<p style="float:bottom;" id="r4_weapons"></p>' +
								'</center>' +
							'</div>' +
						'</div>' +
					'</div>' +
					// ROW 2 of the board
					'<div class="row">' +
						'<div id="location_r5" class="card vertHall" title="Hallway" background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r5" title="Professor Plum"><img src="img/characterIcons/ProfessorPlum.PNG"></span></center>' +
						'</div>' +
					'</div>' +
					'<div  id="location_r6" class="fillHall" style="">&nbsp;</div>' +
					'<div class="card vertHall" title="Hallway" background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r6"></span></center>' +
						'</div>' +
					'</div>' +
					'<div  class="fillHall">&nbsp;</div>' +
					'<div id="location_r7" class="card vertHall" title="Hallway" background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r7" title="Col. Mustard"><img src="img/characterIcons/ColonelMustard.PNG"></span></center>' +
						'</div>' +
					'</div>' +
				'</div>' +
					// ROW 3 of the board
					'<div class="row">' +
						'<div id="location_r8" class="card room" title="Library" style="background-image:url(img/board-images/library.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><p style="float:top;" id="r8_suspects"></p>' +
							'<p style="float:bottom;" id="r8_weapons"></p>' +
							'</center>' +
						'</div>' +
					'</div>' +
					'<div id="location_r9" class="card horizHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r9"></span></center>' +
						'</div>'+
					'</div>'+
					'<div id="location_r10" class="card room" title="Billiards" style="background-image:url(img/board-images/billiards.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><p style="float:top;" id="r10_suspects"></p>' +
							'<p style="float:bottom;" id="r10_weapons"></p>' +
							'</center>' +
						'</div>' +
					'</div>' +
					'<div id="location_r11" class="card horizHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r11"></span></center>' +
						'</div>' +
					'</div>' +
					'<div id="location_r12" class="card room" title="Dinning" style="background-image:url(img/board-images/dinning.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><p style="float:top;" id="r12_suspects"></p>' +
							'<p style="float:bottom;" id="r12_weapons"></p>' +
							'</center>' +
						'</div>' +
					'</div>' +
				'</div>' +
				// ROW 4 of the board
				'<div class="row">' +
					'<div id="location_r13" class="card vertHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r13" title="Mrs. Peacock"><img src="img/characterIcons/MrsPeacock.PNG"></span></center>' +
							'</div>'+
						'</div>'+
					'<div class="fillHall">&nbsp;</div>' +
						'<div id="location_r14" class="card vertHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r14"></span></center>' +
							'</div>' +
						'</div>' +
					'<div class="fillHall">&nbsp;</div>' +
						'<div id="location_r15" class="card vertHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r15"></span></center>' +
							'</div>' +
						'</div>' +
					'</div>' +
				//ROW 5
				'<div class="row">' +
					'<div id="location_r16" class="card room" title="Conservatory" style="background-image:url(img/board-images/conservatory.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><p style="float:top;" id="r16_suspects"></p>' +
									'<p style="float:bottom;" id="r16_weapons"></p>' +
							'</center>' +
						'</div>' +
					'</div>' +
		
					'<div id="location_r17" class="card horizHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r17" title="Mr. Green"><img src="img/characterIcons/ReverendGreen.PNG"></span></center>' +
						'</div>' +
					'</div>' +
				'<div id="location_r18" class="card room" title="Ballroom" style="background-image:url(img/board-images/ballroom.png);">' +
					'<div class="card-body">' +
						'<h5 class="card-title"><center></center></h5>' +
						'<center><p style="float:top;" id="r18_suspects"></p>' +
						'<p style="float:bottom;" id="r18_weapons"></p>' +
						'</center>' +
					'</div>' +
				'</div>' +
				'<div id="location_r19" class="card horizHall" title="Hallway" style="background-image:url(img/board-images/hallway-tile.png);">' +
					'<div class="card-body">' +
						'<h5 class="card-title"><center></center></h5>' +
						'<center><span id="r19" title="Mrs. White"><img src="img/characterIcons/MrsWhite.PNG"></span></center>' +
					'</div>' +
				'</div>' +
				'<div id="location_r20" class="card room" title="Kitchen" style="background-image:url(img/board-images/kitchen.png);">' +
					'<div class="card-body">' +
					'<h5 class="card-title"><center><span id="r20"></span></center></h5>' +
					'<center><p style="float:top;" id="r20_suspects"></p>' +
					'<p style="float:bottom;" id="r20_weapons"></p>' +
					'</center>' +
					'</div>' +
				'</div>' +
			'</div>' +
		'</div>';
	document.getElementById('gameboard').innerHTML = html;
	
}

function generateChecklist() {
	
		var html = "<table><th>&nbsp;</th><th><center>P1</center></th><th><center>P2</center></th><th><center>P3</center></th><th><center>P4</center></th><th><center>P5</center></th><th><center>P6</center></th></tr>";
		var suspects = ["Miss Scarlet", "Professor Plum", "Col. Mustard", "Mrs. Peacock", "Mr. Green", "Mrs. White"];
		var dropdown_options = "<td><select class='custom-select' id='inputGroupSelect01'><option selected>-</option><option value='1'>Y</option><option value='2'>N</option><option value='3'>?</option></select></td>";
		for (var s in suspects){
			html += "<tr><td><h5>" + suspects[s] + "</h5></td>";
			
			for (var i =0; i < 6; i++) {
			
				html += dropdown_options;
			}
			html += "</tr>";
		}
		var weapons = ["Knife", "Rope", "Wrench", "CandleStick", "Revolver", "Pipe"];
		for (var w in weapons){
			html += "<tr><td><h5>" + weapons[w] + "</h5></td>";
			
			for (var i =0; i < 6; i++) {
			
				html += dropdown_options;
			}
			html += "</tr>";
		}
		var rooms = ["Study", "Hall", "Lounge", "Library", "Billiards Room", "Dining Room", "Conservatory", "Ballroom", "Kitchen"];
		for (var r in rooms){
			html += "<tr><td><h5>" + rooms[r] + "</h5></td>";
			
			for (var i =0; i < 6; i++) {
			
				html += dropdown_options;
			}
			html += "</tr>";
		}
		
		html += "</table>";
		document.getElementById('checklist-placeholder').innerHTML = html;
}

// TODO eventually pass in the locations that player can move to
function generateMovePad() {
	html = '<a class="up" href="#" id="upPad" title="N/A"></a>' +
		   '<a class="right" href="#" id="rightPad" title="Lounge" onmouseover=highlightRoom(\'Lounge\'); onmouseout=unhighlightRoom(\'Lounge\');></a>' +
		   '<a class="down" href="#" id="downPad" title="N/A"></a>' +
		   '<a class="left" href="#" id="leftPad" title="Hall" onmouseover=highlightRoom(\'Hall\'); onmouseout=unhighlightRoom(\'Hall\');></a>';
	document.getElementById('dpad').innerHTML = html;
}

//location_r0 -> study
function highlightRoom(room) {
	
	if (room in location_to_ids) {
		document.getElementById(location_to_ids[room]).style.cssText += "opacity: 0.4;filter: alpha(opacity=40);";
		
		// testing out how the suspects and weapons look on the location
		//document.getElementById('r0_weapons').innerHTML = "<img width=30 height=30 src='img/weaponIcons/Candlestick.PNG' title='Candlestick'>";
		//document.getElementById('r0_suspects').innerHTML = "<img width=30 height=30 src='img/weaponIcons/Candlestick.PNG' title='Candlestick'><img width=30 height=30 src='img/weaponIcons/Candlestick.PNG' title='Candlestick'>";
	}
}

function unhighlightRoom(room) {
	
	if (room in location_to_ids) {
		originalCss = document.getElementById(location_to_ids[room]).style.cssText;
		document.getElementById(location_to_ids[room]).style.cssText = originalCss.replace("opacity: 0.4;","");
	}
	
}

/* No longer needed?
function suspectSelectionDropdown() {
	/* the backend stores this os an enum so use the value as numbers
	 * COLNEL_MUSTARD	= 0
	   MISS_SCARLET	= 1
	   PROFESSOR_PLUM	= 2
	   MR_GREEN	= 3
	   MRS_WHITE	= 4
	   MRS_PEACOCK	= 5
	
	// send updateType: 6, UpdateObjectType: SuspectSelectionUpdate PlayerName, no scope needed
	//playerSuspectValue
	// include a dropdown in the host / lobby to allow them to select which suspect they want to run // send gameType of 6 (SuspectSelection) 
	var html = "<div class='btn-group'>" +
					"<button type='button' class='btn btn-info'>Select Suspect</button>" +
					"<button type='button' class='btn btn-info dropdown-toggle dropdown-toggle-split' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>" +
					"<span class='sr-only'>Toggle Dropdown</span></button>" + 
					"<div class='dropdown-menu'>" +
						"<a class='dropdown-item' onclick='updatePlayerSuspectValue(0);'>Colnel Mustard</a>" +
						"<a class='dropdown-item' onclick='updatePlayerSuspectValue(1);'>Miss Scarlet</a>" +
						"<a class='dropdown-item' onclick='updatePlayerSuspectValue(2);'>Professor Plum</a>" +
						"<a class='dropdown-item' onclick='updatePlayerSuspectValue(3);'>Mr Green</a>" +
						"<a class='dropdown-item' onclick='updatePlayerSuspectValue(4);'>Mrs White</a>" +
						"<a class='dropdown-item' onclick='updatePlayerSuspectValue(5);'>Mrs Peacock</a>" +
					"</div>" +
				"</div>"

	document.getElementById('pickSuspectDropdown').innerHTML = html;
}
*/

function getSuspectValueName() {
	//val = document.getElementById('playerSuspectValue').value;
  radiosPlayerSelect = document.getElementsByName('playerSelect');
	//console.log("playerSuspectValue = ", document.getElementById('playerSuspectValue').value);
	var name = "";
	if (radiosPlayerSelect[0].checked)
		name = "Col. Mustard";
	else if (radiosPlayerSelect[1].checked)
		name = "Miss Scarlet";
	else if (radiosPlayerSelect[2].checked)
		name = "Professor Plum";
	else if (radiosPlayerSelect[3].checked)
		name = "Mr. Green";
	else if (radiosPlayerSelect[4].checked)
		name = "Mrs. White";
	else if (radiosPlayerSelect[5].checked)
		name = "Mrs. Peacock";
	return name;
}

/* No longer needed?
function updatePlayerSuspectValue(val) {
	document.getElementById('playerSuspectValue').value = val;
	//console.log("playerSuspectValue = ", document.getElementById('playerSuspectValue').value);
	var name = "";
	if (val == 0)
		name = suspectsTokens_to_links["MUSTARD"];
	else if (val == 1)
		name = suspectsTokens_to_links["SCARLET"]; 
	else if (val == 2)
		name = suspectsTokens_to_links["PLUM"]; 
	else if (val == 3)
		name = suspectsTokens_to_links["GREEN"]; 
	else if (val == 4)
		name = suspectsTokens_to_links["WHITE"]; 
	else if (val == 5)
		name = suspectsTokens_to_links["PEACOCK"];

	document.getElementById('playerSuspectName').innerHTML = "<img src='"+ name.src + "' title='" + name.alt +"'/>";
}*/

function suggestion_popup() {
	
	
}

function generateWeaponTokens(rooms) {
	// TODO once we get the server response, we can loop through what the server responds with...
	rooms_to_weapons = {
		"Ballroom": "CANDLESTICK",
		"Billards": "KNIFE",
		"Conservatory": "PIPE",
		"Dinning": "REVOLVER",
		"Hall": "ROPE",
		"Kitchen": "SPANNER"
	}
	//console.log("inside generate weapon tokens..");
	for (var r in rooms_to_weapons) {
		//console.log("r = ", r);
		if (r in location_to_ids) {
			document.getElementById(location_to_ids[r]).innerHTML = "<span title='" + weapons_token_to_links[rooms_to_weapons[r]].alt +
																	"'><img height='25px' width=25px' src = '" + weapons_token_to_links[rooms_to_weapons[r]].src + "'></span > ";
        }

    }
	
}
	