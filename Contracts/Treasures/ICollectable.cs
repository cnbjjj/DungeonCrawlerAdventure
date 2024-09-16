using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawlerAdventure
{
    public interface ICollectable
    {
        public ICreature? Owner { get; set; }
        public bool Collect(ICreature creature);
    }
}
