namespace LifeManager.Server.Model.Mapper {
    public interface ITaskMapper {
        public void ToDomain(ITaskEntity entity, ITask domain);

        public void ToEntity(ITask domain, ITaskEntity entity);
    }
}