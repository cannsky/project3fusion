## Web3 Moba Game

**This project is an experimental project to see the limits of blockchain gaming.** As a team of three people, we try to bring an example moba game to the web3 space with transferring the functionalty from web2 to web3 as much as possible. The idea of a full-chain game is commonly used in web3 space, but without some smart shortcuts, it is not a possibility in blockchain world.

This project is an open-source project in code base and easy to implement to any type of moba game.

Here is the full project explanation in both game, blockchain and server side.

### Current Stage of the Project

- Web3 side is being developed along with an Unity SDK.
- Game side is still being developed, core mechanics are ready to go.
- Server side will be implemented soon.

### Versions

- v1: Web2 server side implementation (In Progress...)

### Used Technologies

- Web3:
- Game: Unity Engine
- Server: Unity Netcode

### Player - NetworkBehaviour Class

**Name: Player.cs**

**Description:** Player (with NetworkObject) is the instantiated script when a player connects to a server. 
- Network variables of the Player are stored inside Player Data.
- Components handle the job while Player calls them in Unity specific functions (Awake, Start, Update etc.)

### Player Input - MonoBehaviour Class

**Name: PlayerInput.cs**

**Description:** All inputs will be handled inside PlayerInput.
- When the player **[left click, right click, q, w, e, r]** in Update, the bool variable of appropriate variable is going to be set to true. (q, w, e and r are used for skills)
- At the LateUpdate, all of the bool variables will be set to false. 
- All classes that take input from PlayerInput will take bool variables in Update so that the inputs will be renewed at each frame and there won’t be any confusion.

### Player Movement - MonoBehaviour Class

**Name: PlayerMovement.cs**

**Description:** Player movement is responsible for changing the position of the player.
- When the player right clicks to a point, the player is being moved to that point using **Unity Navmesh AI.** 
- Player sends a raycast with the right click input and gets the position of that point in **x, z formation as Vector2.** (y is not needed for now.)
- The player movement system makes sure that the raycast’s hit object’s layer is the **“Terrain”** layer. If the player's layer is different from the **“Terrain”** layer the player is not going to be moved.
- If the player right clicks to an enemy and the range of the player is not enough, the player will move towards the enemy. Explained in more detail in PlayerAttack.
- PlayerMovement also updates player data’s **“Player Movement Destination”** and **“Player Movement Time”** attributes when player right clicks to somewhere.

### Player Animator - MonoBehaviour Class

**Name: PlayerAnimator.cs**

**Description:** Animations of the player handled by Player Animator.
- PlayerAnimator will get the Animator Controller of the Animator from Champion.cs ScriptableObject. (Champion.cs script explained more in detail below.)
- PlayerMovement calls PlayerAnimator to set animation to **“Run”** when moving and if not calls to set animation to **“Idle”**. 
- Player Attack calls PlayerAnimator to trigger animation to **“Attack”**.

### Player Data - Normal Class

**Name: PlayerData.cs**

**Description:** Player Data stores data related to each player. PlayerData will not be a MonBehaviour class, instead it is a normal class binded to Player with a reference. (This is done for data optimization.)
- **Player Movement Destination (Type: Vector2):** the position the player wants to go that will be saved when the player right clicks to a position on terrain or when an enemy is not in the range of the player.
- **Player Movement Time:** the time the player right clicked for the movement destination last time or the time the player tried to attack an enemy at outside of the player’s range. Desired action time is saved as **[dd.mm.yyyy hh:mm:ss:xxxx] (x represents milliseconds)**.
- **Player Team:** an enum of  **[TeamBlue, TeamRed]** that is the team of the player.
- **Player Champion:** Player’s selected champion, detailed in the Champion section.
- **Player Total Health:** Player’s total health in the game. 
  - **Formula: [Player Champion’s Total Health] + [Extra Total Health Given by Items] + [Extra Total Health Given by Runes]**
- **Player Health Regeneration Speed:** Player’s health regeneration speed in each second of the game.
  - **Formula: [Player Champion’s Health Regeneration Speed] + [Player Champion’s Health Regeneration Speed] * [(Extra Health Regeneration Speed Given by Items +Extra Health Regeneration Speed Given by Runes) / 100]**
- **Player Health Stole Multiplier:** Multiplier value that is multiplied with given damage to calculate the additional health that will be added to the player. If player has health stole multiplier more then zero, each time player attacks and applies damage to the enemy player, the damage will be multiplied with the health stole multiplier and resulting value will be added to the player health.
  - **Formula: [Player Champion’s Health Stole Multiplier] + [Extra Health Stole Multiplier Given by Items] + [Extra Health Stole Multiplier Given by Runes]**
- **Player Health:** Player’s current health in the game. If greater than total health, this value will be set to total health.
- **Player Total Mana:** Player’s total mana in the game. 
  - **Formula: [Player Champion’s Total Mana] + [Extra Total Mana Given by Items] + [Extra Total Mana Given by Runes]**
- **Player Mana Regeneration Speed:** Player’s mana regeneration speed in each second of the game.
  - **Formula: [Player Champion’s Mana Regeneration Speed] + [Player Champion’s Mana Regeneration Speed] * [(Extra Mana Regeneration Speed Given by Items + Extra Mana Regeneration Speed Given by Runes) / 100]**
- **Player Mana:** Player’s current mana in the game. If greater than total mana, this value will be set to total mana.
- **Player Attack Cooldown Time:** Player’s attack cooldown time which makes the player wait for the value of time to make another normal attack.
  - **Formula: [Player Champion’s Attack Cooldown Time] - [Player Champion’s Attack Cooldown Time] * [(Extra Attack Speed Given by Items + Extra Attack Speed Given by Runes) / 100]**
- **Player AD Attack Damage:** Player’s attack damage applied to the enemy player. 
  - **Formula: [Player Champion’s AD Attack Damage] + [Player Champion’s AD Attack Damage] * [(Sum of Extra Attack Damage Given by AD Items + Sum of Extra Attack Damage Given by AD Runes) / 100]**
- **Player AP Attack Damage:** Player’s attack damage applied to the enemy player. 
  - **Formula: [Player Champion’s AP Attack Damage] + [Player Champion’s AP Attack Damage] * [(Sum of Extra Attack Damage Given by AP Items + Sum of Extra Attack Damage Given by AP Runes) / 100]**
- **Player AD Armor:** Player’s armor reduced from incoming damage.
  - **Formula: [Player Champion’s AD Armor] + [Sum of Extra AD Armor Given by Items] + [Sum of Extra AD Armor Given by Runes]**
- **Player AP Armor:** Player’s armor reduced from incoming damage.
  - **Formula: [Player Champion’s AP Armor] + [Sum of Extra AP Armor Given by Items] + [Sum of Extra AP Armor Given by Runes]**
- **Player AD Armor Piercing:** Player’s armor piercing applied to reduce armor value of the targeted enemy player.
  - **Formula: [Player Champion’s AD Armor Piercing] + [Player Champion’s AD Armor Piercing] * [(Extra Armor Piercing Given by AD Items + Extra Armor Piercing Given by AD Runes) / 100]**
- **Player AP Armor Piercing:** Player’s armor piercing applied to reduce armor value of the targeted enemy player.
  - **Formula: [Player Champion’s AP Armor Piercing] + [Player Champion’s AP Armor Piercing] * [(Extra Armor Piercing Given by AP Items + Extra Armor Piercing Given by AP Runes) / 100]**
- **Player Movement Speed:** Player’s movement speed applied while moving.
  - **Formula: [Player Champion’s Movement Speed] + [Player Champion’s Movement Speed] * [(Extra Movement Speed Given by Items + Extra Movement Speed Given by Runes) / 100]**

### Player Attack - Monobehaviour Class

**Name: PlayerAttack.cs**

**Description:** Player Attack is responsible for the normal attacks of the player. Checks several requirements and if they are met player attacks to the enemy team’s any player.

**Variables Important: target, continuouslyCheckRange.**

- If the player right clicks to another gameobject and the raycast's hit object’s layer is **“Player”**, Player Attack will check some conditions.
  - **If the selected player is a teammate,** the player moves to the position of the ally.
  - **If the selected player is not a teammate,** the target variable is set to the enemy player and the player checks the range between two players.
    - **If there is no attack cooldown,**
      - **If the selected player is inside of the range,** an attack will be performed.
      - **If the selected player is not inside of the range,** the player will move towards the enemy player using PlayerMovement. Until there is another input given, variable continuouslyCheckRange will be set to true for checking whether the target is inside of the range or not in each frame. When the target is inside the range, the player will stop moving and attack the other player.
    - **If there is an attack cooldown,** do nothing.
- After each attack there will be a cooldown (wait time) for the other attack.
- **If player attacked**
  - **Player Animator** will run the attack animation.
  - **Player VFX** will instantiate a prefab of the attack vfx.
  - **Player Audio** will play the audio of the attack sound. 

### Player Defense- Monobehaviour Class

**Name: PlayerDefense.cs**

**Description:** Player Defense will take input from the attacker player’s attack damage and calculate the damage which will be applied to the target player. (Player Defense will be the target player’s player defense class.)
- Each attack's applied damage will be calculated as follows:
  - If the attacking player sent an AD attack,
    - **Formula: [Attacking Player AD Attack Damage - Player Defense AD Damaged Armor]**
    - **Player Defense AD Damaged Armor: [Target Player AD Armor - Attacking Player AD Armor Piercing], if value is negative make it 0.**
  - If the attacking player sent an AP attack.
    - **Formula: [Attacking Player AP Attack Damage - Player Defense AP Damaged Armor]**
    - **Player Defense AD Damaged Armor: [Target Player AP Armor - Attacking Player AP Armor Piercing], if value is negative make it 0.**

### Champion - Scriptable Class

**Name: Champion.cs**

**Description:** This class represents the champions available in the game. Each champion will have different features from each other. Features:
- **ID: (Type: int)**
- **Type (Type: Enum[Melee, Ranged]):** There are two types of player characters: Ranged or Melee. Melee characters’ range can be less than or equal to 1f and ranged characters can be higher than that. (1 float = 1 meters)
- **Damage Type (Type: Enum[AD, AP]):** Characters normal attack damage type that is used inside of the game in calculations.
- **Range (Type: Float):** The distance that a player can attack to another player.
- **Attack Cooldown Time: (Type: Float)**
- **AD Attack Damage: (Type: Float)**
- **AP Attack Damage: (Type: Float)**
- **Total Health: (Type: Float)**
- **Health Regeneration Speed: (Type: Float):** Health regeneration speed in seconds.
- **Health Stole Multiplier: (Type: Float)**
- **Total Mana: (Type: Float)**
- **Mana Regeneration Speed: (Type: Float):** Mana regeneration speed in seconds.
- **AD Armor: (Type: Float):** If incoming attack is an AD damage, AD armor will be used to reduce the incoming damage.
- **AP Armor: (Type: Float):** If incoming attack is an AP damage, AP armor will be used to reduce the incoming damage.
- **Armor Piercing AD: (Type: Float):** If player type is AD this will be used otherwise this value set to 0.
- **Armor Piercing AP: (Type: Float):** If player type is AP this will be used otherwise this value set to 0.
- **Movement Speed: (Type: Float):** Movement speed in seconds.
- **Skills (Type: Skill):** [Variable names: qSkill, wSkill, eSkill, rSkill] The skills that a player can perform during battle. Skills are q,w,e and r skills. If a player performs any of these keys, the appropriate skill will be applied.

### Item - Scriptable Class

**Name: Item.cs**

**Description:** This class represents items present in the game. Each item has different changes in the character attributes. Features:
- **ID: (Type: int)**
- **Item Type: (Type: Enum[AD, AP])**
- **Extra Total Health: (Type: Float)**
- **Extra Health Regeneration Speed: (Type: Float)**
- **Extra Health Stole Multiplier: (Type: Float)**
- **Extra Total Mana: (Type: Float)**
- **Extra Mana Regeneration Speed: (Type: Float)**
- **Extra Attack Speed: (Type: Float)**
- **Extra Attack Damage: (Type: Float)**
- **Extra Armor: (Type: Float)**
- **Extra Armor Piercing: (Type: Float**
- **Extra Movement Speed: (Type: Float)**

### Rune - Scriptable Class

**Name: Rune.cs**

**Description:** This class represents runes present in the game. Runes have different effects. Features:
- **ID: (Type: int)**
- **Rune Type: (Type: Enum[AD, AP])**
- **Extra Total Health: (Type: Float)**
- **Extra Health Regeneration Speed: (Type: Float)**
- **Extra Health Stole Multiplier: (Type: Float)**
- **Extra Total Mana: (Type: Float)**
- **Extra Mana Regeneration Speed: (Type: Float)**
- **Extra Attack Speed: (Type: Float)**
- **Extra Attack Damage: (Type: Float)**
- **Extra Armor: (Type: Float)**
- **Extra Armor Piercing: (Type: Float)**
- **Extra Movement Speed: (Type: Float)**

### Attack - Scriptable Class

**Name: Attack.cs**

**Description:** This class represents skills that the players can use, each champion will have a skill set on them.
- **Attack Animation: (Type: Animation)**
- **Attack VFX: (Type: Gameobject)**
- **Attack Sound: (Type: AudioClip)**

### Skill - Scriptable Class

**Name: Skill.cs**

**Description:** This class represents skills that the players can use, each champion will have a skill set on them.
- **ID: (Type: int)**
- **Skill Type: (Type: Enum[AD, AP])**
- **Radius: (Type: Float)**
- **Range: (Type: Float)**
- **Attack Damage: (Type: Float)**
- **Armor: (Type: Float)**
- **Start Time: (Type: Float)**
- **End Time: (Type: Float)**

### Player Skill - Monobehaviour Class

**Name: PlayerSkill.cs**

**Description:** Player Skill is responsible for the skills of the player. Checks several requirements and if they are met player attacks to the enemy team’s any player.
Variables Important: **target**.
- If the player uses keys **[q, w, e or r],** the player will perform a skill in the direction of the right click button. Skill set for each champion is given as qSkill, wSkill, eSkill, rSkill in the Champion which is referenced through Player Data.
- **If there is no cooldown on skill,** the player performs the skill
  - **If there is enough mana,** the player performs the skill.
    - **If there is an enemy inside of the skill area,** the enemy inside of the area will take damage using Player Skill’s apply damage instead of Player Attack’s apply damage.
  - **If there is not enough mana,** do nothing.
- **If there is a skill cooldown,** do nothing.
- After each skill there will be a cooldown (wait time) for the other attack.
- Each attack's applied damage will be calculated as follows:
  - **Formula: [Player Attack Damage - Damaged Armor]**
  - **Damaged Armor: [Armor - Armor Piercing], if value is negative make it 0.**

### Player VFX - Monobehaviour Class

**Name: PlayerVFX.cs**

**Description:** Player Effect is responsible for displaying the vfx effects as a result of player actions.
- Attacks and skills of champions have a vfx component added to them. This vfx component is being instantiated in PlayerVFX.
- Virtual effect’s display position is the same position as the Player Skill’s calculated position. That’s why, Player Skill calls Player VFX with a position given.

_**Note:** Player VFX uses slashes instead of trails. This is done for modularity and variation in champions._

