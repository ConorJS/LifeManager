using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class AppointmentMapper : IAppointmentMapper {
        private readonly ITaskMapper _taskMapper;

        public AppointmentMapper(ITaskMapper taskMapper) {
            _taskMapper = taskMapper;
        }

        public Appointment ToDomain(AppointmentEntity entity) {
            Appointment domain = new Appointment { };

            _taskMapper.ToDomain(entity, domain);

            return domain;
        }

        public AppointmentEntity ToEntity(Appointment domain) {
            AppointmentEntity entity = new AppointmentEntity { };

            _taskMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}