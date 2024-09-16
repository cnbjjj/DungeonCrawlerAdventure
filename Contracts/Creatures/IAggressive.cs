namespace DungeonCrawlerAdventure.Contracts.Creatures
{
    public interface IAggressive
    {
        public void Attack(ICreature target);
        public void Defend(ICreature attacker, int damage = 0);
    }
}
