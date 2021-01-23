using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class AppointmentMapper : IAppointmentMapper {
        private readonly IItemMapper _itemMapper;

        public AppointmentMapper(IItemMapper itemMapper) {
            _itemMapper = itemMapper;
        }

        public Appointment ToDomain(AppointmentEntity entity) {
            Appointment domain = new Appointment { };

            _itemMapper.ToDomain(entity, domain);

            return domain;
        }

        public AppointmentEntity ToEntity(Appointment domain) {
            AppointmentEntity entity = new AppointmentEntity { };

            _itemMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}