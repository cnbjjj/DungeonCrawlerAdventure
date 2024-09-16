using DungeonCrawlerAdventure.Contracts;

namespace DungeonCrawlerAdventure
{
    public interface ICreature
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Armor { get; set; }
        public void ApplyEffect(Effect effect);
    }
}
