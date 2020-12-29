using System.Collections.Generic;
using LifeManager.Server.Model.Domain;

namespace LifeManager.Server.Service {
    public interface IAppointmentService {
        public IEnumerable<Appointment> GetAll();

        public Appointment GetById(long id);

        public void Create(Appointment domain);

        public void Update(Appointment domain);

        public void Remove(long id);
    }
}