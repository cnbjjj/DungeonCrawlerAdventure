using DungeonCrawlerAdventure.Contracts;

namespace DungeonCrawlerAdventure
{
    public interface IUseable
    {
        public bool IsUsed { get; }
        public Effect Effect { get;}
        public Effect Use();
    }
}
