using DungeonCrawlerAdventure.Bases.Creatures;
using DungeonCrawlerAdventure.Contracts;
using DungeonCrawlerAdventure.Concretes.Creatures;
using DungeonCrawlerAdventure.Concretes.Treasures;
using Utilities;

namespace DungeonCrawlerAdventure
{
    public enum PathOptions
    {
        Left = 1,
        Right = 2,
        Straight = 3,
        DeadEnd = 4
    }
    public enum EventOptions
    {
        Monster = 1,
        Trap = 2,
        Treasure = 3,
        Nothing = 4
    }
    public enum GameEventOptions
    {
        MainMenu,
        GamePlay,
        GameOver,
        GameWin
    }
    public enum PlayerState
    {
        Idle,
        Fighting,
        Trapped
    }
    public class DungeonCrawlerAdventureGame
    {
        private const string INPUT = "  <= ";
        private const string OUTPUT = "  => ";

        private static int Round = 0;
        private GameEventOptions _dcaGameState;
        private PlayerState _PlayerState;

        public event Action<GameEventOptions>? OnGameEventTriggered;
        public event Action<EventOptions>? OnEventTriggered;

        public PlayerBase Player;
        public Monster? Monster;

        private readonly List<ITreasure> Treasures =
        [
            new Treasure() {
                Name = "Gold",
                Value = new Random().Next(100),
                Description = "Gold will increase your score",
                Effect = new Effect() { Score = new Random().Next(100) }
            },
            new Treasure() {
                Name = "ACID",
                Value = new Random().Next(100),
                Description = "!!!Danger!!! ACID will damage your health",
                Effect = new Effect() { Health = -10 }
            },
            new Treasure() {
                Name = "Health Potion",
                Value = new Random().Next(50),
                Description = "Restores health when used",
                Effect = new Effect() { Health = 20 }
            },
            new Treasure() {
                Name = "Magic Scroll",
                Value = new Random().Next(100),
                Description = "A mysterious scroll that increases your strength",
                Effect = new Effect() { Strength = 15 }
            },
            new Treasure() {
                Name = "Diamond",
                Value = new Random().Next(200),
                Description = "A valuable diamond that boosts your score",
                Effect = new Effect() { Score = new Random().Next(200) }
            }
        ];
        public DungeonCrawlerAdventureGame()
        {
            InitGameEventHandlers();
            OnGameEventTriggered?.Invoke(GameEventOptions.MainMenu);
        }
        public void StartGame()
        {
            Round = 0;
            InitializePlayer();
            OnGameEventTriggered?.Invoke(GameEventOptions.GamePlay);
        }
        public void EndGame()
        {
            OnGameEventTriggered?.Invoke(GameEventOptions.GameOver);
        }
        public void WinGame()
        {
            OnGameEventTriggered?.Invoke(GameEventOptions.GameWin);
        }
        private void InitializePlayer()
        {
            Player = new Hero() { Name = "Knight", Health = 100, Armor = 50, Strength = 20 };
            Player.OnDeath += (sender, creature) =>
            {
                Output($"You died!");
                EndGame();
            };
            Player.OnTreasureCollected += (sender, treasure) =>
            {
                Output($"You collected a treasure: [ {treasure.Name} - {treasure.Description} ]");
                Output($"You now have {Player.Treasures.Count} treasures in your bag!");
                // Automatically use the treasure
                Player.UseTreasure((IUseable)treasure);
            };
            Player.OnTreasureUsed += (sender, treasure) =>
            {
                Output($"You used treasure: {treasure.Name} [ {((IUseable)treasure).Effect} ]");
                Output($"You now have {Player.Treasures.Count()} treasures in your bag!");
            };
            Player.OnApplyEffectSuccessed += (sender, effect) =>
            {
                Output($"[ {effect} ]");
            };
            Player.OnAttack += (sender, args) => Output($"You attacked [ {args.Defender.Name} ] with [ {args.Damage} ] damage!");
            _PlayerState = PlayerState.Idle;
        }
        private void InitGameEventHandlers()
        {
            OnEventTriggered += HandleMonsterEvent;
            OnEventTriggered += HandleTrapEvent;
            OnEventTriggered += HandleTreasureEvent;
            OnEventTriggered += HandleNothingEvent;
            OnGameEventTriggered += OnGameEvent;
        }
        private Monster GenerateMonster()
        {
            string[] randomNames =
            [
                "Goblin",
                "Orc",
                "Troll",
                "Skeleton",
                "Zombie",
                "Dragon"
            ];
            Monster dragon = new Monster()
            {
                Name = $"{randomNames[new Random().Next(randomNames.Length)]}",
                Health = new Random().Next(10, 100),
                Armor = new Random().Next(0, 100),
                Strength = new Random().Next(10, 50)
            };
            dragon.OnDeath += (sender, creature) =>
            {
                Output($"[ {creature.Name} ] died!");
                Player.ApplyEffect(new Effect() { Score = creature.Strength });
                Output($"You gained [ {creature.Strength} ] score!");
                if (_PlayerState == PlayerState.Fighting)
                    _PlayerState = PlayerState.Idle;
            };
            dragon.OnAttack += (sender, args) => Output($"[ {args.Attacker.Name} ] attacked you with [ {args.Damage} ] damage!");
            return dragon;
        }
        // Event handlers
        private void HandleNothingEvent(EventOptions evt)
        {
            if (evt != EventOptions.Nothing) return;
            Output("Nothing happened.");
            int sel = PromptAction("You can [  1.Continue  ]", 1, 1);
        }
        private void HandleMonsterEvent(EventOptions evt)
        {
            if (evt != EventOptions.Monster) return;
            string msg = "";
            if (_PlayerState == PlayerState.Fighting)
                msg = $"Fighting with: [ {Monster} ]";
            else
            {
                _PlayerState = PlayerState.Fighting;
                Monster = GenerateMonster();
                msg = $"You encountered a monster!: [ {Monster} ]";

            }
            Output(msg);
            int sel = PromptAction($"You can [  1.Fight!  2.Flee!  ]");
            if (sel == 1)
            {
                Player.Attack(Monster);
                Monster.Attack(Player);
            }
            if (sel == 2)
            {
                Monster.Attack(Player);
                Output($"You fled.");
                _PlayerState = PlayerState.Idle;
            }
        }
        private void HandleTrapEvent(EventOptions evt)
        {
            if (evt != EventOptions.Trap) return;
            _PlayerState = PlayerState.Trapped;
            Output("You encountered a trap!");
            int sel = PromptAction("You can [  1.Roll dice!  2.Take damage!  ]");
            if (sel == 1)
            {
                int luck = new Random().Next(1, 10);
                if (luck > 5) // 50% chance to escape
                {
                    _PlayerState = PlayerState.Idle;
                    Output($"Congrats! You rolled [ {luck} ] and escaped the trap!");
                }
                else
                {
                    Player.ApplyEffect(new Effect() { Health = -5 });
                    Output($"You rolled [ {luck} ] and failed to escape the trap!");
                    Output($"You took [ 5 ] damage!");
                }
            }
            if (sel == 2)
            {
                int damage = 10;
                Player.ApplyEffect(new Effect() { Health = -damage });
                Output($"You took [ {damage} ] damage to escape from the trap!");
                _PlayerState = PlayerState.Idle;
            }
        }
        private void HandleTreasureEvent(EventOptions evt)
        {
            if (evt != EventOptions.Treasure) return;
            Output("You found a mysterious treasure box!");
            int sel = PromptAction("You can [  1.Open it!  2.Leave it!  ]");
            if (sel == 1)
            {
                ITreasure treasure = new MysteriousTreasureBox(Treasures).Open();
                Player.CollectTreasure(treasure);
            }
            if (sel == 2)
            {
                Output("You left the mysterious treasure box!");
            }
        }

        // Game states handlers
        private void ShowMainMenu()
        {
            Console.Clear();
            Output("Welcome to Dungeon Crawler Adventure!");
            Output("Explore the dungeon, fight monsters, avoid traps, and collect treasures.");
            Output("Your goal is to survive and collect as many treasures as possible.");
            Output("Good Luck!");
            PrintLn("");
            int sel = PromptAction("Do you want to start the game? [  1.Yes  2.No  ]");
            if (sel == 1) StartGame();
        }
        private void ShowResult(string message)
        {
            PrintLn("\n\n");
            Output(message);
            Output($"Your score: {Player.Score}");
            int sel = PromptAction("Do you want to play again? [  1.Yes  2.No  ]");
            if (sel == 1) StartGame();
        }
        private void GamePlay()
        {
            Console.Clear();
            Output($"Game Started!");
            Run();
        }
        private void GameOver() => ShowResult($"Game Over.");
        private void GameWin() => ShowResult($"You Win!");
        private void OnGameEvent(GameEventOptions evt)
        {
            _dcaGameState = evt;
            switch (evt)
            {
                case GameEventOptions.MainMenu:
                    ShowMainMenu();
                    break;
                case GameEventOptions.GamePlay:
                    GamePlay();
                    break;
                case GameEventOptions.GameOver:
                    GameOver();
                    break;
                case GameEventOptions.GameWin:
                    GameWin();
                    break;
                default:
                    break;
            }
        }
        private void Run()
        {
            int sel = 0;
            int evt = 0;
            while (_dcaGameState == GameEventOptions.GamePlay)
            {
                PrintLn("\n\n");
                Output($"Round {++Round}");
                Output($"Your status: [ {Player} | State:{_PlayerState} ]");
                evt = GenerateNextEvent(_PlayerState);
                if (_PlayerState == PlayerState.Idle)
                {
                    string options = string.Join("  ", GetPathOptions().Select(o => $"{o}.{Enum.GetName(typeof(PathOptions), o)}"));
                    sel = PromptAction($"You can move to [  {options}  ]", input => options.Contains(input));
                }
                OnEventTriggered?.Invoke((EventOptions)evt);
            }
        }
        private int GenerateNextEvent(PlayerState ps)
        {
            int evt = TriggerEvent();
            if (ps == PlayerState.Fighting)
                evt = (int)EventOptions.Monster;
            if (ps == PlayerState.Trapped)
                evt = (int)EventOptions.Trap;
            return evt;
        }

        //Utility methods
        private static int PromptAction(string message, int min = 1, int max = 2)
        {
            int selection = PromptAction(message, input => int.TryParse(input, out int result) && result >= min && result <= max);
            return selection;
        }
        private static int PromptAction(string message, Func<string, bool> validator)
        {
            Output(message);
            int selection = GameIO.ReadInputAsInt($"{INPUT}", validator, $"{OUTPUT}Invalid input. Please try again!");
            return selection;
        }
        private static void Output(string message)
        {
            PrintLn($"{OUTPUT}{message}");
        }
        private static void PrintLn(string message)
        {
            GameIO.PrintLn(message);
        }

        // Provied code
        public static int[] GetPathOptions()
        {
            Random random = new Random();
            // Check if it's a dead end
            bool isDeadEnd = random.Next(0, 10) == 0; // 25% chance for a dead end

            if (isDeadEnd)
            {
                return new int[] { (int)PathOptions.DeadEnd };
            }
            else
            {
                // Randomize available directions (left, right, straight)
                List<int> options = new List<int>();

                if (random.Next(0, 2) == 1) options.Add((int)PathOptions.Left);
                if (random.Next(0, 2) == 1) options.Add((int)PathOptions.Right);
                if (random.Next(0, 2) == 1) options.Add((int)PathOptions.Straight);

                // If no options were randomly selected, return all options (to avoid a lockout)
                if (options.Count == 0)
                {
                    options.AddRange(new int[] { (int)PathOptions.Left, (int)PathOptions.Right, (int)PathOptions.Straight });
                }

                return options.ToArray();
            }
        }
        public static int TriggerEvent()
        {
            Random random = new Random();
            // Randomly select an event (Monster, Trap, Treasure, Nothing)
            return random.Next(1, 5); // This returns an int between 1 and 4 inclusive
        }
    }
}