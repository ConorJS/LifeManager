namespace LifeManager.Server.User.Configuration {
    public class ColumnSortOrder {
        public long UserConfigurationId { get; set; }
        
        public bool IsSortedAscending { get; set; }
        
        public string ColumnName { get; set; }

        public int Precedence { get; set; }
    }
}