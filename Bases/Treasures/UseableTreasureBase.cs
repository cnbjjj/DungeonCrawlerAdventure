using DungeonCrawlerAdventure.Contracts;

namespace DungeonCrawlerAdventure.Bases.Treasures
{
    public abstract class UseableTreasureBase : TreasureBase, IUseable, ICollectable
    {
        protected bool _isUsed;
        public bool IsUsed { get => _isUsed; }
        public bool IsReusable { get; set; } = true;
        public ICreature? Owner { get; set; }
        public Effect Effect { get; set; } = new Effect();
        public virtual bool Collect(ICreature creature)
        {
            if (Owner != null || Owner == creature)
                return false;
            Owner = creature;
            return Owner != null;
        }
        public virtual Effect Use()
        {
            if (IsReusable && Owner != null)
                return Effect;
            if (IsUsed || Owner == null)
                return new Effect();
            _isUsed = true;
            return Effect;
        }
    }
}
