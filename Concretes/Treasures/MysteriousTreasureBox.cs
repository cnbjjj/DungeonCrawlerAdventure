namespace DungeonCrawlerAdventure.Concretes.Treasures
{
    public class MysteriousTreasureBox
    {
        private readonly List<ITreasure> _treasures;
        public MysteriousTreasureBox(List<ITreasure> treasures) => _treasures = treasures;
        public ITreasure Open()
        {
            int rand = new Random().Next(_treasures.Count);
            return _treasures[rand];
        }
    }
}