COMP 476 Assignment 3
----------------------

This assignment is a multiplayer tank battle where two players can play via a local internet connection and must try and eliminate the other by using a tank to attack the other player. While this happens, the players will be chased by NPC tanks that will try to eliminate the PC's a well. The multiplayer aspect was developed using the Photon Unity Networking tools for Unity.

Much of the code for camera behaviour, Launching the game, and instantiating the game objects such as walls, tanks, NPC tanks, power ups, etc. was heavily guided by the information in the Photon Documentation available at https://doc.photonengine.com/en-us/pun/current/demos-and-tutorials/pun-basics-tutorial/intro.

Project Files
-----------------

At the root of the project is a file called DecisionTree.png. This file shows an image of the decision tree used by the NPC tanks that chase you around the map to determine what they should be doing. The visibility checks are done with raycasting, and the range checks done with a distance from the player. All AI movement was done using a navmesh with dynamic obstacles. Essentially each wall is a navmesh obstacle that when removed, updates the navmesh automatically to allow navigation for the AI through the newly created space.

The scripts are contained in COMP-476-A3/Assets/Scripts. The AI has a script called AIMovement that uses the decision tree found in the DecisionTools Script to dictate the movement and execute it using the navmesh. Each AI has a PhotonView component so that their transforms can be observed by other players. This is the same technique used for bullets, walls, and player tanks, which can also be seen by other players. The AI also has a tank attack script that has two important methods. The first is FireRound() which instantiates a projectile travelling in the direction the AI, or player (since they also have the TankAttack script) is facing.

This projectile has a Projectile Script on it, that moves it in the direction it is facing and has collider logic. When the projectile hits a wall, it calls the Wall Script method Remove since each Wall has a wall script, and this removes the wall on the network. When the projectile hits another tank, the method TakeDamage from the TankAttack script is called using Photon's RPC. This RPC is sent to the PhotonView of the tank that was hit, and that tank updates its current health based on the damage that the projectile does.

Each Player tank also has a TankMovement script that handles input for the players so they can move around. Input handling for attacking is done in the TankAttack script.

The script GameManager is responsible for instantiating a tank for each player, the NPC tanks, as well as all the walls when the tank battle scene is loaded. The Launcher Script is used in the Launcher Scene where players can connect to the room and start playing, as well as enter their display name (done in PlayerNameInputField). This name is saved from session to session by storing it using Photon's PlayerPrefs.

Spawning is done in random fashion based on a list of possible spawn locations. When a tank is created, a random, not yet selected spot is selected for that tank to spawn so that the game begins differently for each playthrough.

The camera for the players is found in the CameraWork Script. It contains methods for following the player, as well as snapping to it when beginning the game.

Finally we have the PowerUp script, which has collider logic for determining if a player has hit the power up. If this happens, the player is granted a speed boost for a limited time, allowing that player to more easily evade NPC tanks or chase down the other player.

Controls
----------------

The player can move around by using WASD. W to go forward, S to go backward, A to rotate the tank right, and D to rotate the tank left. Firing projectiles is done by clicking with the mouse. The projectile is fired in a straight line in the direction the tank is facing.  
