namespace DungeonCrawlerAdventure.Contracts
{
    public struct Effect
    {
        public int? Health { get; set; }
        public int? Strength { get; set; }
        public int? Armor { get; set; }
        public int? Score { get; set; }
        public int? Coins { get; set; }
        public override string ToString()
        {
            string str = string.Empty;
            if (Health != null)
                str += $"HP:{GetValue(Health)} ";
            if (Strength != null)
                str += $"ST:{GetValue(Strength)} ";
            if (Armor != null)
                str += $"AR:{GetValue(Armor)} ";
            if (Score != null)
                str += $"Score: {GetValue(Score)} ";
            if (Coins != null)
                str += $"Coins: {GetValue(Coins)} ";
            return str; 
        }
        private string GetValue(int? value)
        {
            return (value > 0 ? "+" : "") + value;
        }
    }
}
