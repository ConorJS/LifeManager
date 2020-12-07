using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface IAppointmentService {
        public Appointment GetById(long id);

        public void Create(Appointment domain);
    }
}