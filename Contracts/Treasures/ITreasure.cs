namespace DungeonCrawlerAdventure
{
    public interface ITreasure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }

    }
}
