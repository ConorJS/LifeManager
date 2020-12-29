using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public class AppointmentMapper {
        public Appointment ToDomain(AppointmentEntity entity) {
            Appointment domain = new Appointment { };

            new TaskMapper().ToDomain(entity, domain);

            return domain;
        }

        public AppointmentEntity ToEntity(Appointment domain) {
            AppointmentEntity entity = new AppointmentEntity { };

            new TaskMapper().ToEntity(domain, entity);

            return entity;
        }
    }
}