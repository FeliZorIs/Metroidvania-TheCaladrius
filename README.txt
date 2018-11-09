The Caladrius

HOW TO PLAY:
	When in menus:
		Use the mouse to navigate the menus
		LMB(left mouse Button) to click on the selection you want

	When in Game:
		A:	Moves character left
		D:	Moves character right
		Space:	Makes character jump, press twice for double jump
		P:	Pauses and unpauses game. Brings up pause menu
		ESC:	Pauses and unpauses game. Brings up pause menu
		TAB:	Pauses and unpauses game. Brings up the Map
		F5:	Saves the character's current position
		F9:	Loads the character's saved Position

DELIVERABLES:
	Mapping System w/ Markers:
		When you press tab in game, it brings up the map and from there you can see a very simplistic version of 
		where you are. If you click on another area on the map that is not your own, you can set up a marker that
		will show up in the shape of a star. Once you reach the area that the marker is on, the star will disappear
		because you have reached your destination 

	Dynamic Asset Management:
		Mostly everything is split up into different scenes, mainly the player and the environments. The player
		scene is the one that stays open at all times. There are "gates" at the ends of each level. When the 
		player steps through these "gates" the next scene loads on top the current scene and immediately after the
		previous scene that the player leaves gets unloaded.

	Responsive Environment:
		In the fourth area or TestScene3, there are floor tiles that fall down after a few miliseconds when stepped
		on.
	
	Save System:
		In game, when you press the F5 button, the game saves your character's current position into file. When you
		press F9 in game, the game opens up the file that was previously saved and grabs the character's position
		from the moment you saved and moves the character to that position.

	10 Lock and Key Mechanics:
		currently does not exist.
