# Dungeon Crawler Adventure
A simple console-based dungeon crawler adventure game prototype exercise, focusing on player interactions, event handling, and strategic decisions.
## Game Instructions

In **Dungeon Crawler Adventure**, your goal is to **survive** and **earn as many points as possible** before you die. Explore the dungeon by selecting paths and encountering random events that can either help or hurt you.

### Player Stats:
- **Health (HP)**: Your life points.
- **Strength (ST)**: Used to fight monsters.
- **Armor (AR)**: Reduces incoming damage.
- **Score**: Earned by fighting monsters and collecting treasures.
- **Coins**: Collect these as you progress.

### Game Flow:
Each round, you'll choose a path to proceed and may encounter one of the following events:

1. **Nothing Happens**: Move to the next round without any impact.
2. **Fight a Monster**: Monsters have varying **HP**, **ST**, and **AR**. You can:
   - **Fight**: Use your **Strength (ST)** to attack. Armor reduces damage taken by both you and the monster.
   - **Flee**: Escape, but the monster attacks you once.
3. **Find a Treasure Box**: Collect treasures with random effects on your **HP**, **ST**, **Score**, or **Coins**—some may even cause harm!
4. **Encounter a Trap**: You can:
   - **Roll a Dice**: Take a chance to escape without damage.
   - **Take Damage**: Escape immediately by losing **HP**.

The game continues until your **HP** runs out.

### Controls:
Enter numbers to choose options for each event.

### Download

You can download the executable to give it a try:

[Download Dungeon Crawler Adventure](https://github.com/cnbjjj/DungeonCrawlerAdventure/releases/download/v0.1.0-alpha/DungeonCrawlerAdventure.exe)

Good luck, and try to collect as many treasures as possible!

---

## Project Structure:
### 1. Bases
Defines the core properties and behavior for creatures and treasures.
- **Creatures/**: Base classes for creatures.
  - `AggressiveCreatureBase.cs`: Base class for creatures that can attack.
  - `CreatureBase.cs`: General base class for all creatures.
  - `PlayerBase.cs`: Base class for the player character.
- **Treasures/**: Base classes for treasures.
  - `TreasureBase.cs`: General base class for treasures.
  - `UseableTreasureBase.cs`: Base for treasures that can be used.

### 2. Contracts
Defines the interfaces for behavior and properties of creatures, treasures, and effects.
- **Creatures/**: Interfaces for creatures.
  - `IAggressive.cs`: Defines attacking behavior.
  - `ICreature.cs`: Defines general creature properties.
  - `ICollector.cs`: Defines behavior for collecting and using treasures.
- **Treasures/**: Interfaces for treasure behavior.
  - `ICollectable.cs`: Defines how treasures are collected.
  - `ITreasure.cs`: Defines general treasure properties.
  - `IUseable.cs`: Defines behavior for usable treasures.
- **Effects/**: Interfaces related to the effects of treasures or abilities.
  - `Effect.cs`: Defines the properties of an effect (healing, damage, etc.).

### 3. Concretes
Specific implementations of creatures and treasures.
- **Creatures/**: Specific creature types.
  - `Hero.cs`: Generic hero class.
  - `Monster.cs`: Generic monster class.
- **Treasures/**: Specific treasure types.
  - `MysteriousTreasureBox.cs`: A special treasure box with random items.
  - `Treasure.cs`: Standard treasure class.

### 4. Events
Contains classes for handling game events like combat.
- `CombatEventArgs.cs`: Defines data for combat-related events.

### 5. Utilities
General utility methods.
- **GameIO.cs**: Handles input/output operations (e.g., player choices, printed messages).

### Main Components:
- **DungeonCrawlerAdventureGame.cs**: Main game controller for gameplay flow.
- **Program.cs**: Entry point for the game with the `Main()` method.
