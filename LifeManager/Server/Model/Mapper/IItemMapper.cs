namespace LifeManager.Server.Model.Mapper {
    public interface IItemMapper {
        public void ToDomain(IItemEntity entity, IItem domain);

        public void ToEntity(IItem domain, IItemEntity entity);
    }
}