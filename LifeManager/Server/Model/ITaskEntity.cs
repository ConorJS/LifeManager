namespace LifeManager.Server.Model {
    public interface ITaskEntity : IItemEntity {
        public int RelativeSize { get; set; }
    }
}