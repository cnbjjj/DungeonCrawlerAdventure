using DungeonCrawlerAdventure.Contracts;

namespace DungeonCrawlerAdventure.Bases.Creatures
{
    public abstract class PlayerBase : AggressiveCreatureBase, ICollector
    {
        public event EventHandler<ITreasure>? OnTreasureCollected;
        public event EventHandler<ITreasure>? OnTreasureDiscarded;
        public event EventHandler<ITreasure>? OnTreasureUsed;
        public int Score { get; set; }
        public List<ITreasure> Treasures { get; set; } = new List<ITreasure>();
        public virtual void CollectTreasure(ITreasure treasure)
        {
            if (treasure is ICollectable collectable)
            {
                collectable.Collect(this);
                Treasures.Add(treasure);
                TriggerTreasureCollected(treasure);
            }
            else
            {
                throw new ArgumentException("Treasure is not collectable!");
            }
        }
        public virtual void DiscardTreasure(ITreasure treasure)
        {
            Treasures.Remove(treasure);
            TriggerTreasureDiscarded(treasure);
        }
        public virtual void UseTreasure(IUseable treasure)
        {
            if (Treasures.Contains((ITreasure)treasure))
            {
                try
                {
                    Effect effect = treasure.Use();
                    ApplyEffect(effect);
                    Treasures.Remove((ITreasure)treasure);
                    TriggerTreasureUsed((ITreasure)treasure);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }

            }
            else
            {
                throw new ArgumentException("Treasure is not in the inventory!");
            }
        }
        public virtual void TriggerTreasureCollected(ITreasure treasure)
        {
            OnTreasureCollected?.Invoke(this, treasure);
        }
        public virtual void TriggerTreasureDiscarded(ITreasure treasure)
        {
            OnTreasureDiscarded?.Invoke(this, treasure);
        }
        public virtual void TriggerTreasureUsed(ITreasure treasure)
        {
            OnTreasureUsed?.Invoke(this, treasure);
        }
        public override void ApplyEffect(Effect effect)
        {
            Score += effect.Score ?? 0;
            base.ApplyEffect(effect);
        }
        public override string ToString()
        {
            return $"{Name} | HP:{Health} | ST:{Strength} | AR:{Armor} | Score:{Score}";
        }
        ~PlayerBase()
        {
            //Dispose(false);
        }
    }
}
