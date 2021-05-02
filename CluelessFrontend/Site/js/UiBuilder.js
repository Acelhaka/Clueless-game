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
						"Hallway-20": "location_r20"};
						

var weapons_to_links = { "CANDLESTICK": "img/weaponIcons/Candlestick.PNG",
						 "KNIFE": "img/weaponIcons/Dagger.PNG",
						 "PIPE": "img/weaponIcons/LeadPipe.PNG",
						 "REVOLVER": "img/weaponIcons/Revolver.PNG",
						 "ROPE": "img/weaponIcons/Rope.PNG",
						 "SPANNER": "img/weaponIcons/Spanner.PNG"};
						
						

function generateBoard() {

	var html = '<div class="container" style="float:left;padding-left:20px;">' +
					// ROW 1 of the board
					' <div class="row"> ' +
						'<div id="location_r0" class="card" title="Study" style="background-image:url(img/board-images/study.png); height:120px;width:170px;">' +
							'<div class="card-body">' +
								'<h5 class="card-title" style=""><center></center></h5>' +
									// TODO need to add a suspect and weapon section to each location so weapons and suspects can get assigned to the location (hallways dont need weapons though)
									'<center><p style="float:top;" id="r0_suspects"></p>' +
									'<p style="float:bottom;" id="r0_weapons"></p>'+
									'</center>' +
							'</div>'+
						'</div>' +
						'<div id="location_r1" class="card" title="Hallway" style="height:120px; width:250px; background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								' <h5 class="card-title"><center></center></h5> ' +
								' <center><span id="r1"></span></center> ' +
							'</div>' +
						'</div>' +
						'<div id="location_r2" class="card" title="Hall" style="background-image:url(img/board-images/hall.png); height:120px;width:170px;">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>'+
								'<center><span id="r2"></span></center>' +
							'</div>' +
						'</div>' +
						'<div id="location_r3" class="card" title="Hallway" style="height:120px; width:250px;background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r3"><span title="Miss Scarlet"><img src="img/characterIcons/MissScarlet.PNG"></span></center>' +
							'</div>' +
						'</div>' +
						'<div id="location_r4" class="card" title="Lounge" style="background-image:url(img/board-images/lounge.png); height:120px;width:170px;">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r4"></span></center>' +
							'</div>' +
						'</div>' +
					'</div>' +
					// ROW 2 of the board
					'<div class="row">' +
						'<div id="location_r5" class="card" title="Hallway" style="height:250px; width:170px;background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r5" title="Professor Plum"><img src="img/characterIcons/ProfessorPlum.PNG"></span></center>' +
						'</div>' +
					'</div>' +
					'<div  id="location_r6" style="height:250px; width:250px;">&nbsp;</div>' +
					'<div class="card" title="Hallway" style="height:250px; width:170px;background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r6"></span></center>' +
						'</div>' +
					'</div>' +
					'<div  style="height:120px; width:250px;">&nbsp;</div>' +
					'<div id="location_r7" class="card" title="Hallway" style="height:250px; width:170px;background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r7" title="Col. Mustard"><img src="img/characterIcons/ColonelMustard.PNG"></span></center>' +
						'</div>' +
					'</div>' +
				'</div>' +
					// ROW 3 of the board
					'<div class="row">' +
						'<div id="location_r8" class="card" title="Library" style="background-image:url(img/board-images/library.png); height:120px;width:170px;">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r8"></span></center>' +
						'</div>' +
					'</div>' +
					'<div id="location_r9" class="card" title="Hallway" style="height:120px; width:250px;background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r9"></span></center>' +
						'</div>'+
					'</div>'+
					'<div id="location_r10" class="card" title="Billiards" style="background-image:url(img/board-images/billiards.png); height:120px;width:170px;">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r10"></span></center>' +
						'</div>' +
					'</div>' +
					'<div id="location_r11" class="card" title="Hallway" style="height:120px; width:250px; background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r11"></span></center>' +
						'</div>' +
					'</div>' +
					'<div id="location_r12" class="card" title="Dinning" style="height:120px;width:170px;background-image:url(img/board-images/dinning.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r12"></span></center>' +
						'</div>' +
					'</div>' +
				'</div>' +
				// ROW 4 of the board
				'<div class="row">' +
					'<div id="location_r13" class="card" title="Hallway" style="height:250px; width:170px; background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r13" title="Mrs. Peacock"><img src="img/characterIcons/MrsPeacock.PNG"></span></center>' +
							'</div>'+
						'</div>'+
					'<div style="height:120px; width:250px;">&nbsp;</div>' +
						'<div id="location_r14" class="card" title="Hallway" style="height:250px; width:170px; background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r14"></span></center>' +
							'</div>' +
						'</div>' +
					'<div  style="height:120px; width:250px;">&nbsp;</div>' +
						'<div id="location_r15" class="card" title="Hallway" style="height:250px; width:170px; background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r15"></span></center>' +
							'</div>' +
						'</div>' +
					'</div>' +
				//ROW 5
				'<div class="row">' +
					'<div id="location_r16" class="card" title="Conservatory" style="height:120px;width:170px;background-image:url(img/board-images/conservatory.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r16"></span></center>' +
						'</div>' +
					'</div>' +
		
					'<div id="location_r17" class="card" title="Hallway" style="height:120px; width:250px;background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r17" title="Mr. Green"><img src="img/characterIcons/ReverendGreen.PNG"></span></center>' +
						'</div>' +
					'</div>' +
				'<div id="location_r18" class="card" title="Ballroom" style="height:120px;width:170px;background-image:url(img/board-images/ballroom.png);">' +
					'<div class="card-body">' +
						'<h5 class="card-title"><center></center></h5>' +
						'<center><span id="r18"></span></center>' +
					'</div>' +
				'</div>' +
				'<div id="location_r19" class="card" title="Hallway" style=";height:120px; width:250px;background-image:url(img/board-images/hallway-tile.png);">' +
					'<div class="card-body">' +
						'<h5 class="card-title"><center></center></h5>' +
						'<center><span id="r19" title="Mrs. White"><img src="img/characterIcons/MrsWhite.PNG"></span></center>' +
					'</div>' +
				'</div>' +
				'<div id="location_r20" class="card" title="Kitchen" style="height:120px;width:170px;background-image:url(img/board-images/kitchen.png);">' +
					'<div class="card-body">' +
						'<h5 class="card-title"><center><span id="r20"></span></center></h5>' +
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


function suggestion_popup() {
	
	
}
	