# PogoWars

This is a pogo stick fighting game.

Players must kill the other player by landing their pogostick on the other player's head in a best of 5 rounds. 
Player 1: A & D to move, SPACE to jump
Player 2: J & L to move, RIGHT CTRL to jump

Controller support (UNTESTED) should work?

Created with the help of the Unity Tanks tutorial for the GameManager
And Bracky's Youtube tutorial for camera movement

Both were adapted to suit the game and were used as pointers.

Known bugs:
There is no delay or gap in the amount of hits registered with each raycast meaning players die instantly.
The first round plays and is immediately over which is counted as a draw.
Text may not fit some aspect ratios and settings file has only been tested on one machine so it may throw an error.

**Building the application**

1. Download & Install `Unity 3D 2018.3.9f1`or later
2. Open the `PogoWars` folder
3. File->Build Settings
