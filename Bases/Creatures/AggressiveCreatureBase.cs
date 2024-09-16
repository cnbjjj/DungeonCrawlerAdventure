using DungeonCrawlerAdventure.Contracts.Creatures;
using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawlerAdventure.Events.Combats;

namespace DungeonCrawlerAdventure.Bases.Creatures
{
    public abstract class AggressiveCreatureBase : CreatureBase, IAggressive
    {
        public event EventHandler<CombatEventArgs>? OnAttack;
        public event EventHandler<CombatEventArgs>? OnDefend;

        public virtual void TriggerAttack(ICreature target)
        {
            OnAttack?.Invoke(this, new CombatEventArgs(this, target, Strength));
        }

        public virtual void TriggerDefend(ICreature attacker, int damage)
        {
            OnDefend?.Invoke(this, new CombatEventArgs(attacker, this, damage));
        }
        public void Attack(ICreature target)
        {
            if (!IsAlive) return;
            if (target is IAggressive)
            {
                TriggerAttack(target);
                ((IAggressive)target).Defend(this, Strength);
            }
            else 
                throw new ArgumentException("Target is not an Aggressive creature!");
        }
        public void Defend(ICreature attacker, int damage)
        {
            int actualDamage = damage - Armor;
            if (Armor > 0)
                Armor = Math.Max(0, Armor - damage);
            if (actualDamage > 0)
                Health = Math.Max(0, Health - actualDamage);
            if (!IsAlive)
                Die();
            else
                TriggerDefend(attacker, damage);
        }
    }
}
