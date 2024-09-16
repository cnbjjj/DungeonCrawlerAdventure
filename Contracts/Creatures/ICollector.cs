namespace DungeonCrawlerAdventure
{
    public interface ICollector
    {
        public List<ITreasure> Treasures { set; get; }
        public void CollectTreasure(ITreasure treasure);
        public void DiscardTreasure(ITreasure treasure);
        public void UseTreasure(IUseable treasure);
    }
}
