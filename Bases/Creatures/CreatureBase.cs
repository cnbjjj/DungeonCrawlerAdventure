using DungeonCrawlerAdventure.Contracts;

namespace DungeonCrawlerAdventure.Bases.Creatures
{
    public abstract class CreatureBase : ICreature
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Armor { get; set; }
        public bool IsAlive => Health > 0;

        public event EventHandler<Effect>? OnApplyEffectSuccessed;
        public event EventHandler<Effect>? OnApplyEffectFailed;
        public event EventHandler<ICreature>? OnDeath;

        public virtual void TriggerApplyEffectSuccessed(Effect effect)
        {
            OnApplyEffectSuccessed?.Invoke(this, effect);
        }
        public virtual void TriggerApplyEffectFailed(Effect effect)
        {
            OnApplyEffectFailed?.Invoke(this, effect);
        }
        public virtual void TriggerDeath()
        {
            OnDeath?.Invoke(this, this);
        }
        public virtual void ApplyEffect(Effect effect)
        {
            Health += effect.Health ?? 0;
            Strength += effect.Strength ?? 0;
            Armor += effect.Armor ?? 0;
            Health = Math.Max(0, Health);
            Strength = Math.Max(0, Strength);
            Armor = Math.Max(0, Armor);
            TriggerApplyEffectSuccessed(effect);
            if (!IsAlive)
                Die();
        }
        public virtual void Die()
        {
            TriggerDeath();
        }
        public override string ToString()
        {
            return $"{Name} | HP:{Health} | ST:{Strength} | AR: {Armor}";
        }
    }
}
