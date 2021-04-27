function generateBoard() {

	var html = '<div class="container" style="float:left;padding-left:20px;">' +
					// ROW 1 of the board
					' <div class="row"> ' +
						'<div class="card" title="Study" style="background-image:url(img/board-images/study.png); height:120px;width:170px;">' +
							'<div class="card-body">' +
								'<h5 class="card-title" style=""><center></center></h5>' +
									'<center><span id="r0"></span></center>' +
							'</div>'+
						'</div>' +
						'<div class="card" title="Hallway" style="height:120px; width:250px; background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								' <h5 class="card-title"><center></center></h5> ' +
								' <center><span id="r1"></span></center> ' +
							'</div>' +
						'</div>' +
						'<div class="card" title="Hall" style="background-image:url(img/board-images/hall.png); height:120px;width:170px;">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>'+
								'<center><span id="r2"></span></center>' +
							'</div>' +
						'</div>' +
						'<div class="card" title="Hallway" style="height:120px; width:250px;background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r3"><span title="Miss Scarlet"><img src="img/characterIcons/MissScarlet.PNG"></span></center>' +
							'</div>' +
						'</div>' +
						'<div class="card" title="Lounge" style="background-image:url(img/board-images/lounge.png); height:120px;width:170px;">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r4"></span></center>' +
							'</div>' +
						'</div>' +
					'</div>' +
					// ROW 2 of the board
					'<div class="row">' +
						'<div class="card" title="Hallway" style="height:250px; width:170px;background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span title="Professor Plum"><img src="img/characterIcons/ProfessorPlum.PNG"></span></center>' +
						'</div>' +
					'</div>' +
					'<div  style="height:250px; width:250px;">&nbsp;</div>' +
					'<div class="card" title="Hallway" style="height:250px; width:170px;background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r6"></span></center>' +
						'</div>' +
					'</div>' +
					'<div  style="height:120px; width:250px;">&nbsp;</div>' +
					'<div class="card" title="Hallway" style="height:250px; width:170px;background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r7" title="Col. Mustard"><img src="img/characterIcons/ColonelMustard.PNG"></span></center>' +
						'</div>' +
					'</div>' +
				'</div>' +
					// ROW 3 of the board
					'<div class="row">' +
						'<div class="card" title="Library" style="background-image:url(img/board-images/library.png); height:120px;width:170px;">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r8"></span></center>' +
						'</div>' +
					'</div>' +
					'<div class="card" title="Hallway" style="height:120px; width:250px;background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r9"></span></center>' +
						'</div>'+
					'</div>'+
					'<div class="card" title="Billiards" style="background-image:url(img/board-images/billiards.png); height:120px;width:170px;">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r10"></span></center>' +
						'</div>' +
					'</div>' +
					'<div class="card" title="Hallway" style="height:120px; width:250px; background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r11"></span></center>' +
						'</div>' +
					'</div>' +
					'<div class="card" title="Dinning" style="height:120px;width:170px;background-image:url(img/board-images/dinning.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r12"></span></center>' +
						'</div>' +
					'</div>' +
				'</div>' +
				// ROW 4 of the board
				'<div class="row">' +
					'<div class="card" title="Hallway" style="height:250px; width:170px; background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r13" title="Mrs. Peacock"><img src="img/characterIcons/MrsPeacock.PNG"></span></center>' +
							'</div>'+
						'</div>'+
					'<div style="height:120px; width:250px;">&nbsp;</div>' +
						'<div class="card" title="Hallway" style="height:250px; width:170px; background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r14"></span></center>' +
							'</div>' +
						'</div>' +
					'<div  style="height:120px; width:250px;">&nbsp;</div>' +
						'<div class="card" title="Hallway" style="height:250px; width:170px; background-image:url(img/board-images/hallway-tile.png);">' +
							'<div class="card-body">' +
								'<h5 class="card-title"><center></center></h5>' +
								'<center><span id="r15"></span></center>' +
							'</div>' +
						'</div>' +
					'</div>' +
				//ROW 5
				'<div class="row">' +
					'<div class="card" title="Conservatory" style="height:120px;width:170px;background-image:url(img/board-images/conservatory.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r16"></span></center>' +
						'</div>' +
					'</div>' +
		
					'<div class="card" title="Hallway" style="height:120px; width:250px;background-image:url(img/board-images/hallway-tile.png);">' +
						'<div class="card-body">' +
							'<h5 class="card-title"><center></center></h5>' +
							'<center><span id="r17" title="Mr. Green"><img src="img/characterIcons/ReverendGreen.PNG"></span></center>' +
						'</div>' +
					'</div>' +
				'<div class="card" title="Ballroom" style="height:120px;width:170px;background-image:url(img/board-images/ballroom.png);">' +
					'<div class="card-body">' +
						'<h5 class="card-title"><center></center></h5>' +
						'<center><span id="r18"></span></center>' +
					'</div>' +
				'</div>' +
				'<div class="card" title="Hallway" style=";height:120px; width:250px;background-image:url(img/board-images/hallway-tile.png);">' +
					'<div class="card-body">' +
						'<h5 class="card-title"><center></center></h5>' +
						'<center><span id="r19" title="Mrs. White"><img src="img/characterIcons/MrsWhite.PNG"></span></center>' +
					'</div>' +
				'</div>' +
				'<div class="card" title="Kitchen" style="height:120px;width:170px;background-image:url(img/board-images/kitchen.png);">' +
					'<div class="card-body">' +
						'<h5 class="card-title"><center></center></h5>' +
					'</div>' +
				'</div>' +
			'</div>' +
		'</div>';
	document.getElementById('gameboard').innerHTML = html;
	
}