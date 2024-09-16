using DungeonCrawlerAdventure.Contracts;

namespace DungeonCrawlerAdventure.Events.Combats
{
    public class CombatEventArgs : EventArgs
    {
        public ICreature Attacker { get; }
        public ICreature Defender { get; }
        public int Damage { get; }
        public Effect Effect { get; set; }
        public CombatEventArgs(ICreature attacker, ICreature defender, int damage)
        {
            Attacker = attacker;
            Defender = defender;
            Damage = damage;
        }
    }
}
