using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public interface IAppointmentMapper {
        public Appointment ToDomain(AppointmentEntity entity);

        public AppointmentEntity ToEntity(Appointment domain);
    }
}