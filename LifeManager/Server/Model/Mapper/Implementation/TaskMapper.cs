namespace LifeManager.Server.Model.Mapper.Implementation {
    public class TaskMapper : ITaskMapper {
        private readonly IItemMapper _itemMapper;

        public TaskMapper(IItemMapper itemMapper) {
            _itemMapper = itemMapper;
        }

        public void ToDomain(ITaskEntity entity, ITask domain) {
            _itemMapper.ToDomain(entity, domain);

            domain.RelativeSize = entity.RelativeSize;
        }

        public void ToEntity(ITask domain, ITaskEntity entity) {
            _itemMapper.ToEntity(domain, entity);

            entity.RelativeSize = domain.RelativeSize;
        }
    }
}