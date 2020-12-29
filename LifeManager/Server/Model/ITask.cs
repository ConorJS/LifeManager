namespace LifeManager.Server.Model {
    public interface ITask : IItem {
        public int RelativeSize { get; set; }
    }
}