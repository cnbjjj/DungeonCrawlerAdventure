namespace DungeonCrawlerAdventure.Bases.Treasures
{
    public abstract class TreasureBase : ITreasure
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Value { get; set; } = 0;
        public override string ToString()
        {
            return $"{Name} | {Description}";
        }
    }
}
